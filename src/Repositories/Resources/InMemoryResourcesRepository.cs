
using IdentityServer4.Models;

namespace IdentityServer.Repositories.Resources;
public class InMemoryResourcesRepository : IResourcesRepository
{
    public Task<IEnumerable<IdentityResource>> GetAllIdentityResourcesAsync()
    {
        var resources = new List<IdentityResource>
        {
            new IdentityResource(
                name: "openid",
                userClaims: new[] { "sub" },
                displayName: "Your user identifier"),

            new IdentityResource(
                name: "profile",
                userClaims: new[] { "username", "email", "website" },
                displayName: "Your profile data")
        };
        return Task.FromResult<IEnumerable<IdentityResource>>(resources);
    }

    public Task<IEnumerable<ApiResource>> GetAllApiResourcesAsync()
    {
        var resources = new List<ApiResource>
        {
            new ApiResource("invoice", "Invoice API")
            {
                Scopes = { "invoice.read", "invoice.write" },
            },
        };
        return Task.FromResult<IEnumerable<ApiResource>>(resources);
    }

    public Task<IEnumerable<ApiScope>> GetAllApiScopesAsync()
    {
        var scopes = new[]
        {
            new ApiScope("invoice.read"),
            new ApiScope("invoice.write"),
        };
        return Task.FromResult<IEnumerable<ApiScope>>(scopes);
    }

    public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
        IEnumerable<string> scopeNames)
    {
        var identityResources = await GetAllIdentityResourcesAsync();
        return identityResources.Where(r => scopeNames.Contains(r.Name));
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(
      IEnumerable<string> scopeNames)
    {
        var apiResources = await GetAllApiResourcesAsync();
        return apiResources.Where(r => scopeNames.Intersect(r.Scopes).Any());
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(
        IEnumerable<string> names)
    {
        var resources = await GetAllApiResourcesAsync();
        return resources.Where(s => names.Contains(s.Name));
    }

    public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(
           IEnumerable<string> scopeNames)
    {
        var apiScopes = await GetAllApiScopesAsync();
        return apiScopes.Where(s => scopeNames.Contains(s.Name));
    }


}
