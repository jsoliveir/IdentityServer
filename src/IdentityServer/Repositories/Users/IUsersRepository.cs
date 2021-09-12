
namespace IdentityServer.Repositories.Users;

public interface  IUsersRepository
{
    Task<UserModel> FindUserByUserName(string username);
    Task<UserModel> FindUserById(string subjectId);
    Task<bool> ValidateCredentials(string username, string password);
}
