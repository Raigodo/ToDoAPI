using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Dto;
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

    public async Task<TaskBoxEntity> CreateTaskBoxAsync(TaskBoxDto taskBoxDto)
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

    public async Task<bool> DeleteTaskBoxAsync(TaskBoxEntity taskBox)
    {
        _dbCtx.ApiTaskBoxes.Remove(taskBox);
        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetAllTaskBoxesAsync()
    {
        return await _dbCtx.ApiTaskBoxes.ToListAsync();
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetGroupRootTaskBoxesAsync()
    {
        var currentUser = await _userRepository.GetCurrentUserAsync();
        if (currentUser == null)
            return new List<TaskBoxEntity>();

        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b =>
                b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == currentUser.Id) &&
                b.ParrentBoxId == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetGroupRootTaskBoxesAsync(GroupEntity group)
    {
        var currentUser = await _userRepository.GetCurrentUserAsync();
        if (currentUser == null)
            return new List<TaskBoxEntity>();

        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b =>
                b.AssociatedGroupId == group.Id &&
                b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == currentUser.Id) &&
                b.ParrentBoxId == null)
            .ToListAsync();
    }

    public async Task<TaskBoxEntity?> GetTaskBoxByIdAsync(int boxId)
    {
        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .FirstOrDefaultAsync(b => b.Id == boxId);
    }

    public async Task<bool> UpdateTaskBoxAsync(TaskBoxEntity taskBox, TaskBoxDto taskBoxDto)
    {
        taskBox.Title = taskBoxDto.Title;
        taskBox.AssociatedGroupId = taskBoxDto.AssociatedGroupId;
        taskBox.ParrentBoxId = taskBoxDto.ParrentBoxId;

        await _dbCtx.SaveChangesAsync();
        return true;
    }
}
