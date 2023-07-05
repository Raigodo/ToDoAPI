using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity> TryGetTaskByIdAsync(int taskId);
    Task<TaskEntity> TryCreateTaskAsync(ReceiveTaskDto taskDto);
    Task TryUpdateTaskAsync(ReceiveUpdateTaskDto taskDto);
    Task TryDeleteTaskAsync(int taskId);
}
