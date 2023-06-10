using SimbaBot.BOT;
using SimbaBot.BOT.Commands;
using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.BOT.Commands
{
    public class HelloCommand : Command
    {
        private readonly IKeyboardManager _keyboardManager;
        public HelloCommand(IUserManager userManager, Bot bot, IRepository<BotUser> repository, IKeyboardManager keyboardManager)
            : base(userManager, bot, repository)
        {
            _keyboardManager = keyboardManager;
        }

        public override string Name => "/Start";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var user = await _userManager.GetBotUserAsync(message.Message.From.Id);
            if (user == null)
            {
                user = new BotUser();
                user.FirstDate = DateTime.Now;
                user.Name = message.Message.From.Username;
                user.TId = message.Message.From.Id;
                user.Status = UserStatus.Default;
                if (message.Message.Text.Split(' ').Length == 2)
                {
                    long.TryParse(message.Message.Text.Split(' ')[1], out var tid);
                    var inviter = await _userManager.GetBotUserAsync(tid);
                    if (inviter != null)
                        user.Inviter = tid;
                }
                await _repository.AddAsync(user);
                await _repository.SaveChangesAsync();
            }
            var keyboard = _keyboardManager.GetMainKeyboard();
            await client.SendTextMessageAsync(user.TId, "Привет!", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: keyboard);
        }
    }
}