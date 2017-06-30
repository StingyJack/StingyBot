namespace StingyBot.Common.SlashCommand
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Microsoft.AspNet.WebHooks.Controllers;

    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebHooksController : WebHookReceiversController
    {
        public Task<IHttpActionResult> Post()
        {
            return ProcessWebHook();
        }

        private async Task<IHttpActionResult> ProcessWebHook()
        {
            var scm = SlashCommandManagerProvider.GetInstance();

            if (scm == null)
            {
                return NotFound();
            }

            try
            {
                var response = await scm.ProcessCommandAsync(RequestContext, Request);
                return ResponseMessage(response);
            }
            catch (HttpResponseException rex)
            {
                return ResponseMessage(rex.Response);
            }
        }
    }
}