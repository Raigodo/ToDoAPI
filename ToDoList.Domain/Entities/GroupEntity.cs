using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities;

public class GroupEntity
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = "New Group";
    public string ShortDescription { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;


    public ICollection<TaskBoxEntity> AcessibleBoxes { get; set; } = new List<TaskBoxEntity>();
    public ICollection<GroupsUsersEntity> MembersInGroup { get; set; } = new List<GroupsUsersEntity>();
}
