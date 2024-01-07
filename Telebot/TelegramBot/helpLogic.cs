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
    internal class helpLogic
    {
        public helpLogic()
        {
            this.countFeilds = DTO.countFeilds;
        }

        private int countFeilds;

        public static IReplyMarkup Buttons(int columns, Dictionary<string, string> emojiList)
        {
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[columns][];

            int k = 1;

            for (int i = 0; i < columns; i++)
            {
                List<InlineKeyboardButton> ints = new List<InlineKeyboardButton>();

                for (int x = 0; x < columns; x++)
                {
                    if (k <= emojiList.Count)
                    {
                        if (emojiList[$"{k}"] == "none")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData($"{k}", $"{k}")); // ■
                        }
                        if (emojiList[$"{k}"] == "true")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData("🔴", $"{k}"));
                        }
                        if (emojiList[$"{k}"] == "false")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData("❌", $"{k}"));
                        }
                    }
                    k += 1;
                }

                buttons[i] = ints.ToArray();
            }

            return new InlineKeyboardMarkup(buttons);
        }

        public static InlineKeyboardButton[][] Buttons2(int columns, Dictionary<string, string> emojiList)
        {
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[columns][];

            int k = 1;

            for (int i = 0; i < columns; i++)
            {
                List<InlineKeyboardButton> ints = new List<InlineKeyboardButton>();

                for (int x = 0; x < columns; x++)
                {
                    if (k <= emojiList.Count)
                    {
                        if (emojiList[$"{k}"] == "none")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData($"{k}", $"{k}"));
                        }
                        if (emojiList[$"{k}"] == "true")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData("🔴", $"{k}"));
                        }
                        if (emojiList[$"{k}"] == "false")
                        {
                            ints.Add(InlineKeyboardButton.WithCallbackData("❌", $"{k}"));
                        }
                    }
                    k += 1;
                }

                buttons[i] = ints.ToArray();
            }

            return buttons;
        }

        public List<string> findFalse(Dictionary<string, string> emojiList, string fl)
        {
            List<string> lst = new List<string>();

            foreach (var i in DTO.emojiList)
            {
                if (i.Value == fl)
                {
                    lst.Add(i.Key);
                }
            }

            return lst;
        }

        public List<List<string>> findAllFalse(List<List<string>> ints, Dictionary<string, string> emojiList)
        {
            List<string> lst = findFalse(emojiList, "false");
            List<List<string>> mainLst = new List<List<string>>();

            foreach (var i in lst)
            {
                List<string> ls = new List<string>();
                ls.Add((traversingANestedArray(ints, i)[0]).ToString());
                ls.Add((traversingANestedArray(ints, i)[1]).ToString());
                mainLst.Add(ls);
            }

            return mainLst;
        }

        public List<string> showAllFalse(Dictionary<string, string> emojiList)
        {
            List<string> show = new List<string>();
            List<string> all = findFalse(emojiList, "false");
            List<List<string>> ls = listOfNumber(DTO.countFeilds);

            foreach (string a in all)
            {
                for(int i = 0; i < ls.Count; i++)
                {
                    for(int x = 0; x < ls[i].Count; x++)
                    {
                        if (ls[i][x] == a)
                        {
                            if(i - 1 >= 0 && emojiList[ls[i - 1][x]] != "false" && emojiList[ls[i - 1][x]] != "true")
                            {
                                if (!show.Contains(ls[i - 1][x]))
                                {
                                    show.Add(ls[i - 1][x]);
                                }
                            }
                            if(i + 1 < ls[i].Count && emojiList[ls[i + 1][x]] != "false" && emojiList[ls[i + 1][x]] != "true")
                            {
                                if (!show.Contains(ls[i + 1][x]))
                                {
                                    show.Add(ls[i + 1][x]);
                                }
                            }
                            if (x + 1 < ls[i].Count && emojiList[ls[i][x + 1]] != "false" && emojiList[ls[i][x + 1]] != "true")
                            {
                                if (!show.Contains(ls[i][x + 1]))
                                {
                                    show.Add(ls[i][x + 1]);
                                }
                            }
                            if (x - 1 >= 0 && emojiList[ls[i][x - 1]] != "false" && emojiList[ls[i][x - 1]] != "true")
                            {
                                if (!show.Contains(ls[i][x - 1]))
                                {
                                    show.Add(ls[i][x - 1]);
                                }
                            }
                            if (i - 1 >= 0 && x - 1 >= 0 && emojiList[ls[i - 1][x - 1]] != "false" && emojiList[ls[i - 1][x - 1]] != "true")
                            {
                                if (!show.Contains(ls[i - 1][x - 1]))
                                {
                                    show.Add(ls[i - 1][x - 1]);
                                }
                            }
                            if (i - 1 >= 0 && x + 1 < ls[i].Count && emojiList[ls[i - 1][x + 1]] != "false" && emojiList[ls[i - 1][x + 1]] != "true")
                            {
                                if (!show.Contains(ls[i - 1][x + 1]))
                                {
                                    show.Add(ls[i - 1][x + 1]);
                                }
                            }
                            if (i + 1 < ls[i].Count && x - 1 >= 0 && emojiList[ls[i + 1][x - 1]] != "false" && emojiList[ls[i + 1][x - 1]] != "true")
                            {
                                if (!show.Contains(ls[i + 1][x - 1]))
                                {
                                    show.Add(ls[i + 1][x - 1]);
                                }
                            }
                            if (i + 1 < ls[i].Count && x + 1 < ls[i].Count && emojiList[ls[i + 1][x + 1]] != "false" && emojiList[ls[i + 1][x + 1]] != "true")
                            {
                                if (!show.Contains(ls[i + 1][x + 1]))
                                {
                                    show.Add(ls[i + 1][x + 1]);
                                }
                            }
                        }
                    }
                }
            }

            return show;
        }

        public async void edM(long idOfChat)
        {
            var keyboard1 = new InlineKeyboardMarkup(helpLogic.Buttons2(DTO.countFeilds, DTO.emojiList));

            if (DTO.lastIndexOfMessage != 0)
            {
                await DTO.botClient.EditMessageTextAsync(
                    messageId: DTO.lastIndexOfMessage,
                    chatId: idOfChat,
                    text: "Suck",
                    replyMarkup: keyboard1);
            }
        }

        public List<List<string>> listOfNumber(int countFeilds)
        {
            List<List<string>> strings = new List<List<string>>();
            int k = 0;

            for (int i = 0; i < countFeilds; i++)
            {
                List<string> temp = new List<string>();

                for (int x = 0; x < countFeilds; x++)
                {
                    k += 1;
                    temp.Add(k.ToString());
                }

                strings.Add(temp);
            }

            return strings;
        }

        public int countFalse(Dictionary<string, string> emojiList)
        {
            int k = 0;

            foreach (var i in emojiList)
            {
                if (i.Value == "false")
                {
                    k++;
                }
            }

            return k;
        }

        public List<int> traversingANestedArray(List<List<string>> ints, string m)
        {
            List<int> lst = new List<int>();

            for (int i = 0; i < countFeilds; i++)
            {
                for (int x = 0; x < countFeilds; x++)
                {
                    if (ints[i][x] == m)
                    {
                        lst.Add(i);
                        lst.Add(x);
                    }
                }
            }

            return lst;
        }
    }
}
