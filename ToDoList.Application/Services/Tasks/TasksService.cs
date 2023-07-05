using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Tasks;

public class TasksService
{
    private ITaskRepository _taskRepository;
    public TasksService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskEntity> TryGetByIdAsync(int id)
    {
        return await _taskRepository.TryGetTaskByIdAsync(id);
    }

    public async Task<TaskEntity> TryCreateAsync(ReceiveTaskDto taskDto)
    {
        return await _taskRepository.TryCreateTaskAsync(taskDto);
    }
    public async Task TryUpdateAsync(ReceiveUpdateTaskDto taskDto)
    {
        await _taskRepository.TryUpdateTaskAsync(taskDto);
    }
    public async Task TryDeleteAsync(int id)
    {
        await _taskRepository.TryDeleteTaskAsync(id);
    }
}
