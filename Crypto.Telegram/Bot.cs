using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace Crypto.Telegram
{
    public class Bot
    {
        private readonly TelegramBotClient _client;

        public Bot(string token)
        {
          //  _client = new TelegramBotClient(token);
        }
        public async Task Start()
        {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient("8040659146:AAEeJELy6WOw9PJPiEi-PIXdJpOKHzzNOVw", cancellationToken: cts.Token);
            var me = await bot.GetMe();
            bot.OnMessage += OnMessage;

            Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
            Console.ReadLine();
            cts.Cancel(); // stop the bot

            // method that handle messages received by the bot:
            async Task OnMessage(Message msg, UpdateType type)
            {
                if (msg.Text is null) return;   
                Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
                // let's echo back received text in the chat
                await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");

                if (msg.Text.StartsWith("/Price"))
                {
                    
                }
            }
        }
    }
}
