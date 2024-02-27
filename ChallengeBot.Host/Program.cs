using ChallengeBot.Host.Components;
using ChallengeBot.Host.Services;
using Discord.Interactions;
using Discord.WebSocket;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Build configuration
var _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services
    .AddMudServices()
    .AddSingleton(_configuration)
    .AddSingleton(new DiscordSocketConfig()
    {
        GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.GuildMembers,
        AlwaysDownloadUsers = true
    })
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton(x => 
        new InteractionService(x.GetRequiredService<DiscordSocketClient>(), 
        new InteractionServiceConfig()))
    .AddSingleton<InteractionHandler>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
