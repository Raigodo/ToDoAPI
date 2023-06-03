using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Domain.Dto;

public class UserDto
{
    [Required]
    public string Nickname { get; set; }
}
