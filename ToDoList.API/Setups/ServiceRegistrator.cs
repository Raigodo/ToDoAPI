using ToDoList.Application.Services.Tasks;
using ToDoList.Application.Services.Users;
using ToDoList.Services.Auth;

namespace ToDoList.API.Setups;

public static class ServiceRegistrator
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<GroupsService>();
        services.AddTransient<MembersService>();
        services.AddTransient<TaskBoxesService>();
        services.AddTransient<TasksService>();
        services.AddTransient<UsersService>();

        return services;
    }
}
