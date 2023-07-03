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

    public async Task<TaskEntity> CreateTaskAsync(ReceiveTaskDto taskDto)
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

    public async Task DeleteTaskAsync(ReceiveTaskIdDto taskId)
    {
        var task = await GetTaskByIdAsync(taskId);
        _dbCtx.ApiTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
    {
        return await _dbCtx.ApiTasks.ToListAsync();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(ReceiveTaskIdDto taskId)
    {
        return await _dbCtx.ApiTasks
            .FirstOrDefaultAsync(t => t.Id == taskId.Id);
    }

    public async Task UpdateTaskAsync(ReceiveUpdateTaskDto taskDto)
    {
        var taskId = new ReceiveTaskIdDto
        {
            Id = taskDto.Id
        };
        var task = await GetTaskByIdAsync(taskId);
        task.ParrentBoxId = taskDto.ParrentBoxId;
        task.Title = taskDto.Title;
        task.Description = taskDto.Description;

        await _dbCtx.SaveChangesAsync();
    }
}
