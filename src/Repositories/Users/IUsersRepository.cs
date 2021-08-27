
namespace IdentityServer.Repositories.Users;

public interface  IUsersRepository
{
    Task<UserModel> FindUserByUserName(string userName);
    Task<UserModel> FindUserById(string subjectId);
}
