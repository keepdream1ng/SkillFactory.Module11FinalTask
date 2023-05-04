using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Configuration;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController : BaseController , ITextMessageController
    {
        public TextMessageController(AppSettings appSettings,ISimpleLogger logger, ITelegramBotClient telegramBotClient) : base(appSettings, logger, telegramBotClient) { }

        public override async Task HandleAsync(Message message, CancellationToken ct)
        {
            _logger.Log($"Controller {GetType().Name} get a message \"{message.Text}\".");
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Count letters in message" , $"lettersCount"),
                    });
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Count sum of numbers in message" , $"sumNumbers")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Bot can do awesome stuff!</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Pick with a button below.{Environment.NewLine}",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, _returnMessage, cancellationToken: ct);
                    break;
            }
        }
    }
}
