using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace EComm.API.Auth;

public class MyCustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MyCustomAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
: base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers.Authorization;

        if (authHeader.Count == 0 || string.IsNullOrEmpty(authHeader[0]))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authoization header missing"));
        }
        string authHeaderValue = authHeader[0]!;
        if (!authHeaderValue.StartsWith("Basic"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authentication scheme"));
        }
        string encodedApiKey = authHeaderValue.Substring(6);
        string apiKey = Encoding.UTF8.GetString(
            Convert.FromBase64String(encodedApiKey));

        if (apiKey != "abc123")
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
        }
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity([
                new Claim(ClaimTypes.Name, "SomeUser"),
                new Claim(ClaimTypes.Role, "Admin")
              ], nameof(MyCustomAuthHandler)));

        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
