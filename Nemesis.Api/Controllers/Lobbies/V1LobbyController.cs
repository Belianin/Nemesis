using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Nemesis.Api.Controllers.Lobbies.Models.Requests;
using Nemesis.Api.Controllers.Lobbies.Models.Respones;
using Nemesis.Api.Lobbies;
using Nemesis.Api.Users.Sessions;
using Nemesis.Api.WebSockets;

namespace Nemesis.Api.Controllers.Lobbies;

[ApiController]
[Route("V1/lobbies")]
public class V1LobbyController : ControllerBase
{
    private readonly LobbyService lobbyService;
    private readonly ISessionRepository sessionStore;

    public V1LobbyController(LobbyService lobbyService, ISessionRepository sessionStore)
    {
        this.lobbyService = lobbyService;
        this.sessionStore = sessionStore;
    }

    [HttpGet]
    [ProducesResponseType(typeof(LobbyResponse[]), 200)]
    public IActionResult GetLobbies()
    {
        return Ok(lobbyService.GetLobbies().Select(x => new LobbyResponse(x)));
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(LobbyResponse), 200)]
    public IActionResult CreateLobby([FromBody] CreateLobbyRequest request)
    {
        var lobby = lobbyService.Create(request.Title, User.Identity.Name);

        return Ok(new LobbyResponse(lobby));
    }
    
    [HttpGet("{lobbyId}/connect")]
    public async Task Connect([FromRoute] string lobbyId, [FromQuery] string sid, CancellationToken token)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var login = await sessionStore.GetUserLoginAsync(sid);

        var lobby = lobbyService.GetLobby(lobbyId);
        if (lobby == null || lobby.Players.Any(x => x == login))
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

        var listener = new WebSocketListener<PlayerEvent>(webSocket);

        listener.OnMessage += (sender, e) => lobby.Process(e, login);
        lobby.OnEvent += (sender, e) => webSocket.SendAsync(e);
        
        lobby.AddPlayer(login);
        
        await listener.ListenAsync(token);

        lobby.RemovePlayer(login);
    }
}