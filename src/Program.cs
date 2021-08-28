using IdentityServer.Repositories.Clients;
using IdentityServer.Repositories.Resources;
using IdentityServer.Repositories.Users;
using IdentityServer.Services;
using IdentityServer4.Validation;
using IndentityServer;
using IndentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "IdentityServer", 
        Version = "v1" 
    });
});

builder.Services
    .AddIdentityServer()
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

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
