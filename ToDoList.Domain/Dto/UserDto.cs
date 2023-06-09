using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto;

public class UserDto
{
    [Required]
    public string Nickname { get; set; }
}
