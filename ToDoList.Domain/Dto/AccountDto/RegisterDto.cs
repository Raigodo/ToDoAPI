using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto.AccountDto;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Nickname { get; set; }
}
