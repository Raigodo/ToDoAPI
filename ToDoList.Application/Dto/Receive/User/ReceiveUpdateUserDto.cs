using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.User;

public class ReceiveUpdateUserDto
{
    [Required]
    public string Id { get; set; }
    public string Nickname { get; set; }
}
