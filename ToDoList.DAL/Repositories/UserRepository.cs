using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private ApiDbContext _dbCtx;
    private UserManager<UserEntity> _userManager;

    public UserRepository(
        ApiDbContext appDbContext,
        UserManager<UserEntity> userManager)
    {
        _dbCtx = appDbContext;
        _userManager = userManager;
    }

    public async Task TryDeleteUserAsync(string userId)
    {
        var user = await TryGetUserByIdAsync(userId);
        var results = await _userManager.DeleteAsync(user);
        if (results.Succeeded)
            await _dbCtx.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<UserEntity> TryGetUserByIdAsync(string userId)
    {
        var entity = await _dbCtx.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (entity == null) throw new NotFoundException($"User not found. Id:\"{userId}\"");
        return entity;
    }

    public async Task TryUpdateUserAsync(ReceiveUpdateUserDto userDto)
    {
        var user = await TryGetUserByIdAsync(userDto.Id);
        user.Nickname = userDto.Nickname;
        await _dbCtx.SaveChangesAsync();
    }
}
