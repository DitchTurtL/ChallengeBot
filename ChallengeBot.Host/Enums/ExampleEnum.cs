using Discord.Interactions;

namespace ChallengeBot.Host.Enums;

public enum ExampleEnum
{
    First,
    Second,
    Third,
    Fourth,
    [ChoiceDisplay("Twenty First")]
    TwentyFirst
}
