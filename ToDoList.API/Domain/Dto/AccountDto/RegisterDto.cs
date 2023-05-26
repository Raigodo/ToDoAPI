using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Domain.AccountDto;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }
}
