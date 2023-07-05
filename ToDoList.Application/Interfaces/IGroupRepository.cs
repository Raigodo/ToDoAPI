using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IGroupRepository
{
    Task<IEnumerable<GroupEntity>> GetAllGroupsAsync();
    Task<GroupEntity> TryGetGroupByIdAsync(int groupId);
    Task<GroupEntity> TryCreateGroupAsync(ReceiveGroupDto groupDto);
    Task TryUpdateGroupAsync(ReceiveUpdateGroupDto groupDto);
    Task TryDeleteGroupAsync(int groupId);
}
