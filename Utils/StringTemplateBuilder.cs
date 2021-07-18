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

namespace Lazarus.Utils
{
    class StringTemplateBuilder
    {
        public static string Template { get; set; }
        public static string[] Replacements { get; set; }
        public StringTemplateBuilder(string template, params string[] replacements)
        {
            Template = template;
            Replacements = replacements;
        }

        public async Task<String> Build()
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (Template.Contains($"<{i}>"))
                {
                    try
                    {
                        Template = Template.Replace($"<{i}>", Replacements[i]);
                    }
                    catch (IndexOutOfRangeException Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        await Console.Error.WriteLineAsync(
                            $"▶ Not enough TemplateBuilder Parameters. Trace: ${Exception.Message}");
                    }
                }
            }

            return Template;
        }
    }
}
