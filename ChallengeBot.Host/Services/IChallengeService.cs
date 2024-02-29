using ChallengeBot.Host.Data.Models;

namespace ChallengeBot.Host.Services;

public interface IChallengeService
{
    Task<string?> CreateChallengeLink(User user);
    Task<List<Challenge>> GetActiveChallenges();
}