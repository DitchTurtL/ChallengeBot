using ChallengeBot.Host.Data;
using ChallengeBot.Host.Services;
using Discord;
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

    [SlashCommand("challenges", "List all active challenges")]
    public async Task Challenges()
    {
        var challenges = await _challengeService.GetActiveChallenges();

        var embed = new EmbedBuilder()
            .WithTitle("Active Challenges")
            .WithDescription("Here are all the active challenges")
            .WithColor(Color.Blue);

        foreach (var challenge in challenges)
        {
            var url = StringConstants.BASE_CHALLENGE_URL + challenge.Stub;
            var description = $"[**{challenge.Title}**]({url})\n{challenge.Description}";
            embed.AddField("\u200B", description);
        }

        var channel = Context.Channel as ITextChannel;
        if (channel != null)
            await channel.SendMessageAsync(embed: embed.Build());

    }




}
