using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.HelperServices
{
    public static class JwtService
    {
        /// <summary>
        /// Provides security key.
        /// </summary>
        /// <param name="securityKey">An available security key to be used.</param>
        /// <returns>A security key.</returns>
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            if (string.IsNullOrEmpty(securityKey))
                throw new ArgumentException("Security key cannot be null or empty.", nameof(securityKey));
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
