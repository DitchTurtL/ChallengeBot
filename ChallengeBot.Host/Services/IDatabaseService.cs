using ChallengeBot.Host.Data.Models;

namespace ChallengeBot.Host.Services;

public interface IDatabaseService
{
    Task<User?> GetUserById(long id);
    Task<User> CreateUser(User user);

    Task<string?> CreateNewChallenge(User user);
    Task<Challenge?> GetChallengeByKey(string key);
    Task<bool> UpdateChallenge(Challenge challenge);
    Task<Challenge?> GetChallengeByStubAsync(string stub);
    Task<List<Challenge>> GetActiveChallengesAsync();
}