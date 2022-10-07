namespace Nemesis.Api.Users;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string login);
    Task CreateUserAsync(User user);
}

