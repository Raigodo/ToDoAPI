using ToDoList.Application.Dto.Receive.User;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity> TryGetUserByIdAsync(string userId);
    Task TryUpdateUserAsync(ReceiveUpdateUserDto userDto);
    Task TryDeleteUserAsync(string userId);
}
