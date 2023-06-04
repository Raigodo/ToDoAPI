using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

namespace ToDoList.API.DAL.Repositories;

public class GroupMembershipsRepository : IGroupMembershipRepository
{
    private ApiDbContext _dbCtx;
    private IHttpContextAccessor _httpCtxAcessor;

    public GroupMembershipsRepository(
        ApiDbContext dbCtx,
        IHttpContextAccessor httpCtxAcessor)
    {
        _dbCtx = dbCtx;
        _httpCtxAcessor = httpCtxAcessor;
    }

    public async Task<GroupsUsersEntity> AddMemberAsync(UserEntity user, GroupEntity group)
    {
        var member = new GroupsUsersEntity()
        {
            UserId = user.Id,
            GroupId = group.Id,
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
