using Cocktails.Common.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cocktails.Api.Controllers
{
    public class ApiInfoController : Controller
    {
        private readonly ApiInfo _info;

        public ApiInfoController(IOptions<ApiInfo> info)
        {
            _info = info.Value;
        }

        [Route("~/")]
        public IActionResult Get() => Ok(_info);
    }
}
