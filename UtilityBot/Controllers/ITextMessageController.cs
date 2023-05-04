using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{
    public interface ITextMessageController
    {
        Task HandleAsync(Message message, CancellationToken ct);
    }
}
