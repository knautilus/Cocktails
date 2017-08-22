using Cocktails.Common.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cocktails.Api.Controllers
{
    /// <summary>
    /// API Controller for ApiInfo
    /// </summary>
    public class ApiInfoController : Controller
    {
        private readonly ApiInfo _info;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        public ApiInfoController(IOptions<ApiInfo> info)
        {
            _info = info.Value;
        }

        /// <summary>
        /// Returns API info
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("~/")]
        public IActionResult Get() => Ok(_info);
    }
}
