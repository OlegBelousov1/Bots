using InstaDirect.BOT;
using InstaDirect.Models;
using InstaDirect.Services.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.Services.Implementations
{
    public class SubscribeManager : ISubscribeManager
    {
        private readonly TelegramBotClient _client;
        private readonly ChannelsIds _channelsIds;

        public SubscribeManager(Bot bot, IOptions<ChannelsIds> options)
        {
            _client = bot.Get();
            _channelsIds = options.Value;
        }

        public async Task<string> GetInformationChannelLinkAsync()
        {
            var link = await _client.CreateChatInviteLinkAsync(Convert.ToInt64(_channelsIds.InformationChannel));
            return link.InviteLink;
        }

        public async Task<string> GetLiteChannelLinkAsync()
        {
            var link = await _client.CreateChatInviteLinkAsync(Convert.ToInt64(_channelsIds.LiteChannel));
            return link.InviteLink;
        }

        public async Task<string> GetWiqChannelLinkAsync()
        {
            var link = await _client.CreateChatInviteLinkAsync(Convert.ToInt64(_channelsIds.WiqChannel));
            return link.InviteLink;
        }

        public async Task<bool> IsUserInGroupAndChannelAsync(long tid)
        {
            var groupCheck = await _client.GetChatMemberAsync(Convert.ToInt64(_channelsIds.InformationChannel), tid);
            var channelCheck = await _client.GetChatMemberAsync(Convert.ToInt64(_channelsIds.WiqChannel), tid);
            return !(groupCheck.Status == ChatMemberStatus.Left) && !(groupCheck.Status == ChatMemberStatus.Kicked)
                && !(channelCheck.Status == ChatMemberStatus.Left) && !(channelCheck.Status == ChatMemberStatus.Kicked);
        }

        public async Task SendNotIfNotUserInGroupAsync(long tid)
        {
            InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithUrl("Подписаться на канал новостей ✅", await GetInformationChannelLinkAsync()),
                                         },
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithUrl("Вступить в Lite - чат 🚀", await GetLiteChannelLinkAsync()),
                                         },
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithCallbackData("Проверить", "/start"),
                                         }
            });
            await _client.SendTextMessageAsync(tid, "Добро пожаловать в бот InstaDirect 🦁\n\nЧтобы получить лицензию, подпишитесь на канал и чат и нажмите кнопочку проверки.", replyMarkup: keyboard);
        }
    }
}
