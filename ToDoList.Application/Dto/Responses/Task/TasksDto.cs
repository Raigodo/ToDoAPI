namespace ToDoList.Application.Dto.Responses.Task;

public class TasksDto
{
    public int ParrentBoxId { get; set; }
    public IEnumerable<TaskBaseDto> Tasks { get; set; }
}
