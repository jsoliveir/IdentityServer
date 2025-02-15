﻿
using IdentityServer.Repositories.Clients;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer.Services;
public class ClientStoreService : IClientStore
{
    private readonly IClientsRepository _clientsRepository;

    public ClientStoreService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<Client> FindClientByIdAsync(string clientId)
    {
        var client = await _clientsRepository.FindClientByIdAsync(clientId);
        return client;
    }
}
