using ChallengeBot.Host.Data;

namespace ChallengeBot.Host.Services;

public interface IChallengeService
{
    Task<string?> CreateChallengeLink(User user);
    Task<List<Challenge>> GetActiveChallenges();
}