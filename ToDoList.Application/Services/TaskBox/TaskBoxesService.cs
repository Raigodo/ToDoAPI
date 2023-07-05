using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Tasks;

public class TaskBoxesService
{
    private ITaskBoxRepository _taskBoxRepository;
    public TaskBoxesService(ITaskBoxRepository taskBoxRepository)
    {
        _taskBoxRepository = taskBoxRepository;
    }

    public async Task<IEnumerable<TaskBoxEntity>> TryGetRootAsync(int groupId)
    {
        return await _taskBoxRepository.TryGetRootTaskBoxesAsync(groupId);
    }

    public async Task<TaskBoxEntity> TryGetByIdAsync(int id)
    {
        return await _taskBoxRepository.TryGetTaskBoxByIdAsync(id);
    }

    public async Task<TaskBoxEntity> TryCreateAsync(ReceiveTaskBoxDto taskBoxDto)
    {
        return await _taskBoxRepository.TryCreateTaskBoxAsync(taskBoxDto);
    }
    public async Task TryUpdateAsync(ReceiveUpdateTaskBoxDto taskBoxDto)
    {
        await _taskBoxRepository.TryUpdateTaskBoxAsync(taskBoxDto);
    }
    public async Task TryDeleteAsync(int id)
    {
        await _taskBoxRepository.TryDeleteTaskBoxAsync(id);
    }
}
