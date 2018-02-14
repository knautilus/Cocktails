using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cocktails.Identity.Api.Controllers
{
    /// <summary>
    /// API Controller for Profiles
    /// </summary>
    [Route("v{version:apiVersion}")]
    [ApiVersion("1")]
    public class ProfilesController : Controller
    {
    }
}
