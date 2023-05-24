using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ToDoList.API.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nickname { get; set; } = "User";

    public ICollection<GroupMemberEntity> MemberingGroups { get; set; } = new List<GroupMemberEntity>();
}
