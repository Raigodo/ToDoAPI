using ToDoList.Application.Dto.Responses.Group;

namespace ToDoList.Application.Dto.Responses.Member;

public class MembersDto
{
    public GroupBaseDto Group { get; set; }
    public IEnumerable<MemberBaseDto> Members { get; set; }
}
