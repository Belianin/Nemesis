using System.ComponentModel.DataAnnotations;

namespace Nemesis.Api.Controllers.Lobbies.Models.Requests;

public class CreateLobbyRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; }
}