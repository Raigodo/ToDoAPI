using Microsoft.AspNetCore.Identity;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL.Interfaces;

public interface IGroupMembershipRepository
{
    Task<IEnumerable<GroupsUsersEntity>> GetAllGroupMembersAsync(GroupEntity group);
    Task<GroupsUsersEntity?> GetMemberAsync(UserEntity user, GroupEntity group);
    Task<GroupsUsersEntity> AddMemberAsync(UserEntity user, GroupEntity group, GroupMemberDto memberDto);
    Task<bool> UpdateMemberAsync(GroupsUsersEntity groupMember, GroupMemberDto memberDto);
    Task<bool> RemoveGroupMemberAsync(GroupsUsersEntity groupMember);
}
