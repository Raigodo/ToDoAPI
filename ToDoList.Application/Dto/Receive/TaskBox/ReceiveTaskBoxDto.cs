using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.Application.Dto.Receive.TaskBox;

public class ReceiveTaskBoxDto
{
    [Required]
    public int AssociatedGroupId { get; set; }

    [MaybeNull]
    public int? ParrentBoxId { get; set; }

    [Required]
    public string Title { get; set; }
}
