using IdentityServer4.Models;

namespace IdentityServer.Repositories.Clients
{
    public interface IClientsRepository
    {
        Task<IEnumerable<Client>> FindClientsAsync();
    }
}