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
    enum ModulesEnums
    {
        SendHelp,
        ManageConfig,
        ChangePrefix,
        PromoteMember,
        MuteMember,
        BanMember,
        PurgeMember,
        PinMessage,
        LogMessage,
        UnknownCommand,
    }

    class ModuleRegex
    {
        public ModuleRegex(DynamicConfig config)
        {
            var pfx = config.Prefix;
            Regices = new Dictionary<ModulesEnum, string[]> {{
                ModulesEnum.SendHelp, new string[] {
                    $"{pfx}help"
                }}, {
                ModulesEnum.ManageConfig, new string[] {
                    $"{pfx}config -show",
                    $"{pfx}config -save",
                    $"{pfx}config -autoSave --interval=([0-9]*)",
                }}, {
                ModulesEnum.ChangePrefix, new string[] {
                    $"{pfx}prefix",
                    $"{pfx}prefix -change=(.){{1}}"
                }}, {
                ModulesEnum.PromoteMember, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.MuteMember, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.BanMember, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.PurgeMember, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.PinMessage, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.LogMessage, new string[] {
                    $"",
                    $"",
                }}, {
                ModulesEnum.UnknownCommand, new string[] {
                    $"",
                    $"",
                }}
            };
        }

        public static Dictionary<ModulesEnum, string[]> Regices { get; set; }
    }
}