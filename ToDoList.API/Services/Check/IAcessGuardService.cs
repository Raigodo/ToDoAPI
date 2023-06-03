using ToDoList.API.Domain.Dto;

namespace ToDoList.API.Services.Check
{
    public interface IAcessGuardService
    {
        public Task<bool> IsUserAcessibleAsync(string userId);
        public Task<bool> IsTaskAcessibleAsync(int taskId);
        public Task<bool> IsBoxAcessibleAsync(int boxId);
        public Task<bool> IsGroupAcessibleAsync(int groupId);
        public Task<bool> IsGroupMemberAcessibleAsync(string userId, int groupId);
    }
}
