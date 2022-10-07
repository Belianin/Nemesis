namespace Nemesis.Api.Users.Sessions;

public interface ISessionRepository
{
    Task<string?> GetUserLoginAsync(string sessionId);
    Task<string> CreateSessionAsync(string login);
}