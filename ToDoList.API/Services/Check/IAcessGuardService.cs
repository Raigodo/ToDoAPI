using ToDoList.API.Domain.Dto;

namespace ToDoList.API.Services.Check
{
    public interface IAcessGuardService
    {
        public bool IsUserAcessible(string userId);

        public Task<bool> IsTaskAcessibleAsync(int taskId, bool isAdminRightRequired = false);
        public Task<bool> IsBoxAcessibleAsync(int boxId, bool isAdminRightRequired = false);
        public Task<bool> IsGroupAcessibleAsync(int groupId, bool isAdminRightRequired = false);
        public Task<bool> IsGroupMemberAcessibleAsync(string userId, int groupId);
    }
}
