using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Domain.Entities;

public class UserEntity : IdentityUser
{
    [Required]
    public string Nickname { get; set; } = "User";

    public ICollection<GroupsUsersEntity> GroupMemberships { get; set; } = new List<GroupsUsersEntity>();
}
