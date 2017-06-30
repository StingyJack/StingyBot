namespace StingyBot.Common.SlashCommand
{
    using System;
    using System.Web.Http;
    using Microsoft.Owin.Hosting;
    using Owin;

    /// <summary>
    ///     Self hosts via web api. Kind of like Wcf ServiceHost, but more of a pain in the asss
    ///  (immature tooling or half-baked ideas? not sure, but this was harder to figure out than 
    ///  it should have been)
    /// 
    ///     https://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
    /// </summary>
    public class WebHookHost : IDisposable
    {
        public string ApiName { get; private set; }
        public string RouteTemplate { get; private set; }
        public string BaseWebHookAddress { get; private set; }

        public HttpConfiguration HttpConfiguration { get; private set; }
        private IDisposable _webApp;

        public WebHookHost(string webHookApiName, string webHookRouteTemplate, string baseWebHookAddress)
        {
            ApiName = webHookApiName;
            RouteTemplate = webHookRouteTemplate;
            BaseWebHookAddress = baseWebHookAddress;

            HttpConfiguration = new HttpConfiguration();
            HttpConfiguration.Routes.MapHttpRoute(
               ApiName,
               RouteTemplate
           );
        }

        public void Start()
        {
            if (_webApp != null)
            {
                _webApp.Dispose();
            }
                      
            _webApp = WebApp.Start(BaseWebHookAddress, (appBuilder) =>
            {
                appBuilder.UseWebApi(HttpConfiguration);
            });
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            _webApp?.Dispose();
        }

    }
}
