using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ToDoList.Domain.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(TaskBoxEntity))]
    [Required]
    public int ParrentBoxId { get; set; }

    [NotNull]
    public string Title { get; set; } = "New Task";
    public string Description { get; set; } = string.Empty;


    [JsonIgnore]
    public TaskBoxEntity ParrentBox { get; set; }
}
