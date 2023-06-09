using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto;

public class GroupDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}
