using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using ToDoList.API.DAL;
using ToDoList.API.Services;
using ToDoList.API.Services.Check;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(builder.Configuration.GetConnectionString("AppAssembly"))
    ));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
})
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
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

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<IAcessGuardService, AcessGuardService>();
builder.Services.AddScoped<ICheckExistingRecordService, CheckExistingRecordService>();

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

app.Run();
