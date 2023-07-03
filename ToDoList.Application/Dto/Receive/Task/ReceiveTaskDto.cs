using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Task;

public class ReceiveTaskDto
{
    [Required]
    public int ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; } = string.Empty;
}
