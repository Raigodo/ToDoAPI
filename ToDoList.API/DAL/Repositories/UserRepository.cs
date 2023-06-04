using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.DAL.Interfaces;
using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Services.Check;

namespace ToDoList.API.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;
    private IHttpContextAccessor _httpCtxAcessor;

    public UserRepository(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager, 
        IHttpContextAccessor httpCtxAcessor)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
        _httpCtxAcessor = httpCtxAcessor;
    }

    public async Task<bool> DeleteUserAsync(UserEntity user)
    {
        var results = await _userManager.DeleteAsync(user);
        if (results.Succeeded)
            await _dbCtx.SaveChangesAsync();

        return results.Succeeded;
    }


    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }


    public async Task<UserEntity?> GetCurrentUserAsync()
    {
        var userId = _userManager.GetUserId(_httpCtxAcessor.HttpContext?.User);
        return await _dbCtx.Users
            .Include(u => u.GroupMemberships)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }


    public async Task<UserEntity?> GetUserByIdAsync(string userId)
    {
        return await _dbCtx.Users
            .Include(u => u.GroupMemberships)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }


    public async Task<bool> UpdateUserAsync(UserEntity user, UserDto userDto)
    {
        user.Nickname = userDto.Nickname;
        await _dbCtx.SaveChangesAsync();
        return true;
    }
}
