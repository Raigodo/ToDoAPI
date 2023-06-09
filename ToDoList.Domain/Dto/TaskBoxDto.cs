using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.Domain.Dto;

public class TaskBoxDto
{
    [Required]
    public int AssociatedGroupId { get; set; }

    [MaybeNull]
    public int? ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }
}
