using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToDoList.Domain.Roles;

namespace ToDoList.Domain.Entities;

public class GroupsUsersEntity
{
    //described in db context
    public string UserId { get; set; }
    public int GroupId { get; set; }

    [NotNull]
    public string Role { get; set; } = GroupMemberRoles.Member;

    [JsonIgnore]
    public UserEntity User { get; set; }
    [JsonIgnore]
    public GroupEntity Group { get; set; }
}
