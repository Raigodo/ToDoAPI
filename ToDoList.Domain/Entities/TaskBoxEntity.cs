using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ToDoList.Domain.Entities;

public class TaskBoxEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(GroupEntity))]
    [Required]
    public int AssociatedGroupId { get; set; }

    [ForeignKey(nameof(GroupEntity))]
    public int? ParrentBoxId { get; set; }


    [NotNull]
    public string Title { get; set; } = "New Folder";


    [JsonIgnore]
    public GroupEntity AssociatedGroup { get; set; }
    [JsonIgnore]
    public TaskBoxEntity ParrentBox { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    public ICollection<TaskBoxEntity> SubFolders { get; set; } = new List<TaskBoxEntity>();
}
