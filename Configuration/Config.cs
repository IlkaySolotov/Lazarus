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
    class Config
    {
        public static async Task<bool> SaveAsync(DynamicConfig _config, string[] addittionalUser = null)
        {
            var id = _config.Chat.Id;
            try {
                await File.WriteAllTextAsync($"Chats/{id}.json",
                    JsonConvert.SerializeObject(new ChatConfiguration(
                        _config, await Users.GetChatUsersAsync(
                            id,
                            addittionalUser
                        ))
                    ));
            } catch {
                return false;
            }
            return true;
        }

        public static async Task<bool> AutoSaveAsync(DynamicConfig _config, Match match)
        {
            var id = _config.Chat.Id;
            try
            {
                //await File.WriteAllTextAsync($"Chats/{id}.json",
                //    JsonConvert.SerializeObject(new ChatConfiguration(
                //        _config, await Users.GetChatUsersAsync(
                //            id,
                //            addittionalUser
                //        ))
                //    ));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static async  Task CreateConfigurationAsync(DynamicConfig _config, Update update)
        {
            var id = _config.Chat.Id;
            var from = update.Message.From;

            var clax = new ChatConfiguration(_config, new string[][] {
                new string[] { $"{from.Id}", string.IsNullOrEmpty(from.Username) ? $"{from.FirstName} {from.LastName}" : from.Username }
            });

            await File.WriteAllTextAsync($"Chats/{id}.json",
                JsonConvert.SerializeObject(clax));
        }
    }

    public class ChatConfiguration
    {
        public string Name { get; set; }
        public string Reference { get; set; }
        public char Prefix { get; set; }
        public long Id { get; set; }
        public string[][] Users { get; set; }

        public ChatConfiguration(DynamicConfig config, string[][] users)
        {
            Name = $"{config.Chat.FirstName} {config.Chat.LastName}";
            Reference = string.IsNullOrEmpty(config.Chat.InviteLink) ? $"@{config.Chat.Username}" : config.Chat.InviteLink;
            Prefix = config.Prefix;
            Id = config.Chat.Id;
            Users = users;
        }
    }

    class StaticConfig
    {
        public static string Token { get; set; } = new string("1884301514:AAEwvF6YzqO-4vNgLzPSEEB-mkPzIaoFhhY");
        public static TelegramBotClient Bot { get; set; }
        public static List<DynamicConfig> dynamicConfigs { get; set; } = new List<DynamicConfig>();
    }

    public class DynamicConfig
    {
        public Chat Chat { get; set; }
        public char Prefix { get; set; } = '!';
        public DynamicConfig(Chat _chat)
        {
            Chat = _chat;
        }
    }
}
