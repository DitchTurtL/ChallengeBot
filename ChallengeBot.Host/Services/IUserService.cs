using ChallengeBot.Host.Data;
using Discord.WebSocket;

namespace ChallengeBot.Host.Services;

public interface IUserService
{
    Task<User> GetUser(SocketUser user);
}