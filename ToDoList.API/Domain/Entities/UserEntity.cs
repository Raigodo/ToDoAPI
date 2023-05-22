using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.API.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nickname { get; set; } = "User";

    public ICollection<GroupsUsersEntity> MemberInGroups { get; set; } = new List<GroupsUsersEntity>();
}
