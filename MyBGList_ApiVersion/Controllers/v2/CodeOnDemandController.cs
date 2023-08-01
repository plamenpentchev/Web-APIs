using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyBGList_ApiVersion.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("/v{version:apiVersion}/[controller]/[action]")]
    public class CodeOnDemandController: ControllerBase
    {
        [HttpGet(Name = "Test")]
        [ResponseCache(NoStore = true)]
        [EnableCors(PolicyName = "AnyOrigin")]
        public ContentResult Test()
        {
            return Content("<script>" +
             "window.alert('Your client supports JavaScript!" +
             "\\r\\n\\r\\n" +
             $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
             "\\r\\n" +
             "Client time (UTC): ' + new Date().toISOString());" +
             "</script>" +
             "<noscript>Your client does not support JavaScript</noscript>",
             "text/html");
        }

        [HttpGet(Name = "Test2")]
        [ResponseCache(NoStore = true)]
        [EnableCors(PolicyName = "AnyOrigin")]
        public ContentResult Test2(int? addMinutes = null)
        {
            var t = addMinutes.HasValue ? DateTime.UtcNow.AddMinutes((double)addMinutes).ToString("o") : DateTime.UtcNow.ToString("o");
            return Content($"<script>" +
             "window.alert('Your client supports JavaScript!" +
             "\\r\\n\\r\\n" +
             $"Server time (UTC): {t}" +
             "\\r\\n" +
             "Client time (UTC): ' + new Date().toISOString());" +
             "</script>" +
             "<noscript>Your client does not support JavaScript</noscript>",
             "text/html");
        }
    }
}
