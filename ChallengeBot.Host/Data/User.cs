namespace ChallengeBot.Host.Data;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime JoinedAt { get; set; }
}
