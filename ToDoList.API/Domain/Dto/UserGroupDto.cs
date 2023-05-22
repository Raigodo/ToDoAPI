using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.API.Domain.Dto;

public class UserGroupDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}
