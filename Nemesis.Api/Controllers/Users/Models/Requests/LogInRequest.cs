using System.ComponentModel.DataAnnotations;

namespace Nemesis.Api.Controllers.Users.Models.Requests;

public class LogInRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Login { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}