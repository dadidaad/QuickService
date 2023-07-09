using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.Utilities;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace QuickServiceWebAPI.Services.Authentication
{
    public class UserAuthenticationHandler : AuthenticationHandler<UserAuthenticationOptions>
    {
        public const string Schema = JwtBearerDefaults.AuthenticationScheme;
        private const string HeaderAuthorization = "Authorization";
        private readonly IJWTUtils _jWTUtils;
        public UserAuthenticationHandler(
            IOptionsMonitor<UserAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IJWTUtils jWTUtils)
            : base(options, logger, encoder, clock)
        {
            _jWTUtils =  jWTUtils;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderAuthorization))
            {
                return AuthenticateResult.Fail("Unauthorized - no \"" + HeaderAuthorization + "\" header found.");
            }

            // get the value of the authorization header
            string authorizationHeader = Request.Headers[HeaderAuthorization];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            // snip the schema if it is present
            if (authorizationHeader.StartsWith(Schema, StringComparison.OrdinalIgnoreCase))
            {
                authorizationHeader = authorizationHeader[Schema.Length..];
            }

            // now delegate the actual validation of the string
            try
            {
                return ValidateToken(authorizationHeader.Trim());
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        protected AuthenticateResult ValidateToken(string token)
        {
            var identity = new ClaimsIdentity(new List<Claim> { new(ClaimTypes.Name, token) }, Scheme.Name);
            var principal = new GenericPrincipal(identity, Array.Empty<string>());
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }

    public class UserAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

}
