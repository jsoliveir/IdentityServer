using IdentityServer4.Models;

namespace IdentityServer.Repositories.Clients;
public class InMemoryClientsRepository : IClientsRepository
{
    public Task<Client> FindClientByIdAsync(string clientId)
    {
        var clients = new List<Client>
        {
            new Client
            {
                ClientId = "service",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = { GrantType.ClientCredentials },
                AllowedScopes = { "weather.read" },
            },
            new Client
            {
                ClientId = "customer",
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                AllowedScopes = { "openid", "profile" },
                RequireClientSecret = false
            }
        };
        return Task.FromResult(clients.First(c=>c.ClientId == clientId));
    }
}
