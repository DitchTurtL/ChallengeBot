
using Discord;
using Discord.WebSocket;

namespace ChallengeBot.Host.Services
{
    public class HostedClientService : IHostedService
    {
        private readonly InteractionHandler _interactionHandler;
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _configuration;

        public HostedClientService(IConfiguration configuration, InteractionHandler interactionHandler, DiscordSocketClient client)
        {
            _configuration = configuration;
            _interactionHandler = interactionHandler;
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _interactionHandler.InitializeAsync();

            await _client.LoginAsync(TokenType.Bot, _configuration["token"]);
            await _client.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
