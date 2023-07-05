using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.TaskBox;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

public class TaskBoxRepository : ITaskBoxRepository
{
    private ApiDbContext _dbCtx;
    private IUserRepository _userRepository;

    public TaskBoxRepository(
        ApiDbContext appDbContext,
        IUserRepository userRepository)
    {
        _dbCtx = appDbContext;
        _userRepository = userRepository;
    }


    public async Task<TaskBoxEntity> TryCreateTaskBoxAsync(ReceiveTaskBoxDto taskBoxDto)
    {
        var taskBox = new TaskBoxEntity()
        {
            AssociatedGroupId = taskBoxDto.AssociatedGroupId,
            ParrentBoxId = taskBoxDto.ParrentBoxId,
            Title = taskBoxDto.Title,
        };
        await _dbCtx.ApiTaskBoxes.AddAsync(taskBox);
        await _dbCtx.SaveChangesAsync();
        return taskBox;
    }

    public async Task TryDeleteTaskBoxAsync(int taskBoxId)
    {
        var taskBox = await TryGetTaskBoxByIdAsync(taskBoxId);
        _dbCtx.ApiTaskBoxes.Remove(taskBox);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync()
    {
        return await _dbCtx.ApiTaskBoxes.ToListAsync();
    }


    public async Task<IEnumerable<TaskBoxEntity>> TryGetRootTaskBoxesAsync(int groupId)
    {
        return await _dbCtx.ApiTaskBoxes
            .Where(b =>
                b.AssociatedGroupId == groupId &&
                b.ParrentBoxId == null)
            .ToListAsync();
    }

    public async Task<TaskBoxEntity> TryGetTaskBoxByIdAsync(int taskBoxId)
    {
        return await _dbCtx.ApiTaskBoxes
            .FirstOrDefaultAsync(b => b.Id == taskBoxId);
    }

    public async Task TryUpdateTaskBoxAsync(ReceiveUpdateTaskBoxDto taskBoxDto)
    {
        var taskBox = await TryGetTaskBoxByIdAsync(taskBoxDto.Id);
        taskBox.Title = taskBoxDto.Title;
        taskBox.AssociatedGroupId = taskBoxDto.AssociatedGroupId;
        taskBox.ParrentBoxId = taskBoxDto.ParrentBoxId;

        await _dbCtx.SaveChangesAsync();
    }
}
