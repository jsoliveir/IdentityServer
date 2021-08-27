
using IdentityModel;
using IdentityServer.Repositories.Users;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace IndentityServer.Services;
public class ProfileService : IProfileService
{
    private readonly IUsersRepository _usersRepository;

    public ProfileService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await _usersRepository.FindUserById(subjectId);
        var properties = user.GetType().GetProperties();

        var requestedProperties = properties
            .Where(p => context.RequestedClaimTypes.Contains(p.Name.ToLower()));

        foreach (var prop in requestedProperties)
        {
            var value = prop.GetValue(user)?.ToString();
            if (value != null)
            {
                context.IssuedClaims.Add(
                   new Claim(prop.Name.ToLower(), value));
            }
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await _usersRepository.FindUserById(subjectId);
        context.IsActive = user.IsActive;
    }
}
