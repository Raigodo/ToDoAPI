using ToDoList.Domain.Dto;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity?> GetCurrentUserAsync();
    Task<UserEntity?> GetUserByIdAsync(string userId);
    Task<bool> UpdateUserAsync(UserEntity user, UserDto userDto);
    Task<bool> DeleteUserAsync(UserEntity user);
}
