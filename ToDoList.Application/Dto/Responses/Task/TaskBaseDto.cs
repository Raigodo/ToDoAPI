using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Responses.Task;

public class TaskBaseDto
{
    [Required]
    public int Id { get; set; }
    public string Title { get; set; }
}
