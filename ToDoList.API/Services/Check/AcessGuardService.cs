using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.API.DAL;
using ToDoList.API.Domain.AccountDto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Domain.Roles;

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

        public bool IsUserAcessible(string userId)
        {
            var isSelfAction = userId == _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return isSelfAction;
        }

        public async Task<bool> IsGroupAcessibleAsync(int groupId, bool isAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (isAdminRightRequired)
                return await _dbCtx.ApiGroups
                    .Include(g => g.MembersInGroup)
                    .AnyAsync(g =>
                        g.Id == groupId &&
                        g.MembersInGroup.Any(gu => 
                            gu.User.Id == accountId &&
                            gu.Role == GroupMemberRoles.Admin));

            return await _dbCtx.ApiGroups
                .Include(g => g.MembersInGroup)
                .AnyAsync(gu =>
                    gu.Id == groupId &&
                    gu.MembersInGroup.Any(gu => gu.User.Id == accountId));
        }

        public async Task<bool> IsTaskAcessibleAsync(int taskId, bool isAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (isAdminRightRequired)
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
                    && t.ParrentBox.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == accountId));
        }

        public async Task<bool> IsBoxAcessibleAsync(int boxId, bool isAdminRightRequired = false)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            if (isAdminRightRequired)
                return await _dbCtx.ApiTaskBoxes
                    .Include(b => b.AssociatedGroup.MembersInGroup)
                    .AnyAsync(b =>
                        b.Id == boxId
                        && b.ParrentBox.AssociatedGroup.MembersInGroup.Any(gu =>
                            gu.UserId == accountId &&
                            gu.Role == GroupMemberRoles.Admin));

            return await _dbCtx.ApiTaskBoxes
                .Include(b => b.AssociatedGroup.MembersInGroup)
                .AnyAsync(b =>
                    b.Id == boxId
                    && b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == accountId));
        }

        public async Task<bool> IsGroupMemberAcessibleAsync(string userId, int groupId)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);

            if (userId == accountId)
                return await _dbCtx.ApiGroupsUsers
                    .AnyAsync(gu =>
                        gu.UserId == userId &&
                        gu.GroupId == groupId);

            return await _dbCtx.ApiGroupsUsers
                .AnyAsync(gu =>
                    gu.UserId == userId && 
                    gu.GroupId == groupId &&
                    gu.Role == GroupMemberRoles.Admin);
        }
    }
}
