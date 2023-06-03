using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Domain.Dto;

public class TaskBoxDto
{
    [Required]
    public int AssociatedGroupId { get; set; }

    [MaybeNull]
    public int? ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }
}
