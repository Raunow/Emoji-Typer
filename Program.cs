using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Emoji_Typer
{
    class Program
    {
        private static DiscordSocketClient client;
        private static IServiceProvider services;
        private static CommandService commandService;
        private static async Task Main(string[] args)
        {
            client = new DiscordSocketClient(new DiscordSocketConfig{LogLevel = LogSeverity.Info});

            commandService = new CommandService(new CommandServiceConfig{
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async
            });
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            services = new ServiceCollection().BuildServiceProvider();

            client.Ready += Ready;
            client.MessageReceived += MessageRecieved;


            await client.LoginAsync(TokenType.Bot, args[0].Trim());
            await client.StartAsync().ConfigureAwait(false);

            await Task.Delay(-1);
        }
        private static async Task Ready(){
            await client.SetGameAsync("I'm new!");
        }

        private static async Task MessageRecieved(SocketMessage msg){
            var message = msg as SocketUserMessage;

            if (string.IsNullOrWhiteSpace(message.Content) || message is null || message.Author.IsBot){
                return;
            }

            int sigIndex = 0;
            if (!message.HasCharPrefix('+', ref sigIndex)){
                return;
            }

            var context = new SocketCommandContext(client, message);

            await commandService.ExecuteAsync(context, sigIndex, services);
        }
    }
}
