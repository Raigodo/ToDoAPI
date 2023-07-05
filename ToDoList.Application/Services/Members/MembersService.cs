using ToDoList.Application.Dto.Receive.Member;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Tasks;

public class MembersService
{
    private IGroupMembershipRepository _memberRepository;
    public MembersService(IGroupMembershipRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<GroupsUsersEntity>> TryGetMembersAsync(int groupId)
    {
        return await _memberRepository.TryGetAllGroupMembersAsync(groupId);
    }
    public async Task<IEnumerable<GroupEntity>> TryGetGroupsAsync(string userId)
    {
        return await _memberRepository.TryGetAllGroupsAsync(userId);
    }

    public async Task<GroupsUsersEntity> TryGetByIdAsync(string userId, int groupId)
    {
        return await _memberRepository.TryGetMemberByIdAsync(userId, groupId);
    }

    public async Task<GroupsUsersEntity> TryCreateAsync(ReceiveMemberDto memberDto)
    {
        return await _memberRepository.TryAddMemberAsync(memberDto);
    }
    public async Task TryUpdateAsync(ReceiveMemberDto memberDto)
    {
        await _memberRepository.TryUpdateMemberAsync(memberDto);
    }
    public async Task TryDeleteAsync(string userId, int groupId)
    {
        await _memberRepository.TryRemoveGroupMemberAsync(userId, groupId);
    }
}
