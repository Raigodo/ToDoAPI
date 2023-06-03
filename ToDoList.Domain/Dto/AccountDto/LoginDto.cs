using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Domain.AccountDto;

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }
}
