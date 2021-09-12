
using IdentityServer.Services;
using IdentityServer4.Extensions;
using IndentityServer.Services;

namespace IdentityServer.Extensions;
public static class IdentityServerExtensions
{
    public static void AddIdentityServerBehindGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
                options.IssuerUri = configuration.GetValue<string>("IdentityServer:IssuerUri");
            })
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
