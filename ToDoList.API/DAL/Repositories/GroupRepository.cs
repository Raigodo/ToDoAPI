using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.DAL.Repositories;

public class GroupRepository : IGroupRepository
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IHttpContextAccessor _httpCtxAcessor;

    public GroupRepository(
        ApiDbContext dbCtx,
        UserManager<UserEntity> userManager,
        IHttpContextAccessor httpCtxAcessor)
    {
        _dbCtx = dbCtx;
        _userManager = userManager;
        _httpCtxAcessor = httpCtxAcessor;
    }

    public async Task<GroupEntity> CreateGroupAsync(GroupDto groupDto)
    {
        var group = new GroupEntity()
        {
            Title = groupDto.Title,
            Description = groupDto.Description
        };
        await _dbCtx.ApiGroups.AddAsync(group);
        await _dbCtx.SaveChangesAsync();

        var groupMember = new GroupsUsersEntity
        {
            UserId = _userManager.GetUserId(_httpCtxAcessor.HttpContext?.User),
            GroupId = group.Id,
            Role = GroupMemberRoles.Admin,
        };
        await _dbCtx.ApiGroupsUsers.AddAsync(groupMember);
        await _dbCtx.SaveChangesAsync();

        return group;
    }

    public async Task<bool> UpdateGroupAsync(GroupEntity group, GroupDto groupDto)
    {
        group.Title = groupDto.Title;
        group.Description = groupDto.Description;

        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteGroupAsync(GroupEntity group)
    {
        _dbCtx.ApiGroups.Remove(group);
        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GroupEntity>> GetAllGroupsAsync()
    {
        return await _dbCtx.ApiGroups
            .Include(g=>g.AcessibleBoxes)
            .Include(g => g.MembersInGroup)
            .ToListAsync();
    }

    public async Task<GroupEntity?> GetGroupByIdAsync(int groupId)
    {
        return  await _dbCtx.ApiGroups
            .Include(g => g.MembersInGroup)
            .Include(g => g.AcessibleBoxes)
            //TODO include subfolders and tasks
            .FirstOrDefaultAsync(g => g.Id == groupId);
    }

    public async Task<IEnumerable<GroupEntity>> GetAllMemberingGroups()
    {
        var userId = _userManager.GetUserId(_httpCtxAcessor.HttpContext?.User);
        return await _dbCtx.ApiGroups
            .Include(g => g.MembersInGroup)
            .Include(g => g.AcessibleBoxes)
            .Where(g => g.MembersInGroup
                .Any(gu => gu.UserId == userId))
            .ToListAsync();
    }
}
