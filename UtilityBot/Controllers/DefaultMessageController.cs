﻿using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Configuration;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class DefaultMessageController : BaseController , IDefaultMessageController
    {
        protected override string _returnMessage { get; set; } = "Unsupported format message received.";
        public DefaultMessageController(AppSettings appSettings, ISimpleLogger logger, ITelegramBotClient telegramBotClient) : base(appSettings, logger, telegramBotClient) { }
    }
}