using ChallengeBot.Host.Services;
using Discord.Interactions;

namespace ChallengeBot.Host.Modules;

public class ChallengeModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IUserService _userService;
    private readonly IChallengeService _challengeService;

    public ChallengeModule(IUserService userService, IChallengeService challengeService)
    {
        _userService = userService;
        _challengeService = challengeService;
    }

    [SlashCommand("new_challenge", "Create a new challenge")]
    public async Task NewChallenge()
    {
        var user = await _userService.GetUser(Context.User);
        var challengeLink = await _challengeService.CreateChallengeLink(user);

        await RespondAsync($"New Challenge created. Use this link to update it: {challengeLink}");
    }






}
