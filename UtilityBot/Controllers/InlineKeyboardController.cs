using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;
using UtilityBot.Configuration;

namespace UtilityBot.Controllers
{
    public class InlineKeyboardController : BaseController, IInlineKeyboardController
    {
        protected override string _returnMessage { get; set; } = "Button press detected";
        public InlineKeyboardController(AppSettings appSettings, ITelegramBotClient telegramBotClient, ISimpleLogger logger, 
            IStorage memoryStorage) : base(appSettings, logger, telegramBotClient, memoryStorage) { }

        public async Task HandleAsync(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).BotMode = callbackQuery.Data;

            // Генерим информационное сообщение
            string reply = callbackQuery.Data switch
            {
                "simbolsCount" => "Simbols in message count mode is ON",
                "sumNumbers"   => "Numbers in message sum count is ON (separate numbers with a space)",
                _              => String.Empty
            };
            _logger.Log(reply);

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>{reply}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}You can change mode in the main menu.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
