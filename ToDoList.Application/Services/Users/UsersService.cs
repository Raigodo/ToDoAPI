using ToDoList.Application.Dto.Receive.User;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Users;

public class UsersService
{
    private IUserRepository _userRepository;
    public UsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return users;
    }

    public async Task<UserEntity> TryGetByIdAsync(string userId)
    {
        var user = await _userRepository.TryGetUserByIdAsync(userId);
        return user;
    }

    public async Task TryUpdateAsync(ReceiveUpdateUserDto userDto)
    {
        await _userRepository.TryUpdateUserAsync(userDto);
    }
    public async Task TryDeleteAsync(string userId)
    {
        await _userRepository.TryDeleteUserAsync(userId);
    }
}
