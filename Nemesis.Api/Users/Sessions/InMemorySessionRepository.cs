using Nemesis.Api.Users;
using Nemesis.Api.Users.Sessions;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly ILogger<InMemorySessionRepository> logger;
    public readonly Dictionary<string, string> sessionToLogin = new();

    public InMemorySessionRepository(ILogger<InMemorySessionRepository> logger)
    {
        this.logger = logger;
    }
    
    public Task<string> CreateSessionAsync(string login)
    {
        var sessionId = SessionIdGenerator.GenerateId();

        sessionToLogin[sessionId] = login;

        return Task.FromResult(sessionId);
    }

    public Task<string?> GetUserLoginAsync(string sessionId)
    {
        if (sessionToLogin.TryGetValue(sessionId, out var userId))
            return Task.FromResult<string?>(userId);

        return Task.FromResult<string?>(null);
    }
}