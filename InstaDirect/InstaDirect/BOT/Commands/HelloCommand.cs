using InstaDirect.Models;
using InstaDirect.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.BOT.Commands
{
    public class HelloCommand : Command
    {
        private readonly IUserManager _userManager;
        private readonly IRefferalManager _refferalManager;
        private readonly ISubscribeManager _subscribeManager;
        private readonly IKeyboardManager _keyboardManager;
        private readonly ITextManager _textManager;

        public HelloCommand(IUserManager userManager, IRefferalManager refferalManager, ISubscribeManager subscribeManager,
            IKeyboardManager keyboardManager, ITextManager textManager)
        {
            _userManager = userManager;
            _refferalManager = refferalManager;
            _subscribeManager = subscribeManager;
            _keyboardManager = keyboardManager;
            _textManager = textManager;
        }

        public override string Name => "/start";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var userTid = message.CallbackQuery == null ? message.Message.From.Id : message.CallbackQuery.From.Id;
            var user = await _userManager.GetUserByTIdAsync(userTid);
            if (user == null)
            {
                var name = message.Message.From.Username == null ? message.Message.From.FirstName : message.Message.From.Username;
                if (message.Message.Text.Split(' ').Length == 2)
                {
                    long tid = Convert.ToInt64(message.Message.Text.Split(' ')[1]);
                    var checkUser = _userManager.GetUserByTIdAsync(tid);
                    if (checkUser != null)
                    {
                        try
                        {
                            await client.SendTextMessageAsync(tid, $"По Ваше реферальной ссылке приглашен {name}");
                        }
                        catch (Exception)
                        {
                        }
                        if (await _refferalManager.CheckForАvailabilityRefferalAsync(userTid, tid))
                        {
                            var refferal = new RefferalsInfo() { InviterTId = tid, JoinTime = DateTime.Now, RefferalTId = userTid };
                            await _refferalManager.AddRefferalAsync(refferal);
                        }
                    }
                }

                user = new BotUser()
                {
                    FirstDate = DateTime.Now,
                    LastTake = DateTime.Now.AddDays(-1),
                    Name = name,
                    TId = userTid
                };
                await _userManager.AddUserAsync(user);
            }
            if(!await _subscribeManager.IsUserInGroupAndChannelAsync(userTid))
                {
                await _subscribeManager.SendNotIfNotUserInGroupAsync(userTid);
                return;
            }

            var keyboard = await _keyboardManager.GetMainKeyboardAsync();
            var startText = await _textManager.GetStartTextAsync();

            await client.SendTextMessageAsync(userTid, startText.StartText, replyMarkup: keyboard);
        }
    }
}
