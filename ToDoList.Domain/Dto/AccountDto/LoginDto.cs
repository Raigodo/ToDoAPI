using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto.AccountDto;

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }
}
