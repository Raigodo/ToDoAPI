using ToDoList.Domain.Dto.AccountDto;

namespace ToDoList.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateTokenStringAsync(LoginDto loginDto);
    Task<bool> Login(LoginDto loginDto);
    Task<bool> Register(RegisterDto loginDto);
}