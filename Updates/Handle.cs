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

using static Lazarus.Configuration.StaticConfig;

namespace Lazarus.Updates
{
    class Handle
    {
        public async Task UpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await (update.Type switch
            {
                UpdateType.Message => MessageUpdateAsync(update),
                _ => UnknownUpdateAsync(update, true)
            });

        }

        public Task ErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task MessageUpdateAsync(Update update)
        {
            if (dynamicConfigs.Find(x => x.Chat.Id == update.Message.Chat.Id) == null)
                dynamicConfigs.Add(new DynamicConfig(update.Message.Chat));

            var config = dynamicConfigs.Find(x => x.Chat.Id == update.Message.Chat.Id);

            var type = update.Message.Type;
            await (type switch
            {
                MessageType.ChatMembersAdded => Users.AddUserComboToConfigurationAsync(update, config),
                MessageType.Text => TextMessageUpdateAsync(update, config),
                _ => UnknownMessageTypeAsync(update, true)
            });
        }

        private async Task TextMessageUpdateAsync(Update update, DynamicConfig config)
        {
            var text = update.Message.Text;
            var modules = new Module();

            await (text switch
            {
                 => Mod.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}save_config") => modules.SaveConfigAsync(update, config),
                _ when text.Contains($"{config.Prefix}prefix") => modules.ChangePrefixAsync(update, config),
                _ when text.Contains($"{config.Prefix}promote") => modules.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}mute") => modules.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}ban") => modules.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}purge") => modules.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}pin") => modules.SendHelpAsync(update, config),
                _ when text.Contains($"{config.Prefix}") => modules.UnknownCommandAsync(update, config),
                _ => modules.LogMessageAsync(update, config)
            });
        }

        private async Task UnknownMessageTypeAsync(Update update, bool output)
        {
            if (output)
                await OutputHelper.OutputErrorAsync("Unknown Message Type", new Exception($"Unknown message type. Info: {update.Message.Type}"));
        }

        private async Task UnknownUpdateAsync(Update update, bool output)
        {
            if (output)
                await OutputHelper.OutputErrorAsync("Unknown Update Type", new Exception($"Unknown update type. Info: {update.Type}"));
        }
    }
}
