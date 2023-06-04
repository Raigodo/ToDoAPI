using ToDoList.API.Domain.Dto;
using ToDoList.API.Domain.Entities;

namespace ToDoList.API.DAL.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity?> GetCurrentUserAsync();
    Task<UserEntity?> GetUserByIdAsync(string userId);
    Task<bool> UpdateUserAsync(UserEntity user, UserDto userDto);
    Task<bool> DeleteUserAsync(UserEntity user);
}
