using IdentityServer4.Models;

namespace IdentityServer.Repositories.Resources
{
    public interface IResourcesRepository
    {
        Task<IEnumerable<IdentityResource>> GetAllIdentityResourcesAsync();
        Task<IEnumerable<ApiResource>> GetAllApiResourcesAsync();
        Task<IEnumerable<ApiScope>> GetAllApiScopesAsync();

        Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames);
        Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames);
        Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> names);
        Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames);
    
    }
}