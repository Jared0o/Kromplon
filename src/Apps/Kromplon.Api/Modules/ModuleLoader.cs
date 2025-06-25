using System.Reflection;
using Kromplon.Commons.Abstractions.Modules;

namespace Kromplon.Api.Modules;

internal static class ModuleLoader
{
    private const string ModuleName = "Kromplon.Modules.";

    public static IList<IModule> LoadModules()
    {
        var loadAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, ModuleName + "*.dll");
        foreach (var path in referencedPaths)
        {
            if (loadAssemblies.All(a => a.Location != path))
            {
                Assembly.LoadFrom(path);
            }
        }

        var modules = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name!.StartsWith(ModuleName, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IModule).IsAssignableFrom(t) && t is { IsAbstract: false, IsInterface: false })
            .Select(t => (IModule)Activator.CreateInstance(t)!)
            .ToList();

        return modules;
    }

    public static void RegisterModules(IEnumerable<IModule> modules, WebApplication app)
    {
        foreach (var module in modules)
        {
            if(!module.Enabled) continue;
            var group = app.MapGroup($"/{module.RoutePrefix}")
                .WithTags(module.Name);
            module.RegisterEndpoints(group);
            module.RegisterMiddlewares(app);
        }
    }
    
    public static void RegisterModulesServices(IEnumerable<IModule> modules, IServiceCollection services, IConfiguration configuration)
    {
        foreach (var module in modules)
        {
            if(!module.Enabled) continue;
            module.RegisterServices(services, configuration);
        }
    }
}