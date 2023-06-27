using CardsBot.BOT;
using CardsBot.BOT.Commands;
using CardsBot.Models;
using CardsBot.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly Bot _bot;
        private readonly IEnumerable<Command> _commands;
        private readonly IMessageManager _messageManager;

        public MessageController(IUserManager userManager, Bot bot, IEnumerable<Command> commands, IMessageManager messageManager)
        {
            _userManager = userManager;
            _bot = bot;
            _commands = commands;
            _messageManager = messageManager;
        }

        [HttpPost]
        [Route("update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null)
                return Ok();

            var client = _bot.Get();
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.ChatJoinRequest)
            {
                var userId = update.ChatJoinRequest.From.Id;
                await client.ApproveChatJoinRequest(update.ChatJoinRequest.Chat.Id, userId);
                var userCheck = await _userManager.GetUserByTIDAsync(userId);
                if (userCheck == null)
                    await _userManager.AddUserAsync(new BotUser { TId = userId});
                var admissionMessage = await _messageManager.GetMessageTextAsync(MessageType.Admission);
                await client.SendTextMessageAsync(userId, admissionMessage);

            }

            var data = await GetInformationFromUpdate(update, client);
            var text = data.Item1;
            var tid = data.Item2;

            var user = await _userManager.GetUserByTIDAsync(tid);
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
                await client.SendTextMessageAsync(873841135, ex.ToString());
            }
            return Ok();
        }

        public static async Task<(string, long)> GetInformationFromUpdate(Update update, TelegramBotClient client)
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
                await client.SendTextMessageAsync(873841135, ex.ToString());
                return (text, tid);
            }
        }
    }
}
