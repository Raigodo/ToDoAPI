using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IGroupMembershipRepository
{
    Task<IEnumerable<GroupsUsersEntity>> GetAllGroupMembersAsync(ReceiveGroupIdDto groupId);
    Task<GroupsUsersEntity?> GetMemberAsync(ReceiveMemberIdDto memberId);
    Task<GroupsUsersEntity> AddMemberAsync(ReceiveMemberDto memberDto);
    Task UpdateMemberAsync(ReceiveMemberDto memberDto);
    Task RemoveGroupMemberAsync(ReceiveMemberIdDto memberId);
}
