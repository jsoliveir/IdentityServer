
using IdentityServer.Repositories.Resources;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IndentityServer;

namespace IdentityServer.Services;
public class ResourceStoreService : IResourceStore
{
    private readonly IResourcesRepository _resourcesRepository;

    public ResourceStoreService(IResourcesRepository resourcesRepository)
    {
        _resourcesRepository = resourcesRepository;
    }

    public async Task<Resources> GetAllResourcesAsync()
    {
        var identityResources = await _resourcesRepository.GetAllIdentityResourcesAsync();
        var apiResources = await _resourcesRepository.GetAllApiResourcesAsync();
        var scopes = await _resourcesRepository.GetAllApiScopesAsync();

        return new Resources()
        {
            IdentityResources = identityResources.ToList(),
            ApiResources = apiResources.ToList(),
            ApiScopes = scopes.ToList()
        };

    }

    public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
        IEnumerable<string> scopeNames)
    {
        var resources = await _resourcesRepository
            .FindIdentityResourcesByScopeNameAsync(scopeNames);

        return resources;
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(
        IEnumerable<string> scopeNames)
    {
        var resources = await _resourcesRepository
            .FindApiResourcesByScopeNameAsync(scopeNames);

        return resources;
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(
        IEnumerable<string> apiResourceNames)
    {
        var resources = await _resourcesRepository
            .FindApiResourcesByNameAsync(apiResourceNames);
        return resources;
    }

    public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(
        IEnumerable<string> scopeNames)
    {
        var resources = await _resourcesRepository
            .FindApiScopesByNameAsync(scopeNames);

        return resources;
    }


}
