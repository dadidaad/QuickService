using AutoMapper.Execution;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickServiceWebAPI.Helpers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickServiceWebAPI.Utilities
{
    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }
    }

    public interface IJWTUtils
    {
        public Task<string> GenerateToken(User user);
        public List<Claim>? ValidateToken(string token);
    }
    public class JWTUtils : IJWTUtils
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IPermissionService _permissionService;

        public JWTUtils(IOptions<JwtOptions> jwtOptions, IPermissionService permissionService)
        {
            _jwtOptions = jwtOptions.Value;
            _permissionService = permissionService;
        }

        public async Task<string> GenerateToken(User user)
        {
            if(user == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            new(ClaimTypes.Role, user.Role.RoleName),
            new(CustomClaims.RoleType, user.Role.RoleType.ToString())
            };

            HashSet<string> permissions = await GetPermissionsName(user.RoleId);
            if (permissions.Any())
            {
                foreach (string permission in permissions)
                {
                    claims.Add(new(CustomClaims.Permissions, permission));
                }
            }
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
            var signingCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(1),
            signingCredentials);
            var tokenValue = tokenHandler.WriteToken(token);
            return tokenValue;
        }

        private async Task<HashSet<string>> GetPermissionsName(string roleId)
        {
            List<Permission> permissions = await _permissionService.GetPermissionsByRole(roleId);
            return permissions.Select(p => p.PermissionName).ToHashSet();
        }

        public List<Claim> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
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
                var claims = jwtToken.Claims.ToList();

                // return user claims from JWT token if validation successful
                return claims;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
