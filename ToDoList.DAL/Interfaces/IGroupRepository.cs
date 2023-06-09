using ToDoList.Domain.Dto;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Interfaces;

public interface IGroupRepository
{
    Task<IEnumerable<GroupEntity>> GetAllGroupsAsync();
    Task<IEnumerable<GroupEntity>> GetAllMemberingGroups();
    Task<GroupEntity?> GetGroupByIdAsync(int groupId);
    Task<bool> UpdateGroupAsync(GroupEntity group, GroupDto groupDto);
    Task<GroupEntity> CreateGroupAsync(GroupDto groupDto);
    Task<bool> DeleteGroupAsync(GroupEntity group);
}
