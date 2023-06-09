using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Dto;
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

    public async Task<GroupsUsersEntity> AddMemberAsync(UserEntity user, GroupEntity group, GroupMemberDto memberDto)
    {
        var member = new GroupsUsersEntity()
        {
            UserId = user.Id,
            GroupId = group.Id,
            Role = memberDto.Role,
        };
        await _dbCtx.ApiGroupsUsers.AddAsync(member);
        await _dbCtx.SaveChangesAsync();
        return member;
    }

    public async Task<IEnumerable<GroupsUsersEntity>> GetAllGroupMembersAsync(GroupEntity group)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == group.Id)
            .ToListAsync();
    }

    public async Task<GroupsUsersEntity?> GetMemberAsync(UserEntity user, GroupEntity group)
    {
        return await _dbCtx.ApiGroupsUsers
            .Where(gu => gu.GroupId == group.Id &&
                gu.UserId == user.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveGroupMemberAsync(GroupsUsersEntity groupMember)
    {
        _dbCtx.ApiGroupsUsers.Remove(groupMember);
        await _dbCtx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateMemberAsync(GroupsUsersEntity groupMember, GroupMemberDto memberDto)
    {
        groupMember.Role = memberDto.Role;
        await _dbCtx.SaveChangesAsync();

        return true;
    }
}
