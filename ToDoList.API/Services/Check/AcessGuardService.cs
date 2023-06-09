using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Roles;

namespace ToDoList.API.Services.Check
{
    public class AcessGuardService : IAcessGuardService
    {
        private ApiDbContext _dbCtx;
        private UserManager<UserEntity> _userManager;
        private IHttpContextAccessor _httpCtx;

        public AcessGuardService(
            ApiDbContext appDbContext,
            UserManager<UserEntity> userManager,
            IHttpContextAccessor httpCtx)
        {
            _dbCtx = appDbContext;
            _userManager = userManager;
            _httpCtx = httpCtx;
        }

        public async Task<bool> IsUserAcessibleAsync(string userId)
        {
            var isSelfAction = userId == _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return isSelfAction || await IsUserApiAdmin();
        }

        public async Task<bool> IsGroupAcessibleAsync(int groupId, bool groupAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (await IsUserApiAdmin())
                return true;

            if (groupAdminRightRequired)
                return await _dbCtx.ApiGroups
                .Include(g => g.MembersInGroup)
                .AnyAsync(gu =>
                    gu.Id == groupId &&
                    gu.MembersInGroup.Any(gu => 
                        gu.User.Id == accountId &&
                        gu.Role == GroupMemberRoles.Admin));

            return await _dbCtx.ApiGroups
                .Include(g => g.MembersInGroup)
                .AnyAsync(gu =>
                    gu.Id == groupId &&
                    gu.MembersInGroup.Any(gu => 
                        gu.User.Id == accountId));
        }

        public async Task<bool> IsTaskAcessibleAsync(int taskId, bool groupAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (await IsUserApiAdmin())
                return true;

            if (groupAdminRightRequired)
                return await _dbCtx.ApiTasks
                    .Include(t => t.ParrentBox.AssociatedGroup.MembersInGroup)
                    .AnyAsync(t =>
                        t.Id == taskId
                        && t.ParrentBox.AssociatedGroup.MembersInGroup.Any(gu => 
                            gu.UserId == accountId &&
                            gu.Role == GroupMemberRoles.Admin));

            return await _dbCtx.ApiTasks
                .Include(t => t.ParrentBox.AssociatedGroup.MembersInGroup)
                .AnyAsync(t =>
                    t.Id == taskId
                    && t.ParrentBox.AssociatedGroup.MembersInGroup.Any(gu => 
                        gu.UserId == accountId));
        }

        public async Task<bool> IsBoxAcessibleAsync(int boxId, bool groupAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (await IsUserApiAdmin())
                return true;

            if (groupAdminRightRequired)
                return await _dbCtx.ApiTaskBoxes
                    .Include(b => b.AssociatedGroup.MembersInGroup)
                    .AnyAsync(b =>
                        b.Id == boxId
                        && b.AssociatedGroup.MembersInGroup.Any(gu => 
                            gu.UserId == accountId &&
                            gu.Role == GroupMemberRoles.Admin));

            return await _dbCtx.ApiTaskBoxes
                .Include(b => b.AssociatedGroup.MembersInGroup)
                .AnyAsync(b =>
                    b.Id == boxId
                    && b.AssociatedGroup.MembersInGroup.Any(gu => 
                        gu.UserId == accountId));
        }

        public async Task<bool> IsGroupMemberAcessibleAsync(string targetUserId, int targetGroupId, bool groupAdminRightRequired = false)
        {
            var userId = _userManager.GetUserId(_httpCtx.HttpContext?.User);

            if (userId == targetUserId || await IsUserApiAdmin())
                return true;

            if (groupAdminRightRequired)
                return await _dbCtx.ApiGroupsUsers
                    .AnyAsync(gu =>
                        gu.UserId == targetUserId &&
                        gu.GroupId == targetGroupId &&
                        gu.Role == GroupMemberRoles.Admin);

            return await _dbCtx.ApiGroupsUsers
                .AnyAsync(gu =>
                    gu.UserId == targetUserId && 
                    gu.GroupId == targetGroupId);
        }

        private async Task<bool> IsUserApiAdmin()
        {
            var user = await _userManager.GetUserAsync(_httpCtx.HttpContext?.User);
            return await _userManager.IsInRoleAsync(user, ApiUserRoles.Admin);
        }
    }
}
