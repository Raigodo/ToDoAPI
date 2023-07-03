using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Dto.Receive.Group;

public class ReceiveUpdateGroupDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [MaxLength(100)]
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
