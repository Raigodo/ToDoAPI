using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity?> GetTaskByIdAsync(ReceiveTaskIdDto taskId);
    Task<TaskEntity> CreateTaskAsync(ReceiveTaskDto taskDto);
    Task UpdateTaskAsync(ReceiveUpdateTaskDto taskDto);
    Task DeleteTaskAsync(ReceiveTaskIdDto taskId);
}
