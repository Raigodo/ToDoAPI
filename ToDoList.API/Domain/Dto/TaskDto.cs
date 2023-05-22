using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.Domain.Dto;

public class TaskDto
{ 
    [Required]
    public int ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }
}
