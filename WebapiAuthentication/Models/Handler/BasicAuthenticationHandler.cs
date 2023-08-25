using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WebapiAuthentication.Models.Account;

namespace WebapiAuthentication.Models.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserCredentials _userCredentials;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, 
            UserCredentials userCredentials) : base(options, logger, encoder, clock)
        {
            _userCredentials = userCredentials;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var GetUserCredential = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
               
                var username = GetUserCredential[0];
                var password = GetUserCredential[1];

                //Now hit username and password against a data storage
                bool IsValidate = _userCredentials.ValidateUserCredentials(username, password);
                if (IsValidate)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name,username)
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal,Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }
    }
}
