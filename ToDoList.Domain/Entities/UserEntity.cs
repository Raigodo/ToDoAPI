using Microsoft.AspNetCore.Identity;

namespace ToDoList.Domain.Entities;

public class UserEntity : IdentityUser
{
    public string Nickname { get; set; } = "User";

    public ICollection<GroupsUsersEntity> GroupMemberships { get; set; } = new List<GroupsUsersEntity>();
}
