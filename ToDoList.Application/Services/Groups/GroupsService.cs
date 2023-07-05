using ToDoList.Application.Dto.Receive.Group;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Tasks;

public class GroupsService
{
    private IGroupRepository _groupBoxRepository;
    public GroupsService(IGroupRepository groupBoxRepository)
    {
        _groupBoxRepository = groupBoxRepository;
    }

    public async Task<GroupEntity> TryGetByIdAsync(int id)
    {
        return await _groupBoxRepository.TryGetGroupByIdAsync(id);
    }

    public async Task<GroupEntity> TryCreateAsync(ReceiveGroupDto groupDto)
    {
        return await _groupBoxRepository.TryCreateGroupAsync(groupDto);
    }

    public async Task TryUpdateAsync(ReceiveUpdateGroupDto groupDto)
    {
        await _groupBoxRepository.TryUpdateGroupAsync(groupDto);
    }

    public async Task TryDeleteAsync(int id)
    {
        await _groupBoxRepository.TryDeleteGroupAsync(id);
    }
}
