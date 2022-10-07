using Microsoft.AspNetCore.Mvc;
using Nemesis.Api.Controllers.Lobbies.Models.Requests;
using Nemesis.Api.Lobbies;

namespace Nemesis.Api.Controllers.Lobbies;

[ApiController]
[Route("V1/lobbies")]
public class V1LobbyController : ControllerBase
{
    private readonly LobbyService lobbyService;

    public V1LobbyController(LobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
    }

    [HttpGet]
    public IActionResult GetLobbies()
    {
        return Ok(lobbyService.GetLobbies());
    }

    [HttpPost]
    public IActionResult CreateLobby([FromBody] CreateLobbyRequest request)
    {
        var lobby = lobbyService.Create(request.Title);

        return Ok(lobby);
    }
    
    [HttpGet("{lobbyId}/connect")]
    public async Task Connect([FromRoute] string lobbyId, [FromQuery] string player)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }
        
        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        
        lobbyService.AddPlayer(lobbyId, player);
        // subscribe
    }
}