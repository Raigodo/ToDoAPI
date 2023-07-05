using ToDoList.Application.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.API.Setups;

public static class RepositoryRegistrator
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IGroupMembershipRepository, GroupMembershipsRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<ITaskBoxRepository, TaskBoxRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}
