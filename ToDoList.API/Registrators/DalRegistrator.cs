using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.DAL;

namespace ToDoList.API.Registaration;

public static class DalRegistrator
{
    public static WebApplicationBuilder ConfigureDataAcess(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApiDbContext>(opt =>
            opt.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(
                    builder.Configuration.GetConnectionString("AppAssembly"))
                ));
        return builder;
    }
}
