using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity?> GetTaskByIdAsync(int taskId);
    Task<TaskEntity> CreateTaskAsync(TaskDto taskDto);
    Task<bool> UpdateTaskAsync(TaskEntity task,TaskDto taskDto);
    Task<bool> DeleteTaskAsync(TaskEntity task);
}
