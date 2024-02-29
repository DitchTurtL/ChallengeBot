using ChallengeBot.Host.Data;
using ChallengeBot.Host.Data.Models;
using Dapper;
using Npgsql;

namespace ChallengeBot.Host.Services;

public class DatabaseService : IDatabaseService
{
    private readonly IConfiguration _configuration;

    public DatabaseService(IConfiguration configuration)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
    }

    private string ConnectionString => _configuration.GetConnectionString("DefaultConnection")!;

    public async Task<User?> GetUserById(long id)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
    }

    public async Task<User> CreateUser(User user)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        var query = "INSERT INTO users (id, name, username, avatar_url, created_at, joined_at) VALUES (@Id, @Name, @Username, @AvatarUrl, @CreatedAt, @JoinedAt)";
        await connection.ExecuteAsync(query, user);
        return user;
    }

    public async Task<string?> CreateNewChallenge(User user)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        var key = StringConstants.GenerateKey();
        var query = "INSERT INTO challenges (key, created_by) VALUES (@Key, @UserId) RETURNING key";
        return await connection.ExecuteScalarAsync<string>(query, new { Key = key, UserId = user.Id });
    }

    public async Task<Challenge?> GetChallengeByKey(string key)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return await connection.QueryFirstOrDefaultAsync<Challenge>("SELECT * FROM challenges WHERE key = @Key", new { Key = key });
    }

    public async Task<bool> UpdateChallenge(Challenge challenge)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        challenge.UpdatedAt = DateTime.UtcNow;
        var query = "UPDATE challenges SET title = @Title, description = @Description, stub = @Stub, content = @Content, active = @Active, updated_at = @UpdatedAt WHERE key = @Key";
        return await connection.ExecuteAsync(query, challenge) > 0;
    }

    public async Task<Challenge?> GetChallengeByStubAsync(string stub)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return await connection.QueryFirstOrDefaultAsync<Challenge>("SELECT * FROM challenges WHERE stub = @Stub", new { Stub = stub });
    }

    public async Task<List<Challenge>> GetActiveChallengesAsync()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        return (await connection.QueryAsync<Challenge>("SELECT * FROM challenges WHERE active = true")).ToList();
    }

}
