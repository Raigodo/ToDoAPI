using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.Group;
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


    public async Task<GroupsUsersEntity> AddMemberAsync(ReceiveMemberDto memberDto)
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

    public async Task<IEnumerable<GroupsUsersEntity>> GetAllGroupMembersAsync(ReceiveGroupIdDto groupId)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == groupId.Id)
            .ToListAsync();
    }

    public async Task<GroupsUsersEntity?> GetMemberAsync(ReceiveMemberIdDto memberId)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == memberId.GroupId &&
                gu.UserId == memberId.UserId)
            .FirstOrDefaultAsync();
    }

    public async Task RemoveGroupMemberAsync(ReceiveMemberIdDto memberId)
    {
        var member = await _dbCtx.ApiGroupsUsers
            .Where(m =>
                m.UserId == memberId.UserId &&
                m.GroupId == memberId.GroupId)
            .FirstOrDefaultAsync();

        _dbCtx.ApiGroupsUsers.Remove(member);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task UpdateMemberAsync(ReceiveMemberDto memberDto)
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
