using ChallengeBot.Host.Data.Models;
using Discord.WebSocket;

namespace ChallengeBot.Host.Services;

public interface IUserService
{
    Task<User> GetUser(SocketUser user);
}