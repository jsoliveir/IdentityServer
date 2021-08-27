
using IdentityServer.Repositories.Users;
using IdentityServer4.Validation;

namespace IndentityServer.Services;
public class PasswordValidatorService : IResourceOwnerPasswordValidator
{
    private readonly IUsersRepository _usersRepository;

    public PasswordValidatorService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _usersRepository.FindUserByUserName(context.Request.UserName);

        context.Result = new GrantValidationResult(
            subject: user.Id.ToString(),
            authenticationMethod:  context.Request.GrantType,
            identityProvider:"local"
        );
    }
}
