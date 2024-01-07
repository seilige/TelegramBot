using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class DTO
    {
        public static int WIN = 0;
        public static TelegramBotClient botClient;
        public static int countFeilds = 5;
        public static int lastIndexOfMessage = 0;
        public static int mainIndex = 0;
        public static string m = null;
        public static int b = 0;
        public static int val = 0;
        public static List<string> diag1 = new List<string>();
        public static List<string> diag2 = new List<string>();
        public static List<string> vert1 = new List<string>();
        public static List<string> vert2 = new List<string>();
        public static Dictionary<string, string> emojiList = new Dictionary<string, string>();
    }
}
