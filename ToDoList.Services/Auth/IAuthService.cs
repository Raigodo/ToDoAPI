using ToDoList.Domain.Dto.AccountDto;

namespace ToDoList.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateTokenStringAsync(ReceiveLoginDto loginDto);
    Task<bool> Login(ReceiveLoginDto loginDto);
    Task<bool> Register(RegisterRequestDto loginDto);
}