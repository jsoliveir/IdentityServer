using IdentityServer.Repositories.Clients;
using IdentityServer.Repositories.Resources;
using IdentityServer.Repositories.Users;
using IdentityServer.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Validation;
using IndentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddIdentityServer(o=> o.IssuerUri = builder.Configuration.GetValue<string>("IdentityServer:IssuerUri"))
    .AddResourceStore<ResourceStoreService>()
    .AddClientStore<ClientStoreService>()
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

builder.Services
    .AddSingleton<IResourceOwnerPasswordValidator, PasswordValidatorService>()
    .AddSingleton<IResourcesRepository, InMemoryResourcesRepository>()
    .AddSingleton<IClientsRepository, InMemoryClientsRepository>()
    .AddSingleton<IUsersRepository, InMemoryUsersRepository>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.Use((context, next) =>
{
    context.SetIdentityServerOrigin(
        builder.Configuration.GetValue<string>("IdentityServer:IssuerUri"));

    return next();
});

app.UseIdentityServer();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
