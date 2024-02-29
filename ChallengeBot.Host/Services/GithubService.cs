using Octokit;
using ChallengeBot.Host.Data;

namespace ChallengeBot.Host.Services;

public class GithubService
{
    private readonly IConfiguration _configuration;

    public GithubService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task GetContent(string owner, string repo, string path)
    {
        string accessToken = _configuration["github_token"] ?? "";

        var client = new GitHubClient(new Octokit.ProductHeaderValue(StringConstants.APP_NAME));
        var tokenAuth = new Credentials(accessToken);
        client.Credentials = tokenAuth;

        try
        {
            // Retrieve the contents of the file
            var fileContent = await client.Repository.Content.GetAllContentsByRef(owner, repo, path);

            // Access the content property of the returned ContentResponse object
            string decodedContent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(fileContent[0].Content));

            Console.WriteLine(decodedContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }





}
