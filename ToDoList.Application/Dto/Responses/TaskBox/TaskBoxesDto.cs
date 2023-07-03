namespace ToDoList.Application.Dto.Responses.TaskBox;

public class TaskBoxesDto
{
    public int? ParrentBoxId { get; set; }
    public IEnumerable<TaskBoxBaseDto> TaskBoxes { get; set; }
}
