using Microsoft.EntityFrameworkCore;
using ToDoList.DAL;

namespace ToDoList.API.Setups;

public static class DalConfig
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
