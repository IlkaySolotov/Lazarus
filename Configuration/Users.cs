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

using File = System.IO.File;

namespace Lazarus.Configuration
{
    class Users
    {
        public static async Task<string[][]> GetChatUsersAsync(long id, string[] addittionalUser = null)
        {
            var file = $"Chats/{id}.json";
            try
            {
                var result = JsonConvert.DeserializeObject<ChatConfiguration>(
                    await File.ReadAllTextAsync(file));

                var final = new List<string[]>();

                foreach (var combo in result.Users)
                    final.Add(combo);

                if (!(addittionalUser is null))
                    final.AddRange((IEnumerable<string[]>)addittionalUser.ToList());

                return final.ToArray();
            }
            catch (Exception ex)
            {
                await OutputHelper.OutputErrorAsync($"Couldn't gather an users list from the file \"{file}\"", ex);
            }
            return null;
        }

        public static async Task AddUserComboToConfigurationAsync(Update update, DynamicConfig config)
        {
            var from = update.Message.From;

            if (!File.Exists($"Chats/{update.Message.Chat.Id}.json"))
            {
                if (config == null)
                    Console.WriteLine("Gay");

                await Config.CreateConfigurationAsync(config, update);
            }
            else
            {
                await Config.SaveAsync(config, new string[] {
                    $"{from.Id}",
                    string.IsNullOrEmpty(from.Username) ? $"{from.FirstName} {from.LastName}" : from.Username
                });
            }
        }
    }
}
