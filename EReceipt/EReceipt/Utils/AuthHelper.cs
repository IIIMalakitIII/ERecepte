using EReceipt.Common.Authentication;
using EReceipt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EReceipt.Utils
{
    public static class AuthHelper
    {
        public static UserData GetUserData(ICollection<Claim> claims)
        {
            var userData = new UserData
            {
                UserId = GetClaimValue(claims, CustomClaimName.Id),
                UserName = GetClaimValue(claims, CustomClaimName.Name),
                Role = GetClaimValue(claims, CustomClaimName.Role)
            };

            return userData;
        }

        private static string GetClaimValue(IEnumerable<Claim> claims, string claimName)
        {
            return claims
                .FirstOrDefault(x => x.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase))
                ?.Value;
        }
    }
}
