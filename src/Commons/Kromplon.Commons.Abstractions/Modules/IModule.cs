using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kromplon.Commons.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    string RoutePrefix { get; }
    bool Enabled { get; }
    void RegisterEndpoints(RouteGroupBuilder app);
    void RegisterServices(IServiceCollection services, IConfiguration configuration);
    Task RegisterMiddlewares(IApplicationBuilder app);
}