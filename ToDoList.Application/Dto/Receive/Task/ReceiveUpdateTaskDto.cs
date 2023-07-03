using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Task;

public class ReceiveUpdateTaskDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; } = string.Empty;
}
