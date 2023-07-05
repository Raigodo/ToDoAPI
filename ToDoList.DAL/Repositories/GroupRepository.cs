using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Group;
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
            .ToListAsync();
    }

    public async Task<IEnumerable<GroupEntity>> TryGetAllGroupsAsync(string userId)
    {
        return await _dbCtx.ApiGroups
            .Where(g => g.MembersInGroup
                .Any(gu => gu.UserId == userId))
            .ToListAsync();
    }

    public async Task<GroupEntity> TryGetGroupByIdAsync(int groupId)
    {
        return await _dbCtx.ApiGroups
            .FirstOrDefaultAsync(g => g.Id == groupId);
    }

    public async Task<GroupEntity> TryCreateGroupAsync(ReceiveGroupDto groupDto)
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

    public async Task TryUpdateGroupAsync(ReceiveUpdateGroupDto groupDto)
    {
        var group = await TryGetGroupByIdAsync(groupDto.Id);
        group.Name = groupDto.Name;
        group.Description = groupDto.Description;

        await _dbCtx.SaveChangesAsync();
    }

    public async Task TryDeleteGroupAsync(int groupId)
    {
        var group = await TryGetGroupByIdAsync(groupId);
        _dbCtx.ApiGroups.Remove(group);
        await _dbCtx.SaveChangesAsync();
    }
}
