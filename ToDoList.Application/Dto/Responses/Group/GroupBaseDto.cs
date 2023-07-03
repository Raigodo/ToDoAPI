namespace ToDoList.Application.Dto.Responses.Group;

public class GroupBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
}
