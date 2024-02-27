
namespace ChallengeBot.Host.Data;

public class StringConstants
{
    public const string BASE_URL = "https://localhost:7084";
    public static readonly string BASE_CHALLENGE_URL = $"{BASE_URL}/challenges/";
    public static readonly string BASE_NEW_CHALLENGE_URL = $"{BASE_URL}/edit/";

    internal static string GenerateKey()
    {
        return Ulid.NewUlid().ToString();
    }
}
