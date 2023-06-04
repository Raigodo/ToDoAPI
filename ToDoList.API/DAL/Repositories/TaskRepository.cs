using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Services.Check;

namespace ToDoList.API.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private ApiDbContext _dbCtx;

    public TaskRepository(
        ApiDbContext appDbContext)
    {
        _dbCtx = appDbContext;
    }

    public async Task<TaskEntity> CreateTaskAsync(TaskDto taskDto)
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

    public async Task<bool> DeleteTaskAsync(TaskEntity task)
    {
        _dbCtx.ApiTasks.Remove(task);
        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
    {
        return await _dbCtx.ApiTasks.ToListAsync();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(int taskId)
    {
        return await _dbCtx.ApiTasks
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<bool> UpdateTaskAsync(TaskEntity task, TaskDto taskDto)
    {
        task.ParrentBoxId = taskDto.ParrentBoxId;
        task.Title = taskDto.Title;
        task.Description = taskDto.Description;

        await _dbCtx.SaveChangesAsync();
        return true;
    }
}
