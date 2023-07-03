using ToDoList.Application.Dto.Responses.User;

namespace ToDoList.Application.Dto.Responses.Member;

public class MemberBaseDto
{
    public UserBaseDto User { get; set; }
    public string Role { get; set; }
}
