namespace ToDoList.API.Services.Check;

public interface ICheckExistingRecordService
{
    public Task<bool> DoesUserExistAsync(string userId);
    public Task<bool> DoesTaskExistAsync(int taskId);
    public Task<bool> DoesBoxExistAsync(int boxId);
    public Task<bool> DoesGroupExistAsync(int groupId);
    public Task<bool> DoesGroupMemberExistAsync(string userId, int groupId);
}
