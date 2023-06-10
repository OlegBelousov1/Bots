using Microsoft.Extensions.Options;
using SimbaBot.Models;
using System;
using Telegram.Bot;

namespace SimbaBot.BOT
{
    public class Bot
    {
        private readonly BotSettings _settings;
        private TelegramBotClient _client;
        private object _lock = new object();

        public Bot(IOptions<BotSettings> options)
        {
            _settings = options.Value;
        }

        public TelegramBotClient Get()
        {
            if (_client == null)
            {
                lock(_lock)
                {
                    if (_client == null)
                    {
                        _client = new TelegramBotClient(_settings.BotToken);
                        _client.SetWebhookAsync(_settings.BaseUrl).GetAwaiter().GetResult();
                    }
                }
            }
            return _client;
        }
    }
}
