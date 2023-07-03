using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.User;

public class ReceiveRegisterDto
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
