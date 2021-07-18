// System
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
// Alias
using File = System.IO.File;

namespace Lazarus.Messages
{
    class ReplyMessage
    {
        public static string ShowHelpCommandAsync(long id, char prefix) {
            return $"🆔 **BotId**: `{id}`\n🈂 **Prefix**: `{prefix}`".Replace(".", "\\.");
        }

        public static async Task<string> ShowJsonConfigurationAsync(bool success, string file) {
            if (success)
                return $"⚙️ Heres the json configuration for this group.\n{ await File.ReadAllTextAsync(file) }".Replace(".", "\\.");
            return $"🤕 No json configuration file found for this group!";
        }

        public static string SaveJsonConfigurationAsync(bool success, char prefix) {
            if (success)
                return $"✅ **Group configuration updated successfully.\nMode**: `Manual`\n**Config**: `{prefix}config`".Replace(".", "\\.");
            return "🤕 **Failed to manually update Group configuration!**";
        }

        public static string AutoSaveJsonConfigurationAsync(bool success, char prefix) {
            if (success)
                return $"✅ **Group configuration updated successfully.\nMode**: `Automatic`\n**Config**: `{prefix}config`".Replace(".", "\\.");
            return "🤕 **Failed to auto update Group configuration!**";
        }
    }
}
