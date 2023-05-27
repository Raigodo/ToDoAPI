using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using ToDoList.API.DAL;
using ToDoList.API.Domain.Entities;
using ToDoList.API.Services.Auth;
using ToDoList.API.Services.Check;
using ToDoList.API.Services.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(builder.Configuration.GetConnectionString("AppAssembly"))
    ));
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
})
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
    {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAcessGuardService, AcessGuardService>();
builder.Services.AddScoped<ICheckExistingRecordService, CheckExistingRecordService>();
builder.Services.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


await RoleSeeder.SeedAsync(app);

app.Run();
