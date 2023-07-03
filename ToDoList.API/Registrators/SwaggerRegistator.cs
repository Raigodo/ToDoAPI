using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ToDoList.API.Registaration;

public static class SwaggerRegistator
{
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "bearer {token}",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        return builder;
    }
}
