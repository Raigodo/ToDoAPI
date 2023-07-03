using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Group;
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


    public async Task<TaskBoxEntity> CreateTaskBoxAsync(ReceiveTaskBoxDto taskBoxDto)
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

    public async Task DeleteTaskBoxAsync(ReceiveTaskBoxIdDto taskBoxId)
    {
        var taskBox = await GetTaskBoxByIdAsync(taskBoxId);
        _dbCtx.ApiTaskBoxes.Remove(taskBox);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync()
    {
        return await _dbCtx.ApiTaskBoxes.ToListAsync();
    }


    public async Task<IEnumerable<TaskBoxEntity>> GetRootTaskBoxesAsync(ReceiveGroupIdDto groupId)
    {
        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b =>
                b.AssociatedGroupId == groupId.Id &&
                b.ParrentBoxId == null)
            .ToListAsync();
    }

    public async Task<TaskBoxEntity?> GetTaskBoxByIdAsync(ReceiveTaskBoxIdDto taskBoxId)
    {
        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .FirstOrDefaultAsync(b => b.Id == taskBoxId.Id);
    }

    public async Task UpdateTaskBoxAsync(ReceiveUpdateTaskBoxDto taskBoxDto)
    {
        var taskBoxId = new ReceiveTaskBoxIdDto
        {
            Id = taskBoxDto.Id
        };
        var taskBox = await GetTaskBoxByIdAsync(taskBoxId);
        taskBox.Title = taskBoxDto.Title;
        taskBox.AssociatedGroupId = taskBoxDto.AssociatedGroupId;
        taskBox.ParrentBoxId = taskBoxDto.ParrentBoxId;

        await _dbCtx.SaveChangesAsync();
    }
}
