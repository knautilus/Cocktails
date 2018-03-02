using System;
using System.Security.Claims;

namespace Cocktails.Api.Common
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user.FindFirstValue("Id");
            if (string.IsNullOrWhiteSpace(idClaim))
            {
                idClaim = "0";
            }
            var userId = Convert.ToInt64(idClaim);
            return userId;
        }
    }
}
