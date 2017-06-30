// ReSharper disable UnusedMember.Global
namespace StingyBot.Handlers
{
    using System;
    using System.Threading;
    using Common;
    using Common.HandlerInterfaces;
    using Microsoft.Extensions.Configuration;
    using SlackConnector.Models;

    public class TeaTimeBackgroundTaskHandler : SlackConnectedBase, IBackgroundTaskHandler
    {
        private DateTime? _teaTime;
        private DateTime? _elevensies;
        private Thread _clockWatcher;
        private bool _isRunning;
        private TeaTimeConfig _config;

        public void Configure(IConfigurationRoot configurationRoot)
        {
            _config = new TeaTimeConfig();
            configurationRoot.Bind(_config);

            SetTheTeaTimes();
            _clockWatcher = new Thread(CheckTheTime);
            _clockWatcher.IsBackground = true;
        }

        private void SetTheTeaTimes()
        {
            _teaTime = DateTime.Today.Add(new TimeSpan(Convert.ToInt32(_config.HighTeaHour), 0, 0));
            _elevensies = DateTime.Today.Add(new TimeSpan(Convert.ToInt32(_config.LowTeaHour), 0, 0));
        }

        private void CheckTheTime()
        {
            const int SLEEP_TIME = 1 * 1000;
            while (_isRunning)
            {
                var now = DateTime.Now;
                if (now.TimeOfDay.TotalSeconds < 10)
                {
                    SetTheTeaTimes();
                    Thread.Sleep(SLEEP_TIME);
                    continue;
                }

                if (_elevensies.HasValue && now >= _elevensies.Value)
                {
                    var botConnect = GetNewSlackConnectionAsync().GetAwaiter().GetResult();
                    botConnect.Say(new BotMessage {Text = "Time for Elevensies!"});
                    _elevensies = null;
                }

                if (_teaTime.HasValue && now >= _teaTime.Value)
                {
                    var botConnect = GetNewSlackConnectionAsync().GetAwaiter().GetResult();
                    botConnect.Say(new BotMessage { Text = "High Tea must begin!" });
                    _teaTime = null;
                }
                Thread.Sleep(SLEEP_TIME);
            }
        }


        public void Start()
        {
            _isRunning = true;
            _clockWatcher.Start();
        }

        public void Stop()
        {
            _isRunning = false;
            _clockWatcher.Join(3 * 1000);
        }
    }
}