using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.API.Domain.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    [NotNull]
    public string Title { get; set; } = "New Task";
    public string Description { get; set; } = string.Empty;

    [Required]
    public TaskBoxEntity ParrentBox { get; set; }
}
