using System.Runtime.CompilerServices;

namespace Identity.Api.Configuration;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddInfrastructures()
            .AddContext()
            .AddAuthentications()
            .AddDevTools();
        return new(builder);
    }
    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UserIdentity()
            .UseDevtools()
            .UseController();
        return new(app);
    }
}
