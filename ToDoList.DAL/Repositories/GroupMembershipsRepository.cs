using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

public class GroupMembershipsRepository : IGroupMembershipRepository
{
    private ApiDbContext _dbCtx;

    public GroupMembershipsRepository(
        ApiDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }


    public async Task<GroupsUsersEntity> TryAddMemberAsync(ReceiveMemberDto memberDto)
    {
        var member = new GroupsUsersEntity()
        {
            UserId = memberDto.UserId,
            GroupId = memberDto.GroupId,
            Role = memberDto.Role,
        };
        await _dbCtx.ApiGroupsUsers.AddAsync(member);
        await _dbCtx.SaveChangesAsync();
        return member;
    }

    public async Task<IEnumerable<GroupsUsersEntity>> TryGetAllGroupMembersAsync(int groupId)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == groupId)
            .ToListAsync();
    }

    public Task<IEnumerable<GroupEntity>> TryGetAllGroupsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<GroupsUsersEntity> TryGetMemberByIdAsync(string userId, int groupId)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == groupId &&
                gu.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task TryRemoveGroupMemberAsync(string userId, int groupId)
    {
        var member = await _dbCtx.ApiGroupsUsers
            .Where(m =>
                m.UserId == userId &&
                m.GroupId == groupId)
            .FirstOrDefaultAsync();

        _dbCtx.ApiGroupsUsers.Remove(member);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task TryUpdateMemberAsync(ReceiveMemberDto memberDto)
    {
        var member = await _dbCtx.ApiGroupsUsers
            .Where(m =>
                m.UserId == memberDto.UserId &&
                m.GroupId == memberDto.GroupId)
            .FirstOrDefaultAsync();

        member.Role = memberDto.Role;
        await _dbCtx.SaveChangesAsync();
    }
}
