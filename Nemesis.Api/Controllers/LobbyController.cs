using Microsoft.AspNetCore.Mvc;
using Nemesis.Api.Controllers.Models.Requests;
using Nemesis.Api.Lobbies;

namespace Nemesis.Api.Controllers;

[ApiController]
[Route("V1/lobbies")]
public class LobbyController : ControllerBase
{
    private readonly LobbyService lobbyService;

    public LobbyController(LobbyService lobbyService)
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