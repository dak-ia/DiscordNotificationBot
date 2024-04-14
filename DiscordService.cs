using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordNotificationBot
{
    public class DiscordService
    {
        public async Task<string?> GetChannelNameAsync(string token, ulong channelId)
        {
            if (token.Length != 0)
            {
                DiscordSocketClient client = new DiscordSocketClient();
                try
                {
                    await client.LoginAsync(TokenType.Bot, token);
                    await client.StartAsync();
                    var channel = await client.GetChannelAsync(channelId) as IMessageChannel;
                    return channel?.Name;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
            return null;
        }

        public async Task SendMessageAsync(string token, ulong channelId, string message)
        {
            if (token.Length != 0 && message.Length != 0)
            {
                DiscordSocketClient client = new DiscordSocketClient();
                try
                {
                    await client.LoginAsync(TokenType.Bot, token);
                    await client.StartAsync();
                    IMessageChannel? channel = await client.GetChannelAsync(channelId) as IMessageChannel;
                    channel?.SendMessageAsync(message);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
        }

        public async Task CommandReceiveAsync(string token)
        {
            if (token.Length != 0)
            {
                try
                {
                    DiscordSocketConfig config = new DiscordSocketConfig
                    {
                        GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
                    };
                    DiscordSocketClient client = new DiscordSocketClient(config);
                    CommandService commandService = new CommandService();
                    ServiceProvider serviceProvider = new ServiceCollection().BuildServiceProvider();

                    client.MessageReceived += async (SocketMessage message) =>
                    {
                        SocketUserMessage? receivedMessage = message as SocketUserMessage;
                        if (receivedMessage == null || receivedMessage.Author.IsBot) return;
                        int argPos = 0;
                        if (receivedMessage.HasStringPrefix("/", ref argPos))
                        {
                            SocketCommandContext context = new SocketCommandContext(client, receivedMessage);
                            IResult result = await commandService.ExecuteAsync(context, argPos, serviceProvider);
                        }
                        else if (receivedMessage.HasMentionPrefix(client.CurrentUser, ref argPos))
                        {
                            SocketCommandContext context = new SocketCommandContext(client, receivedMessage);
                            IResult result = await commandService.ExecuteAsync(context, argPos, serviceProvider);
                            if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
                        }
                    };

                    await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
                    await client.LoginAsync(TokenType.Bot, token);
                    await client.StartAsync();

                    await Task.Delay(-1);

                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
        }
    }
}
