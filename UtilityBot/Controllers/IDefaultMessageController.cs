using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{
    public interface IDefaultMessageController
    {
        Task HandleAsync(Message message, CancellationToken ct);
    }
}