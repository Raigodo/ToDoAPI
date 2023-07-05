using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface ITaskBoxRepository
{
    Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync();
    Task<IEnumerable<TaskBoxEntity>> TryGetRootTaskBoxesAsync(int groupId);
    Task<TaskBoxEntity> TryGetTaskBoxByIdAsync(int taskBoxId);
    Task<TaskBoxEntity> TryCreateTaskBoxAsync(ReceiveTaskBoxDto taskBoxDto);
    Task TryUpdateTaskBoxAsync(ReceiveUpdateTaskBoxDto taskBoxDto);
    Task TryDeleteTaskBoxAsync(int taskBoxId);
}
