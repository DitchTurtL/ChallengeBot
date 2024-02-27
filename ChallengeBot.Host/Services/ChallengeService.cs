﻿using ChallengeBot.Host.Data;

namespace ChallengeBot.Host.Services;

public class ChallengeService : IChallengeService
{
    private readonly IDatabaseService _databaseService;

    public ChallengeService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<string?> CreateChallengeLink(User user)
    {
        var challengeId = await _databaseService.CreateNewChallenge(user);
        if (challengeId == null)
            return null;

        return StringConstants.BASE_NEW_CHALLENGE_URL + challengeId;
    }
}
