using IdentityServer4.Models;

namespace IdentityServer.Repositories.Clients;
public class InMemoryClientsRepository : IClientsRepository
{
    public Task<IEnumerable<Client>> FindClientsAsync()
    {
        var clients = new List<Client>
        {
            new Client
            {
                ClientId = "service.client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = 
                { 
                    GrantType.AuthorizationCode,
                    GrantType.ClientCredentials, 
                    GrantType.ResourceOwnerPassword,
                },
                RedirectUris =
                {
                    "http://localhost:8080"
                },                
                RequirePkce=false,
                AllowedScopes = { "openid", "profile", "invoice.read" },
            }
        };
        return Task.FromResult<IEnumerable<Client>>(clients);
    }
}
