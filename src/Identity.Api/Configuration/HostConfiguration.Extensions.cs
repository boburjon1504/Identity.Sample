using Identity.Application.Common.Settings;
using Identity.Application.Services;
using Identity.Infrastructure.Services;
using Identity.Persistence.DataContext;
using Identity.Persistence.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Identity.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddInfrastructures(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenGeneratorService, TokenGeneratorService>();

        return builder;

    }
    private static WebApplicationBuilder AddAuthentications(this WebApplicationBuilder builder)
    {
        var jwtToken = new JwtSettings();

        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtToken);

        builder.Services
            .AddAuthentication()
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtToken.ValidateIssuer,
                    ValidIssuer = jwtToken.ValidIssuer,
                    ValidAudience = jwtToken.ValidAudience,
                    ValidateAudience = jwtToken.ValidateAudience,
                    ValidateLifetime = jwtToken.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtToken.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken.SecretKey))
                };
            });

        return builder;
    }
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Identity Api",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }});
        });

        return builder;
    }
    private static WebApplicationBuilder AddContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        return builder;
    }
    private static WebApplication UserIdentity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
    private static WebApplication UseDevtools(this WebApplication app)
    {
        var a = app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
    private static WebApplication UseController(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
