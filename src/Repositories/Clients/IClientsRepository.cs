using IdentityServer4.Models;

namespace IdentityServer.Repositories.Clients
{
    public interface IClientsRepository
    {
        Task<Client> FindClientByIdAsync(string clientId);
    }
}