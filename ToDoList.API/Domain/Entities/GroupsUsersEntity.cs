using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.API.Domain.Entities;

public class GroupsUsersEntity
{
    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }
    [ForeignKey(nameof(GroupEntity))]
    public int GroupId { get; set; }

    public UserEntity User { get; set; }
    public GroupEntity Group { get; set; }
}
