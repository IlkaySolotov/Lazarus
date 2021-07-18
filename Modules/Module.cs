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
using Lazarus.Enums;
using Lazarus.Updates;
using Lazarus.Modules;
using Lazarus.Messages;
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

using static Lazarus.Configuration.StaticConfig;
using File = System.IO.File;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Lazarus.Modules
{
    class Module : IModule
    {
        public async Task SendHelpAsync(Update update, DynamicConfig config) {
            await Bot.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: ReplyMessage.ShowHelpCommandAsync(Bot.BotId.Value, config.Prefix),
                parseMode: ParseMode.MarkdownV2
            );
        }

        public async Task ManageConfigAsync(Update update, DynamicConfig config,
            ConfigActionTypeEnum ConfigActionTypeEnum, Match match) {

            var id = update.Message.Chat.Id;
            var file = $"Chats/{id}.json";
            await (ConfigActionTypeEnum switch
            {
                ConfigActionTypeEnum.Show =>
                    Bot.SendTextMessageAsync(
                        chatId: id,
                        text: await ReplyMessage.ShowJsonConfigurationAsync(
                            File.Exists(file),
                            file
                        ),
                        parseMode: ParseMode.MarkdownV2
                    ),
                ConfigActionTypeEnum.Save =>
                    Bot.SendTextMessageAsync(
                        chatId: id,
                        text: ReplyMessage.SaveJsonConfigurationAsync(
                            await Config.SaveAsync(config),
                            config.Prefix
                        ),
                        parseMode: ParseMode.MarkdownV2
                    ),
                ConfigActionTypeEnum.ManageAutoSave =>
                    Bot.SendTextMessageAsync(
                        chatId: id,
                        text: ReplyMessage.AutoSaveJsonConfigurationAsync(
                            await Config.AutoSaveAsync(config, match),
                            config.Prefix
                        ),
                        parseMode: ParseMode.MarkdownV2
                    ),
                _ => throw new NotImplementedException(),
            });
        }

        //public static async Task ChangePrefixAsync(Update update, DynamicConfig config)
        //{

        //}

        //public static async Task PromoteMemberAsync(Update update, DynamicConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task MuteMemberAsync(Update update, DynamicConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task BanMemberAsync(Update update, DynamicConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task PurgeMemberAsync(Update update, DynamicConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task PinMessageAsync(Update update, DynamicConfig config)
        //{
        //    throw new NotImplementedException();
        //}

        //public static async Task UnknownCommandAsync(Update update, DynamicConfig config)
        //{
        //    var chatId = update.Message.Chat.Id;
        //    await Bot.SendTextMessageAsync(chatId, $"❓ Unknown Command, check {config.Prefix}help".Replace(".", "\\."));
        //}

        //public static async Task LogMessageAsync(Update update, DynamicConfig config)
        //{
        //    await Console.Error.WriteLineAsync(
        //        $"\nMessage Log - {DateTime.Now}\n" +
        //        $"👤 {update.Message.From.FirstName} {update.Message.From.LastName}\n" +
        //        $"💬 {update.Message.Text}\n");
        //}
    }
}
