using ToDoList.API.Domain.AccountDto;

namespace ToDoList.API.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateTokenStringAsync(LoginDto loginDto);
    Task<bool> Login(LoginDto loginDto);
    Task<bool> Register(RegisterDto loginDto);
}