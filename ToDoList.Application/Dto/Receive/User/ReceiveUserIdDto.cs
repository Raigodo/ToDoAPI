using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.User;

public class ReceiveUserIdDto
{
    [Required]
    public string Id { get; set; }
}
