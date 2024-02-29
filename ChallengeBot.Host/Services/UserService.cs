using ChallengeBot.Host.Data.Models;
using Discord.WebSocket;

namespace ChallengeBot.Host.Services;

public class UserService : IUserService
{
    private readonly IDatabaseService _databaseService;
    private readonly ILogger<UserService> _logger;

    public UserService(IDatabaseService databaseService, ILogger<UserService> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }


    public async Task<User> GetUser(SocketUser user)
    {
        var dbUser = await _databaseService.GetUserById((long)user.Id);
        if (dbUser != null)
            return dbUser;

        string username;
        DateTime joinedAt = DateTime.MinValue;
        if (user is not SocketGuildUser guildUser)
            username = user.Username;
        else
        {
            username = guildUser.DisplayName;
            if (guildUser.JoinedAt.HasValue)
                joinedAt = guildUser.JoinedAt.Value.DateTime;
        }

        var newUser = new User
        {
            Id = (long)user.Id,
            Name = username,
            Username = user.Username,
            AvatarUrl = user.GetAvatarUrl(),
            CreatedAt = user.CreatedAt.DateTime,
            JoinedAt = joinedAt
        };

        _logger.LogInformation("Creating new user {Id}:{Username}", newUser.Id, username);
        return await _databaseService.CreateUser(newUser);
    }
}
