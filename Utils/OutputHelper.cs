// System
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
// Lazarus
using Lazarus.Utils;
using Lazarus.Updates;
using Lazarus.Modules;
using Lazarus.Configuration;
// Newtonsoft
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

namespace Lazarus.Utils
{
    class OutputHelper
    {
        public static async Task OutputErrorAsync(string _message, Exception _exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Error.WriteLineAsync(
                $"▶ {_message}.\n▶ Trace: {_exception.Message}.");
        }
    }
}
