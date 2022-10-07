using Nemesis.Api.Users.Sessions;

namespace Nemesis.Api.Users;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> users = new();

    public Task CreateUserAsync(User user)
    {
        users[user.Login] = user;

        return Task.CompletedTask;
    }

    public Task<User?> GetUserAsync(string login)
    {
        if (users.TryGetValue(login, out var user))
            return Task.FromResult<User?>(user);

        return Task.FromResult<User?>(null);
    }
}