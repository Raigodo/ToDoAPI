using ToDoList.Application.Dto.Receive.User;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity?> GetUserByIdAsync(ReceiveUserIdDto userId);
    Task UpdateUserAsync(ReceiveUpdateUserDto userDto);
    Task DeleteUserAsync(ReceiveUserIdDto userId);
}
