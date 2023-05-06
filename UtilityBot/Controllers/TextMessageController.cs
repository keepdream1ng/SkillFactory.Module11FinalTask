using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Configuration;
using UtilityBot.Services;
using UtilityBot.Utilities;

namespace UtilityBot.Controllers
{
    public class TextMessageController : BaseController , ITextMessageController
    {
        public TextMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient, ISimpleLogger logger,
            IStorage memoryStorage) : base(appSettings, logger, telegramBotClient, memoryStorage) { }

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
                        InlineKeyboardButton.WithCallbackData($" Count simbols in message" , $"simbolsCount"),
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
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, ModeBasedReply(message), cancellationToken: ct);
                    break;
            }
        }

        private string ModeBasedReply(Message message)
        {
            string reply = _memoryStorage.GetSession(message.Chat.Id).BotMode switch
            {
                "simbolsCount" => $"Your message has {message.Text.Length} simbols in it.",
                "sumNumbers"   => $"{(MultibleIntParser.TryParse(message.Text, out int[] numbers) ? "Succesful count:" : "Errors occured but:")}" +
                                  $" sum of numbers is {numbers.Sum()}",
                _              => "Something went wrong, please pick a mode in main menu." 
            }; 
            _logger.Log($"Generated answer: \"{reply}\"");
            return reply;
        }
    }
}
