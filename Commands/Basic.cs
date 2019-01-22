using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Emoji_Typer
{
    class Basic : ModuleBase<SocketCommandContext>
    {
        [Command("")]
        public async Task EmojiTyper([Remainder] string input)
        {

            string reply = Translator(Context.Message.Content.Substring(1));

            await ReplyAsync(reply);
        }

        private string Translator(string input)
        {
            StringBuilder reply = new StringBuilder(input);
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

        private string GetNumberAsWord(char input)
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