using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.API.Domain.Entities;

public class TaskBoxEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(UserGroupEntity))]
    [Required]
    public int AssociatedGroupId { get; set; }

    [ForeignKey(nameof(UserGroupEntity))]
    public int? ParrentBoxId { get; set; }


    [NotNull]
    public string Title { get; set; } = "New Folder";


    public UserGroupEntity AssociatedGroup { get; set; }
    public TaskBoxEntity ParrentBox { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    public ICollection<TaskBoxEntity> SubFolders { get; set; } = new List<TaskBoxEntity>();
}
