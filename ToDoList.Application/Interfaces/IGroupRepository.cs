using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IGroupRepository
{
    Task<IEnumerable<GroupEntity>> GetAllGroupsAsync();
    Task<IEnumerable<GroupEntity>> GetAllGroupsAsync(ReceiveUserIdDto userId);
    Task<GroupEntity?> GetGroupByIdAsync(ReceiveGroupIdDto groupId);
    Task<GroupEntity> CreateGroupAsync(ReceiveGroupDto groupDto);
    Task UpdateGroupAsync(ReceiveUpdateGroupDto groupDto);
    Task DeleteGroupAsync(ReceiveGroupIdDto groupId);
}
