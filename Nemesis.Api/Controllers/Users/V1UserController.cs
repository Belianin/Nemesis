using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nemesis.Api.Auth;
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
    [Authorize(AuthenticationSchemes = AuthConsts.Scheme)]
    [ProducesResponseType(typeof(UserResponse), 200)]
    public async Task<IActionResult> GetMe()
    {
        var user = await userRepository.GetUserAsync(User.Identity.Name);

        return Ok(new UserResponse
        {
            Login = user.Login
        });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LogInResponse), 200)]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request)
    {
        var user = await userRepository.GetUserAsync(request.Login);

        if (user == null)
            return StatusCode(403);

        var passwordHash = PasswordHasher.Hash(request.Password);

        if (user.PasswordHash != passwordHash)
            return StatusCode(403);

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

        if (existingUser != null)
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

    [HttpGet("sessions")]
    public async Task<IActionResult> GetSessions()
    {
        var sessions = ((InMemorySessionRepository) sessionRepository).sessionToLogin;

        return Ok(sessions);
    }
}