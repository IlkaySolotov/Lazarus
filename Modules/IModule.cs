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

namespace Lazarus.Modules
{
    interface IModule
    {
        Task SendHelpAsync(Update update, DynamicConfig config);
        Task ManageConfigAsync(Update update, DynamicConfig config, ConfigActionTypeEnum ConfigActionTypeEnum, Match match);
        //Task ChangePrefixAsync(Update update, DynamicConfig config);
        //Task PromoteMemberAsync(Update update, DynamicConfig config);
        //Task MuteMemberAsync(Update update, DynamicConfig config);
        //Task BanMemberAsync(Update update, DynamicConfig config);
        //Task PurgeMemberAsync(Update update, DynamicConfig config);
        //Task PinMessageAsync(Update update, DynamicConfig config);
        //Task UnknownCommandAsync(Update update, DynamicConfig config);
        //Task LogMessageAsync(Update update, DynamicConfig config);
    }
}
