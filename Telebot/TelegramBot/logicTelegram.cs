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
    internal class logicTelegram
    {

        public static async Task editMessage(CallbackQuery callBackData)
        {
            try
            {
                if (DTO.emojiList[callBackData.Data] != "true" && DTO.emojiList[callBackData.Data] != "false")
                {
                    DTO.emojiList[callBackData.Data] = "true";
                    var keyboard = new InlineKeyboardMarkup(helpLogic.Buttons2(DTO.countFeilds, DTO.emojiList));
                    logic2 lg2 = new logic2(DTO.botClient, callBackData.Message.Chat.Id);

                    if (DTO.lastIndexOfMessage != 0)
                    {
                        await DTO.botClient.EditMessageTextAsync(
                            messageId: DTO.lastIndexOfMessage,
                            chatId: callBackData.Message.Chat.Id,
                            text: "Suck",
                            replyMarkup: keyboard);

                    }
                    lg2.logicBot(callBackData.Message.Chat.Id);
                }
            }
            catch (Exception e) { }
        }
    }
}
