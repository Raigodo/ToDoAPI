using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface ITaskBoxRepository
{
    Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync();
    Task<IEnumerable<TaskBoxEntity>> GetRootTaskBoxesAsync(ReceiveGroupIdDto groupId);
    Task<TaskBoxEntity?> GetTaskBoxByIdAsync(ReceiveTaskBoxIdDto taskBoxId);
    Task<TaskBoxEntity> CreateTaskBoxAsync(ReceiveTaskBoxDto taskBoxDto);
    Task UpdateTaskBoxAsync(ReceiveUpdateTaskBoxDto taskBoxDto);
    Task DeleteTaskBoxAsync(ReceiveTaskBoxIdDto taskBoxId);
}
