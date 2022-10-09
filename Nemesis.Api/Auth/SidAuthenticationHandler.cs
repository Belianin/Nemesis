using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Nemesis.Api.Users;
using Nemesis.Api.Users.Sessions;

namespace Nemesis.Api.Auth;

public class SidAuthenticationHandler : AuthenticationHandler<SidAuthenticationSchemeOptions>
{
    private ISessionRepository sessionRepository;
    private IUserRepository userRepository;

    public SidAuthenticationHandler(
        IOptionsMonitor<SidAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ISessionRepository sessionRepository,
        IUserRepository userRepository) : base(options, logger, encoder, clock)
    {
        this.sessionRepository = sessionRepository;
        this.userRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers.Authorization;

        if (authorizationHeader.Count != 1)
            return AuthenticateResult.Fail("No header");

        var sessionId = authorizationHeader.Single();

        var login = await sessionRepository.GetUserLoginAsync(sessionId);

        if (login == null)
            return AuthenticateResult.Fail("Invalid session");

        var user = await userRepository.GetUserAsync(login);

        var claims = new[] 
        {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Name, user.Login)
        };

        var claimsIdentity = new ClaimsIdentity(claims, nameof(SidAuthenticationHandler));

        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}