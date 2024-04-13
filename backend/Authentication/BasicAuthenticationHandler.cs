using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using art_gallery.Models;
using art_gallery.Persistence;
using BCrypt.Net;

namespace art_gallery.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly ILogger<BasicAuthenticationHandler> _logger;

        public BasicAuthenticationHandler(
           IOptionsMonitor<AuthenticationSchemeOptions> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
           IUserDataAccess userDataAccess
           
       )
           : base(options, logger, encoder, clock)
        {
            _userDataAccess = userDataAccess;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", @"Basic realm=""Access to the robot controller.""");

            var check = Context.GetEndpoint()?.Metadata.GetMetadata<IAllowAnonymous>() != null;
            if (check) return Task.FromResult(AuthenticateResult.Fail("No Metadata"));

            try
            {
                string authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
                {
                    Response.StatusCode = 401;
                    return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
                }

                string base64Credentials = authHeader["Basic ".Length..].Trim();
                byte[] credentialsBytes = Convert.FromBase64String(base64Credentials);
                string decodedCredentials = Encoding.UTF8.GetString(credentialsBytes);

                string[] credentials = decodedCredentials.Split(':');

                if (credentials.Length != 2)
                {
                    Response.StatusCode = 401;
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Credentials Format"));
                }

                string email = credentials[0];
                string password = credentials[1];

                User user = _userDataAccess.GetUsers().FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Response.StatusCode = 401;
                    return Task.FromResult(AuthenticateResult.Fail("User Not Found"));
                }

                bool pwVerificationResult = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

                if (pwVerificationResult)
                {
                    Claim[] claims = new[]
                    {
                        new Claim("name", $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Role, user.Role!)
                    };

                    ClaimsIdentity identity = new(claims, "Basic");
                    ClaimsPrincipal principal = new(identity);
                    AuthenticationTicket ticket = new(principal, Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    Response.StatusCode = 401;
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Password"));
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while handling basic authentication.");

                Response.StatusCode = 500;
                return Task.FromResult(AuthenticateResult.Fail("Internal Server Error"));
            }
        }
    }
}


