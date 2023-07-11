using InstaDirect.BOT;
using InstaDirect.BOT.Commands;
using InstaDirect.Models;
using InstaDirect.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly Bot _bot;
        private readonly IEnumerable<Command> _commands;
        private readonly ChannelsIds _channelsIds;

        public MessageController(IUserManager userManager, Bot bot, IEnumerable<Command> commands, IOptions<ChannelsIds> options)
        {
            _userManager = userManager;
            _bot = bot;
            _commands = commands;
            _channelsIds = options.Value;
        }

        [HttpPost]
        [Route("update")]
        public async Task<OkResult> UpdateAsync([FromBody] Update update)
        {
            if (update == null)
                return Ok();

            var client = _bot.Get();
            var data = await GetInformationFromUpdateAsync(update, client);
            var text = data.Item1;
            var tid = data.Item2;

            var user = await _userManager.GetUserByTIdAsync(tid);
            if (user != null && user.Banned)
            {
                await client.SendTextMessageAsync(tid, "Вы забанены");
                return Ok();
            }

            try
            {
                foreach (var command in _commands)
                {
                    if (command.Contains(text))
                    {
                        await command.ExecuteAsync(update, client);
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(Convert.ToInt64(_channelsIds.DeveloperTid), ex.ToString());
            }
            return Ok();
        }

        private async Task<(string, long)> GetInformationFromUpdateAsync(Update update, TelegramBotClient client)
        {
            string text = "";
            long tid = 0;

            try
            {
                if (update.CallbackQuery?.Data != null || update.Message?.Text != null)
                {
                    text = update.CallbackQuery == null ? update.Message.Text : update.CallbackQuery.Data;
                    tid = update.CallbackQuery == null ? update.Message.From.Id : update.CallbackQuery.From.Id;
                }
                return (text, tid);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(Convert.ToInt64(_channelsIds.DeveloperTid), ex.ToString());
                return (text, tid);
            }
        }
    }
}
