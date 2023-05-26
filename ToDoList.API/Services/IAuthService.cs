using ToDoList.API.Domain.AccountDto;

namespace ToDoList.API.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginDto loginDto);
        Task<bool> Login(LoginDto loginDto);
        Task<bool> Register(RegisterDto loginDto);
    }
}