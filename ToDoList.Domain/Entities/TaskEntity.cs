using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(TaskBoxEntity))]
    public int ParrentBoxId { get; set; }

    public string Title { get; set; } = "New Task";
    public string Description { get; set; } = string.Empty;


    public TaskBoxEntity ParrentBox { get; set; }
}
