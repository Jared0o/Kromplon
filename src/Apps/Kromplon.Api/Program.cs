using Kromplon.Api.Modules;
using Kromplon.Commons.Infrastructure;
using NSwag;

var builder = WebApplication.CreateBuilder(args);
var modules = ModuleLoader.LoadModules();

ModuleLoader.RegisterModulesServices(modules, builder.Services, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(x =>
{
    x.Title = "Kromplon Api";
    x.DocumentName = "Kromplon Api V1";
    x.Version = "v1";
    x.PostProcess = d => d.Info.Title = "Kromplon Api";
    x.AddSecurity("Bearer", new ()
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = OpenApiSecuritySchemeType.ApiKey,
        In = OpenApiSecurityApiKeyLocation.Header,
    });
    x.OperationProcessors.Add(new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(x => x.DocExpansion = "list");
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", ()=> "Hello From Kromplon.Api! It's awesome api");

ModuleLoader.RegisterModules(modules, app);

app.Run();
