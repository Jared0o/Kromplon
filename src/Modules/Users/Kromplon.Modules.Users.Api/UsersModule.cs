using Kromplon.Commons.Abstractions.Modules;
using Kromplon.Modules.Users.Api.Routes;
using Kromplon.Modules.Users.Core;
using Kromplon.Modules.Users.Infrastructure;
using Kromplon.Modules.Users.Infrastructure.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kromplon.Modules.Users.Api;

public class UsersModule : IModule
{
    public string Name => "Users";
    public string RoutePrefix => "users";
    public bool Enabled => true;

    public void RegisterEndpoints(RouteGroupBuilder app)
        => app.MapUserEndpoints();

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        => services.AddCore()
            .AddInfrastructure(configuration);

    public async Task RegisterMiddlewares(IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        await ApplyUsersMigrations.ApplyUsersMigration(serviceProvider);
    }
}