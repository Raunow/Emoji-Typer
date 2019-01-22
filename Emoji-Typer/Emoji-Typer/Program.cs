using System;
using System.Reflection;
using System.Text;
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
		private static CommandService commandService;
		private static async Task Main(string[] args)
		{
			client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Info });

			commandService = new CommandService(new CommandServiceConfig
			{
				CaseSensitiveCommands = false,
				DefaultRunMode = RunMode.Async
			});
			await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);

			client.Ready += Ready;
			client.Log += Client_Log;
			client.MessageReceived += Client_MessageReceived; ;

			string token = "";
			try
			{
				token = args[0].Trim();
			}
			catch
			{
				token = "NTM3MzAyNzQ0MTcyODU1MzE2.Dyjlvg.pok6vXFlU7cuaAUuFteudXkocT4";
			}

			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync().ConfigureAwait(false);

			await Task.Delay(-1);
		}

		private static Task Client_Log(LogMessage arg)
		{
			Console.WriteLine(arg.Message);

			return Task.CompletedTask;
		}

		private static async Task Client_MessageReceived(SocketMessage msg)
		{
			var message = msg as SocketUserMessage;

			if (string.IsNullOrWhiteSpace(message.Content) || message is null || message.Author.IsBot)
			{
				return;
			}

			int sigIndex = 0;
			if (!message.HasStringPrefix("+", ref sigIndex))
			{
				return;
			}

			await message.Channel.SendMessageAsync(Translator(message.Content.Substring(1)));
			
		}

		private static async Task Ready()
		{
			await client.SetGameAsync("I'm new!");
		}

		private static string Translator(string input)
		{
			StringBuilder reply = new StringBuilder("");
			foreach (char c in input)
			{
				if (char.IsLetter(c))
				{
					reply.Append(string.Format(":regional_indicator_{0}:", c));
				}
				else if (char.IsNumber(c))
				{
					reply.Append(GetNumberAsWord(c));
				}
				else
				{
					reply.Append(c);
				}
			}

			return reply.ToString();
		}

		private static string GetNumberAsWord(char input)
		{
			string word = "";

			switch (input)
			{
				case '1':
					word = ":one:";
					break;
				case '2':
					word = ":two:";
					break;
				case '3':
					word = ":three:";
					break;
				case '4':
					word = ":four:";
					break;
				case '5':
					word = ":five:";
					break;
				case '6':
					word = ":six:";
					break;
				case '7':
					word = ":seven:";
					break;
				case '8':
					word = ":eight:";
					break;
				case '9':
					word = ":nine:";
					break;
				case '0':
					word = ":zero:";
					break;
				default:
					break;
			}

			return word;
		}
	}
}
