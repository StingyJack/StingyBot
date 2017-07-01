namespace StingyBot.ConsoleHost
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Bot;
    using log4net;

    internal class Program
    {
        private const string LOCALTUNNEL_CMD = "lt.cmd";
        private static Process _localtunnel;
        private static ILog _logger;

        private static void Main()
        {
            if (!HttpListener.IsSupported)
            {
                throw new InvalidOperationException();
            }
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            var bot = new PluggableBot();

            var botConfigClone = bot.Configure(_logger);
            //You can also bot.Configure(logger, Dictionary<string, IConfigurationRoot) if you want to specify your own configs

            if (botConfigClone.RequiresExternalWebHookAccess.HasValue
                && botConfigClone.RequiresExternalWebHookAccess.Value == true)
            {
                StartLocalTunnel(botConfigClone.BaseWebHookAddress, "stinginess");
            }

            Task.WaitAll(bot.StartupAsync());

            Console.WriteLine("Press enter to shutdown");
            Console.ReadLine();

            Console.WriteLine("Shutting Down");
            bot.Shutdown();
            ShutdownLocalTunnel();
        }

        private static void ShutdownLocalTunnel()
        {
            _localtunnel?.Close();
            _localtunnel?.Dispose();
        }

        private static void StartLocalTunnel(string baseWebHookAddress, string subDomain)
        {
            VerifyNpmAndLocalTunnelModulesAreAvailable();

            var nodeModulesBasePath = GetNodejsBasePath();

            var url = new Uri(baseWebHookAddress);
            var port = url.Port;
            var args = $"--port {port} --subdomain {subDomain}";

            _localtunnel = new Process();
            _localtunnel.StartInfo.FileName = LOCALTUNNEL_CMD;
            _localtunnel.StartInfo.Arguments = args;
            _localtunnel.StartInfo.UseShellExecute = false;
            _localtunnel.StartInfo.RedirectStandardOutput = true;
            _localtunnel.StartInfo.CreateNoWindow = true;
            _localtunnel.StartInfo.WorkingDirectory = nodeModulesBasePath;

            var consoleStandardOut = new StringBuilder();
            var consoleErrors = new StringBuilder();
            _localtunnel.OutputDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null) { consoleStandardOut.Append(eventArgs.Data); }
            };
            _localtunnel.ErrorDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null) { consoleErrors.Append(eventArgs.Data); }
            };

            _localtunnel.Start();
            _localtunnel.BeginOutputReadLine();

            const int LOCALTUNNEL_START_TIMEOUT_MS = 5000;
            var enoughOfAResponseHasBeenReceived = false;
            var theResponseIsGood = false;
            var startTime = DateTime.Now;

            while (enoughOfAResponseHasBeenReceived == false)
            {
                if (DateTime.Now.Subtract(startTime).TotalMilliseconds >= LOCALTUNNEL_START_TIMEOUT_MS)
                {
                    _logger.Error($"Timeout hit while waiting for localtunnel to start {LOCALTUNNEL_START_TIMEOUT_MS}ms");
                    enoughOfAResponseHasBeenReceived = true;
                }

                Thread.Sleep(100);

                var stdOutSnapshot = consoleStandardOut.ToString();
                var stdErrSnapshot = consoleErrors.ToString();

                if (string.IsNullOrWhiteSpace(stdErrSnapshot) == false)
                {
                    _logger.Error($"Errors read from localtunnel startup: {stdErrSnapshot}");
                    enoughOfAResponseHasBeenReceived = true;
                }

                if (stdOutSnapshot.IndexOf($"your url is: https://{subDomain}.localtunnel.me", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    enoughOfAResponseHasBeenReceived = true;
                    theResponseIsGood = true;
                }
            }

            if (theResponseIsGood == false)
            {
                var msg = $"Failed to get localtunnel. Errors: {consoleErrors}, StdOut: {consoleStandardOut}";
                _logger.Error(msg);
                throw new InvalidOperationException(msg);
            }
        }

        private static void VerifyNpmAndLocalTunnelModulesAreAvailable()
        {
            var nodejsBasePath = GetNodejsBasePath();

            if (Directory.Exists(nodejsBasePath) == false)
            {
                throw new NotSupportedException($"Cant find the usual node js path {nodejsBasePath}");
            }

            var localTunnelModulePath = $"{nodejsBasePath}\\node_modules\\localtunnel";
            if (Directory.Exists(localTunnelModulePath) == false)
            {
                throw new NotSupportedException(
                    $"Localtunnel does not appear to be installed at {localTunnelModulePath}. Run 'npm install -g localtunnel' to install it.");
            }

            var ltCmdPath = $"{nodejsBasePath}\\{LOCALTUNNEL_CMD}";
            if (File.Exists(ltCmdPath) == false)
            {
                throw new FileNotFoundException("Cant find localtunnel cmd executable", ltCmdPath);
            }
        }

        private static string GetNodejsBasePath()
        {
            var nodeModulesBasePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\npm";
            return nodeModulesBasePath;
        }
    }
}