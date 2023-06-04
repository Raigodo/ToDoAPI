using ToDoList.API.Domain.Dto;

namespace ToDoList.API.Services.Check
{
    public interface IAcessGuardService
    {
        public Task<bool> IsUserAcessibleAsync(string userId);
        public Task<bool> IsTaskAcessibleAsync(int taskId, bool groupAdminRightRequired = false);
        public Task<bool> IsBoxAcessibleAsync(int boxId, bool groupAdminRightRequired = false);
        public Task<bool> IsGroupAcessibleAsync(int groupId, bool groupAdminRightRequired = false);
        public Task<bool> IsGroupMemberAcessibleAsync(string userId, int groupId, bool groupAdminRightRequired = false);
    }
}
