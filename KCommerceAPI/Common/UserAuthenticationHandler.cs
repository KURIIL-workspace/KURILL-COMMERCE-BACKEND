using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Models.Json.Result;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KCommerceAPI.Common
{
    public class UserAuthenticationHandler
    {
        private readonly string securityKey;

        private readonly int expirationMiutes;

        private readonly string issuer;

        private readonly string audience;

        private readonly int noOfDaysRefreshTokenValid;

        public UserAuthenticationHandler(string securityKey, int expirationMiutes, string issuer, string audience,
                                        int noOfDaysRefreshTokenValid)
        {
            this.securityKey = securityKey;
            this.expirationMiutes = expirationMiutes;
            this.issuer = issuer;
            this.audience = audience;
            this.noOfDaysRefreshTokenValid = noOfDaysRefreshTokenValid;
        }

        public JwtTokenResultJson HandleSignInProcessAsync(EmployeeLogin userLogin, string clientIp)
        {
            var accessToken = GenerateAccessToken(userLogin.EmployeeId.Value);

            return new JwtTokenResultJson()
            {
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid().ToString(),
                NameIdentifier = userLogin.EmployeeId.ToString(),
            };
        }

        private string GenerateAccessToken(Guid userId)
        {
            var claims = new[] {
               // new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMiutes),
                signingCredentials: creds
            );

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return serializedToken;
        }

        private string GenerateAccessTokenOld(Guid userId)
        {
            var claims = new[] {
               // new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims, null,
                expires: DateTime.Now.AddMinutes(noOfDaysRefreshTokenValid), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
