﻿using System;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;

namespace UtilityBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            ISimpleLogger logService = new Logger();

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .ConfigureServices((hostContext, services) => services.AddSingleton(logService))
                .UseConsoleLifetime()
                .Build();


            logService.Log("Service is started.");
            await host.RunAsync();
            logService.Log("Service is stopped.");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);
            services.AddTransient<IDefaultMessageController, DefaultMessageController>();
            services.AddTransient<ITextMessageController, TextMessageController>();
            services.AddTransient<IInlineKeyboardController, InlineKeyboardController>();
            services.AddSingleton<IStorage, MemoryStorage>();

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
        static AppSettings BuildAppSettings()
        {
            string _jsonString = File.ReadAllText(Path.Combine(Extensions.DirectoryExtension.GetSolutionRoot(), "AppSettings.json"));
            return JsonSerializer.Deserialize<AppSettings>(_jsonString);
        }
    }
}