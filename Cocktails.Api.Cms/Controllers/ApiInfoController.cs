using Cocktails.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace Cocktails.Api.Cms.Controllers
{
    /// <summary>
    /// API Controller for ApiInfo
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class ApiInfoController : Controller
    {
        private readonly ApiInfo _info;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        public ApiInfoController(ApiInfo info)
        {
            _info = info;
        }

        /// <summary>
        /// Returns API info
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("~/")]
        public IActionResult Get() => Ok(_info);
    }
}
