
namespace IdentityServer.Repositories.Users;
public class InMemoryUsersRepository : IUsersRepository
{
    private Task<UserModel> GetTestUser()
    {
        return Task.FromResult(new UserModel()
        {
            Email = "jose.oliveira@domain.com",
            Username = "jsoliveira",
            Password = "1234",
            IsActive = true,
            Id = 1234,
        });
    }
    public Task<UserModel> FindUserById(string subjectId)
    {
        return GetTestUser();
    }

    public Task<UserModel> FindUserByUserName(string username)
    {
        return GetTestUser();
    }
}
