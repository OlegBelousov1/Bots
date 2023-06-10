using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbaBot.Models;
using Telegram.Bot.Types;
using SimbaBot.BOT;
using Telegram.Bot;
using SimbaBot.Services.Interfaces;
using SimbaBot.BOT.Commands;
using SimbaBot.Repository;

namespace SimbaBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly Bot _bot;
        private readonly IEnumerable<Command> _commands;
        private readonly IUserManager _userManager;
        private readonly IWithdrawalManager _withdrawalManager;

        public MessageController(Bot bot, IEnumerable<Command> commands, IUserManager userManager,
            IWithdrawalManager withdrawalManager)
        {
            _bot = bot;
            _commands = commands;
            _userManager = userManager;
            _withdrawalManager = withdrawalManager;
        }

        [HttpPost("update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null)
                return Ok();
            var client = _bot.Get();

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.PreCheckoutQuery)
            {
                await client.AnswerPreCheckoutQueryAsync(update.PreCheckoutQuery.Id);
                return Ok();
            }
            if (update.Message != null && update.Message.Type == Telegram.Bot.Types.Enums.MessageType.SuccessfulPayment)
            {
                var payload = update.Message.SuccessfulPayment.InvoicePayload;
                var buyer = await _userManager.GetBotUserAsync(Convert.ToInt64(payload));
                if (buyer != null)
                {
                    var inviter = await _userManager.GetBotUserAsync(buyer.Inviter);
                    if (inviter != null)
                    {
                        inviter.Balance += (decimal)(update.Message.SuccessfulPayment.TotalAmount * 0.001);
                        await _userManager.EditUserAsync(inviter);
                    }
                }
                await client.SendTextMessageAsync(payload, "Заказ успешно оплачен");
                return Ok();
            }
            var data = await GetInformationFromUpdate(update, client);
            var text = data.Item1;
            var tid = data.Item2;

            var user = await _userManager.GetBotUserAsync(tid);

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

                if (user.Status == UserStatus.ReadyOrder)
                {
                    var withdrawal = new Withdrawal()
                    { Amount = user.Balance, DateWithdrawal = DateTime.UtcNow, Description = text, Tid = tid };
                    await _withdrawalManager.AddWithdrawalAsync(withdrawal);
                    await client.SendTextMessageAsync(tid, "Ваша заявка успешно отправлена");
                    return Ok();
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
                Console.WriteLine(text);
                return (text, tid);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(873841135, ex.ToString());
                return ("", 0);
            }
        }
    }
}
