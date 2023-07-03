using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.User;

public class ReceiveLoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
