using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.Domain.Entities;

public class GroupEntity
{
    [Key]
    public int Id { get; set; }

    [NotNull]
    public string Title { get; set; } = "New Group";

    [NotNull]
    public string Description { get; set; } = string.Empty;


    public ICollection<TaskBoxEntity> AcessibleBoxes { get; set; } = new List<TaskBoxEntity>();
    public ICollection<GroupsUsersEntity> MembersInGroup { get; set; } = new List<GroupsUsersEntity>();
}
