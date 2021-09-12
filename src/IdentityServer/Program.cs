using IdentityServer.Extensions;
using IdentityServer.Repositories.Clients;
using IdentityServer.Repositories.Resources;
using IdentityServer.Repositories.Users;
using IdentityServer.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Validation;
using IndentityServer.Services;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddMvc();

builder.Services
    .AddIdentityServerBehindGateway(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
})
.AddCookie("Cookies");

builder.Services
    .AddSingleton<IResourceOwnerPasswordValidator, PasswordValidatorService>()
    .AddSingleton<IResourcesRepository, InMemoryResourcesRepository>()
    .AddSingleton<IClientsRepository, InMemoryClientsRepository>()
    .AddSingleton<IUsersRepository, InMemoryUsersRepository>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseIdentityServerBehindGateway(builder.Configuration);
//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
});

app.Run();
