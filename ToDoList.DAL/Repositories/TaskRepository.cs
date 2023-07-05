using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Task;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private ApiDbContext _dbCtx;

    public TaskRepository(
        ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    public async Task<TaskEntity> TryCreateTaskAsync(ReceiveTaskDto taskDto)
    {
        var task = new TaskEntity()
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            ParrentBoxId = taskDto.ParrentBoxId,
        };
        await _dbCtx.ApiTasks.AddAsync(task);
        await _dbCtx.SaveChangesAsync();

        return task;
    }

    public async Task TryDeleteTaskAsync(int taskId)
    {
        var task = await TryGetTaskByIdAsync(taskId);
        _dbCtx.ApiTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
    {
        return await _dbCtx.ApiTasks.ToListAsync();
    }

    public async Task<TaskEntity?> TryGetTaskByIdAsync(int taskId)
    {
        return await _dbCtx.ApiTasks
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task TryUpdateTaskAsync(ReceiveUpdateTaskDto taskDto)
    {
        var task = await TryGetTaskByIdAsync(taskDto.Id);
        task.ParrentBoxId = taskDto.ParrentBoxId;
        task.Title = taskDto.Title;
        task.Description = taskDto.Description;

        await _dbCtx.SaveChangesAsync();
    }
}
