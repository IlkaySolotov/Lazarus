// System
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// Lazarus
using Lazarus.Utils;
using Lazarus.Updates;
using Lazarus.Modules;
using Lazarus.Configuration;
// Telegram
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Lazarus
{
    class Program
    {
        static async Task Main() {
            StaticConfig.Bot = new TelegramBotClient(StaticConfig.Token);

            var me = await StaticConfig.Bot.GetMeAsync();
            var cts = new CancellationTokenSource();
            var handle = new Handle();

            StaticConfig.Bot.StartReceiving(new DefaultUpdateHandler(
                handle.UpdateAsync,
                handle.ErrorAsync), cts.Token);

            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = $"SpaceX: {me.Username}";
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(
                $"🚀 SpaceX: {me.Username}\n" +
                $"🆔 Api Id: {me.Id}\n");
            Console.ReadLine();

            cts.Cancel();
        }
    }
}
