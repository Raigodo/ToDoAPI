using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.API.DAL;

namespace ToDoList.API.Services.Check
{
    public class AcessGuardService : IAcessGuardService
    {
        private ApiDbContext _dbCtx;
        private UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor _httpCtx;

        public AcessGuardService(
            ApiDbContext appDbContext,
            UserManager<IdentityUser> userManager,
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

        public async Task<bool> IsGroupAcessibleAsync(int groupId)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return await _dbCtx.ApiGroupsUsers
                .Include(gu => gu.User)
                .AnyAsync(gu => 
                    gu.GroupId == groupId 
                    && gu.User.Id == accountId);
        }

        public async Task<bool> IsTaskAcessibleAsync(int taskId)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return await _dbCtx.ApiTasks
                .Include(t => t.ParrentBox.AssociatedGroup.MembersInGroup)
                .AnyAsync(t =>
                    t.Id == taskId
                    && t.ParrentBox.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == accountId));
        }

        public async Task<bool> IsBoxAcessibleAsync(int boxId)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return await _dbCtx.ApiTaskBoxes
                .Include(b => b.AssociatedGroup.MembersInGroup)
                .AnyAsync(b =>
                    b.Id == boxId
                    && b.AssociatedGroup.MembersInGroup.Any(gu => gu.UserId == accountId));
        }

        public async Task<bool> IsGroupMemberAcessibleAsync(string userId, int groupId)
        {
            var accountId = _userManager.GetUserId(_httpCtx.HttpContext?.User);
            return await _dbCtx.ApiGroupsUsers
                .AnyAsync(gu =>
                    gu.UserId == userId
                    && gu.GroupId == groupId);
        }
    }
}
