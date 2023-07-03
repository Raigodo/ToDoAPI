using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

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

    public async Task DeleteUserAsync(ReceiveUserIdDto userId)
    {
        var user = await GetUserByIdAsync(userId);
        var results = await _userManager.DeleteAsync(user);
        if (results.Succeeded)
            await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<UserEntity?> GetUserByIdAsync(ReceiveUserIdDto userId)
    {
        return await _dbCtx.Users
            .Include(u => u.GroupMemberships)
            .FirstOrDefaultAsync(u => u.Id == userId.Id);
    }

    public async Task UpdateUserAsync(ReceiveUpdateUserDto userDto)
    {
        var userId = new ReceiveUserIdDto
        {
            Id = userDto.Id
        };
        var user = await GetUserByIdAsync(userId);
        user.Nickname = userDto.Nickname;
        await _dbCtx.SaveChangesAsync();
    }
}
