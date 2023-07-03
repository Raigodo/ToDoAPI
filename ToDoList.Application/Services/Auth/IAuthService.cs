using ToDoList.Application.Dto.Receive.User;

namespace ToDoList.Services.Auth;

public interface IAuthService
{
    Task<string> GenerateTokenStringAsync(ReceiveLoginDto loginDto);
    Task<bool> Login(ReceiveLoginDto loginDto);
    Task<bool> Register(ReceiveRegisterDto loginDto);
}