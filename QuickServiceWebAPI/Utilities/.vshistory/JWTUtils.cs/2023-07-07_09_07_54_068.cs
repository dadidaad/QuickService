using AutoMapper.Execution;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickServiceWebAPI.Utilities
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public interface IJWTUtils
    {
        public Task<string> GenerateToken(User user);
        public string? ValidateToken(string token);
    }
    public class JWTUtils : IJWTUtils
    {
        private readonly AppSettings _appSettings;
        private readonly IPermissionService _permissionService;

        public JWTUtils(IOptions<AppSettings> appSettings, IPermissionService permissionService)
        {
            _appSettings = appSettings.Value;
            _permissionService = permissionService;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            new(ClaimTypes.Role, user.Role.RoleName),
            new(type: "RoleType", value: user.Role.RoleType.ToString())
            };
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            HashSet<string> permissions = await _permissionService.GetPermissionsByRole(user.RoleId);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("userId", user.UserId.ToString()),
                    new Claim("userId", user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "userId").Value;

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
