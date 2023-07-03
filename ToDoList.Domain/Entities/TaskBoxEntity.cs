using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Domain.Entities;

public class TaskBoxEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(GroupEntity))]
    public int AssociatedGroupId { get; set; }

    [ForeignKey(nameof(GroupEntity))]
    public int? ParrentBoxId { get; set; }


    public string Title { get; set; } = "New Box";


    public GroupEntity AssociatedGroup { get; set; }
    public TaskBoxEntity ParrentBox { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    public ICollection<TaskBoxEntity> SubFolders { get; set; } = new List<TaskBoxEntity>();
}
