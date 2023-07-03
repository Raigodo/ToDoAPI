using ToDoList.Domain.Roles;

namespace ToDoList.Domain.Entities;

public class GroupsUsersEntity
{
    //described in db context
    public string UserId { get; set; }
    public int GroupId { get; set; }

    public string Role { get; set; } = GroupMemberRoles.Member;

    public UserEntity User { get; set; }
    public GroupEntity Group { get; set; }
}
