using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace WOT_CW_Assistant.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetPlayerNickName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PlayerNickName");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}