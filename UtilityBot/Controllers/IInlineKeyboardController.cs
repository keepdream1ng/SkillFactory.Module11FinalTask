using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{
    public interface IInlineKeyboardController
    {
        Task HandleAsync(CallbackQuery? callbackQuery, CancellationToken ct);
    }
}