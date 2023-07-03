using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Roles;

namespace ToDoList.DAL.Repositories;

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

    public async Task<IEnumerable<GroupEntity>> GetAllGroupsAsync()
    {
        return await _dbCtx.ApiGroups
            .Include(g => g.AcessibleBoxes)
            .Include(g => g.MembersInGroup)
            .ToListAsync();
    }

    public async Task<IEnumerable<GroupEntity>> GetAllGroupsAsync(ReceiveUserIdDto userId)
    {
        return await _dbCtx.ApiGroups
            .Include(g => g.MembersInGroup)
            .Include(g => g.AcessibleBoxes)
            .Where(g => g.MembersInGroup
                .Any(gu => gu.UserId == userId.Id))
            .ToListAsync();
    }

    public async Task<GroupEntity?> GetGroupByIdAsync(ReceiveGroupIdDto groupId)
    {
        return await _dbCtx.ApiGroups
            .Include(g => g.MembersInGroup)
            .Include(g => g.AcessibleBoxes)
            .FirstOrDefaultAsync(g => g.Id == groupId.Id);
    }

    public async Task<GroupEntity> CreateGroupAsync(ReceiveGroupDto groupDto)
    {
        var group = new GroupEntity()
        {
            Name = groupDto.Name,
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

    public async Task UpdateGroupAsync(ReceiveUpdateGroupDto groupDto)
    {
        var groupId = new ReceiveGroupIdDto
        {
            Id = groupDto.Id
        };
        var group = await GetGroupByIdAsync(groupId);
        group.Name = groupDto.Name;
        group.Description = groupDto.Description;

        await _dbCtx.SaveChangesAsync();
    }

    public async Task DeleteGroupAsync(ReceiveGroupIdDto groupId)
    {
        var group = await GetGroupByIdAsync(groupId);
        _dbCtx.ApiGroups.Remove(group);
        await _dbCtx.SaveChangesAsync();
    }
}
