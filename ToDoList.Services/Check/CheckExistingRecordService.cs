using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;

namespace ToDoList.Services.Check;

public class CheckExistingRecordService : ICheckExistingRecordService
{
    private ApiDbContext _dbCtx;

    public CheckExistingRecordService(ApiDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    public async Task<bool> DoesBoxExistAsync(int boxId)
    {
        return await _dbCtx.ApiTaskBoxes.AnyAsync(b => b.Id == boxId);
    }

    public async Task<bool> DoesGroupExistAsync(int groupId)
    {
        return await _dbCtx.ApiGroups.AnyAsync(g => g.Id == groupId);
    }

    public async Task<bool> DoesGroupMemberExistAsync(string userId, int groupId)
    {
        return await _dbCtx.ApiGroupsUsers.AnyAsync(gu => gu.UserId == userId && gu.GroupId == groupId);
    }
        
    public async Task<bool> DoesTaskExistAsync(int taskId)
    {
        return await _dbCtx.ApiTasks.AnyAsync(t => t.Id == taskId);
    }

    public async Task<bool> DoesUserExistAsync(string userId)
    {
        return await _dbCtx.Users.AnyAsync(u => u.Id == userId);
    }
}
