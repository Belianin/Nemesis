using System.ComponentModel.DataAnnotations;

namespace Nemesis.Api.Controllers.Models.Requests;

public class CreateLobbyRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; }
}