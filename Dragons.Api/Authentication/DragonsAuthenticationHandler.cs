using Dragons.Api.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Dragons.Api.Authentication
{
    public class DragonsAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;

        public DragonsAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userRepository)
            :base(options, logger, encoder, clock)
        { 
            _userRepository = userRepository;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Reject if there isn't an authorization header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No header found");
            }

            // Get the authorization header values
            var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            // Fail if the authorization credentials are null
            if (headerValue.Parameter == null)
            {
                return AuthenticateResult.Fail("Null authorization header");
            }

            // Convert the base64 format to regular format
            var headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
            var credentials = Encoding.UTF8.GetString(headerValueBytes);

            // If bad credentials then unauthorized
            if (string.IsNullOrEmpty(credentials))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            // Get the username and the password from the header 
            string[] credentialsArray = credentials.Split(":");
            string username = credentialsArray[0];
            string password = credentialsArray[1];

            // Check if this user exists
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (username != user.Username || password != user.Password)
            {
                return AuthenticateResult.Fail("Username password combo invalid");
            }

            // Generate ticket
            var claim = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claim, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
