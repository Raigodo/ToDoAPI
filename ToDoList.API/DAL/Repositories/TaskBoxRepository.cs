using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL.Repositories;

public class TaskBoxRepository : ITaskBoxRepository
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IHttpContextAccessor _httpCtxAcessor;

    public TaskBoxRepository(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager,
        IHttpContextAccessor httpCtxAcessor)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _httpCtxAcessor = httpCtxAcessor;
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
        var userId = _userManager.GetUserId(_httpCtxAcessor.HttpContext?.User);
        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b =>
                b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == userId) &&
                b.ParrentBoxId == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskBoxEntity>> GetGroupRootTaskBoxesAsync(GroupEntity group)
    {
        var userId = _userManager.GetUserId(_httpCtxAcessor.HttpContext?.User);
        return await _dbCtx.ApiTaskBoxes
            .Include(b => b.Tasks)
            .Include(b => b.SubFolders)
            .Where(b =>
                b.AssociatedGroupId == group.Id &&
                b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == userId) &&
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
