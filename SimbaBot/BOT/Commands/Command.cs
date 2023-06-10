using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimbaBot.BOT.Commands
{
    public abstract class Command
    {
        public readonly IUserManager _userManager;
        public readonly Bot _bot;
        public readonly IRepository<BotUser> _repository;
        public Command(IUserManager userManager, Bot bot, IRepository<BotUser> repository)
        {
            _userManager = userManager;
            _bot = bot;
            _repository = repository;
        }
        public abstract string Name { get; }

        public abstract Task ExecuteAsync(Update message, TelegramBotClient client);

        public bool Contains(string message)
        {
            return message.ToLower().Contains(Name.ToLower());
        }
    }
}