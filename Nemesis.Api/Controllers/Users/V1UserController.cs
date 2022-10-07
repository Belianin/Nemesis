using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Nemesis.Api.Controllers.Users.Models.Requests;
using Nemesis.Api.Controllers.Users.Models.Responses;
using Nemesis.Api.Users;
using Nemesis.Api.Users.Sessions;

namespace Nemesis.Api.Controllers.Users;

[Route("v1/users")]
[ApiController]
public class V1UserController : ControllerBase
{
    private readonly ISessionRepository sessionRepository;
    private readonly IUserRepository userRepository;

    public V1UserController(IUserRepository userRepository, ISessionRepository sessionRepository)
    {
        this.userRepository = userRepository;
        this.sessionRepository = sessionRepository;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var authorizationHeader = HttpContext.Request.Headers.Authorization;

        if (authorizationHeader.Count != 1)
            return Forbid();

        var sessionId = authorizationHeader.Single();

        var login = await sessionRepository.GetUserLoginAsync(sessionId);

        if (login == null)
            return Forbid();

        var user = await userRepository.GetUserAsync(login);

        return Ok(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LogInResponse), 200)]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request)
    {
        var user = await userRepository.GetUserAsync(request.Login);

        if (user == null)
            return Forbid();

        var passwordHash = PasswordHasher.Hash(request.Password);

        if (user.PasswordHash != passwordHash)
            return Forbid();

        var sessionId = await sessionRepository.CreateSessionAsync(user.Login);

        return Ok(new LogInResponse
        {
            SessionId = sessionId
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LogInRequest request)
    {
        var existingUser = await userRepository.GetUserAsync(request.Login);

        if (existingUser == null)
            return BadRequest();

        var passwordHash = PasswordHasher.Hash(request.Password);

        var user = new User
        {
            Login = request.Login,
            PasswordHash = passwordHash
        };

        await userRepository.CreateUserAsync(user);

        return Ok();
    }
}