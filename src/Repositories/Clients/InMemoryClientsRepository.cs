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
                ClientId = "service.weather",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = { GrantType.ClientCredentials },
                AllowedScopes = { "weather.read" },
            }
        };
        return Task.FromResult<IEnumerable<Client>>(clients);
    }
}
