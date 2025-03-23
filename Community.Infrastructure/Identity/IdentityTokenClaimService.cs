using Microsoft.AspNetCore.Identity;
using Community.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Community.Core.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Community.Core.Exceptions;

namespace Community.Infrastructure.Identity
{
    public class IdentityTokenClaimService(UserManager<ApplicationUser> userManager) : ITokenClaimsService
    {
        public async Task<string> GetTokenAsync(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);

            var user = await userManager.FindByNameAsync(userName);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            claims.Add(new Claim(ClaimTypes.Role, user?.Email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
