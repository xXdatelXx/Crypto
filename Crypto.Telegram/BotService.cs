using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Crypto.Telegram
{
    public class BotService : BackgroundService
    {
        private readonly Bot _bot;

        public BotService(Bot bot)
        {
            _bot = bot;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bot.Start();
        }
    }
}
