using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IGroupMembershipRepository
{
    Task<IEnumerable<GroupsUsersEntity>> TryGetAllGroupMembersAsync(int groupId);
    Task<IEnumerable<GroupEntity>> TryGetAllGroupsAsync(string userId);
    Task<GroupsUsersEntity> TryGetMemberByIdAsync(string userId, int groupId);
    Task<GroupsUsersEntity> TryAddMemberAsync(ReceiveMemberDto memberDto);
    Task TryUpdateMemberAsync(ReceiveMemberDto memberDto);
    Task TryRemoveGroupMemberAsync(string userId, int groupId);
}
