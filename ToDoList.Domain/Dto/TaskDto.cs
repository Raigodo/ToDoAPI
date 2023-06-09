using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto;

public class TaskDto
{ 
    [Required]
    public int ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }
}
