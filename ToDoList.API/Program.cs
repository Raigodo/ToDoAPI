using Microsoft.AspNetCore.Identity;
using ToDoList.API.Registaration;
using ToDoList.DAL;
using ToDoList.Domain.Entities;
using ToDoList.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDataAcess();

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

builder.ConfigureJwtBearer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.ConfigureSwagger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
