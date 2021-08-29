
using IdentityServer.Services;
using IdentityServer4.Extensions;
using IndentityServer.Services;

namespace IdentityServer.Extensions;
public static class IdentityServerExtensions
{
    public static void AddIdentityServerBehindGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(o => o.IssuerUri = configuration
                .GetValue<string>("IdentityServer:IssuerUri"))
            .AddResourceStore<ResourceStoreService>()
            .AddClientStore<ClientStoreService>()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();
    }
    public static void UseIdentityServerBehindGateway(this WebApplication app, IConfiguration configuration)
    {
        app.Use((context, next) =>
        {
            context.SetIdentityServerOrigin(
                configuration.GetValue<string>("IdentityServer:IssuerUri"));

            return next();
        });

        app.UseIdentityServer();
    }
}
