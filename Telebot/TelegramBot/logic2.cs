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
    internal class logic2
    {
        private helpLogic hlp;
        private long idOfChat;
        private TelegramBotClient botClient;

        public logic2(TelegramBotClient botClient, long idOfChat)
        {
            this.botClient = botClient;
            hlp = new helpLogic();
            this.idOfChat = idOfChat;
        }

        public async void logicBot(long idOfChat)
        {
            int k = 0;

            foreach (var i in DTO.emojiList)
            {
                if (i.Value == "true")
                {
                    k += 1;
                }
            }

            if (k == 1)
            {
                // first false
                List<string> ints = new List<string>();

                foreach (var i in DTO.emojiList)
                {
                    if (i.Value != "true" && i.Value != "false")
                    {
                        ints.Add(i.Key);
                    }
                }

                Random rnd = new Random();
                DTO.mainIndex = rnd.Next((int)(ints.Count - 1));
                DTO.m = ints[DTO.mainIndex];

                DTO.emojiList[DTO.m] = "false";
                var keyboard = new InlineKeyboardMarkup(helpLogic.Buttons2(DTO.countFeilds, DTO.emojiList));

                if (DTO.lastIndexOfMessage != 0)
                {
                    await botClient.EditMessageTextAsync(
                        messageId: DTO.lastIndexOfMessage,
                        chatId: this.idOfChat,
                        text: "Suck",
                        replyMarkup: keyboard);
                }
            }
            else
            {
                // second false
                List<List<string>> ints = hlp.listOfNumber(DTO.countFeilds);
                int mainX = hlp.traversingANestedArray(ints, DTO.m.ToString())[0];
                int mainY = hlp.traversingANestedArray(ints, DTO.m.ToString())[1];

                string valDL1 = null;
                string valDR1 = null;
                string valDR2 = null;
                string valDL2 = null;
                string valUp = null;
                string valDo = null;
                string valR = null;
                string valL = null;

                if (mainX + 1 < DTO.countFeilds && mainY - 1 >= 0) { valDL2 = ints[mainX + 1][mainY - 1]; }
                if (mainX - 1 >= 0 && mainY + 1 < DTO.countFeilds) { valDR1 = ints[mainX - 1][mainY + 1]; }
                if (mainX + 1 < DTO.countFeilds) { valDo = ints[mainX + 1][mainY]; }
                if (mainX - 1 >= 0) { valUp = ints[mainX - 1][mainY]; }
                if (mainY - 1 >= 0) { valR = ints[mainX][mainY - 1]; }
                if (mainY + 1 < DTO.countFeilds) { valL = ints[mainX][mainY + 1]; }
                if (mainX + 1 < DTO.countFeilds && mainY + 1 < DTO.countFeilds) { valDR2 = ints[mainX + 1][mainY + 1]; }
                if (mainX - 1 >= 0 && mainY - 1 >= 0) { valDL1 = ints[mainX - 1][mainY - 1]; }
                if (valDL1 != null && DTO.emojiList[valDL1] != "true") { DTO.diag2.Add(valDL1); }
                if (valDR1 != null && DTO.emojiList[valDR1] != "true") { DTO.diag2.Add(valDR1); }
                if (valDR2 != null && DTO.emojiList[valDR2] != "true") { DTO.diag1.Add(valDR2); }
                if (valDL2 != null && DTO.emojiList[valDL2] != "true") { DTO.diag1.Add(valDL2); }
                if (valUp != null && DTO.emojiList[valUp] != "true") { DTO.vert1.Add(valUp); }
                if (valDo != null && DTO.emojiList[valDo] != "true") { DTO.vert1.Add(valDo); }
                if (valR != null && DTO.emojiList[valR] != "true") { DTO.vert2.Add(valR); }
                if (valL != null && DTO.emojiList[valL] != "true") { DTO.vert2.Add(valL); }

                if (hlp.countFalse(DTO.emojiList) == 1)
                {
                    List<int> lst1 = new List<int>();
                    Random rnd = new Random();

                    if (DTO.diag1.Count > 0)
                    {
                        lst1.Add(0);
                    }

                    if (DTO.diag2.Count > 0)
                    {
                        lst1.Add(1);
                    }

                    if (DTO.vert1.Count > 0)
                    {
                        lst1.Add(2);
                    }

                    if (DTO.vert2.Count > 0)
                    {
                        lst1.Add(3);
                    }

                    int index = rnd.Next(0, (int)lst1.Count);
                    index = lst1[index];

                    if (index == 0) // diag 1
                    {
                        if (DTO.diag1.Count == 1)
                        {
                            DTO.emojiList[DTO.diag1[0]] = "false";
                        }
                        else if (DTO.diag1.Count == 2)
                        {
                            int index2 = rnd.Next(0, (int)DTO.diag1.Count);
                            DTO.emojiList[DTO.diag1[index2]] = "false";
                        }
                    }

                    if (index == 1) // diag 2
                    {
                        if (DTO.diag2.Count == 1)
                        {
                            DTO.emojiList[DTO.diag2[0]] = "false";
                        }
                        else if (DTO.diag2.Count == 2)
                        {
                            int index2 = rnd.Next(0, (int)DTO.diag2.Count);
                            DTO.emojiList[DTO.diag2[index2]] = "false";
                        }
                    }

                    if (index == 2) // up down
                    {
                        if (DTO.vert1.Count == 1)
                        {
                            DTO.emojiList[DTO.vert1[0]] = "false";
                        }
                        else if (DTO.vert1.Count == 2)
                        {
                            int index2 = rnd.Next(0, (int)DTO.vert1.Count);
                            DTO.emojiList[DTO.vert1[index2]] = "false";
                        }
                    }

                    if (index == 3) // right left
                    {
                        if (DTO.vert2.Count == 1)
                        {
                            DTO.emojiList[DTO.vert2[0]] = "false";
                        }
                        else if (DTO.vert2.Count == 2)
                        {
                            int index2 = rnd.Next(0, (int)DTO.vert2.Count);
                            DTO.emojiList[DTO.vert2[index2]] = "false";
                        }
                    }
                }
                // end false second
                else
                {
                    List<List<string>> ints1 = hlp.listOfNumber(DTO.countFeilds);
                    List<string> lst = hlp.findFalse(DTO.emojiList, "false");

                    logic lg = new logic();

                    if (lst.Count == 2)
                    {
                        Dictionary<string, List<int>> dct = lg.log(lst, botClient);

                        if (dct["mIndex"][0] >= 1 || dct["mIndex"][1] >= 1 || dct["mIndex"][2] >= 1 || dct["mIndex"][3] >= 1)
                        {
                            DTO.val = 1;
                        }
                        else
                        {
                            DTO.val = 0;
                        }
                        int iV1 = 0;
                        int iX = 0;
                        int iY = 0;
                        int iA = 0;
                        int iX1 = 0;
                        int iY1 = 0;
                        int iA1 = 0;
                        int iV = 0;
                        int countFeilds = DTO.countFeilds;
                        List<int> ints2 = new List<int>();

                        if (dct["indexV"][1] == 2) { iV1 = 1; }
                        if (dct["indexV"][0] == 1) { iV = 1; }
                        if (dct["indexV"][0] == 2) { iV = 2; }

                        if (dct["index"][1] == 2) { iX1 = 1; }
                        if (dct["index"][0] == 1) { iX = 1; }
                        if (dct["index"][0] == 2) { iX = 2; }

                        if (dct["indexY"][1] == 2) { iY1 = 1; }
                        if (dct["indexY"][0] == 1) { iY = 1; }
                        if (dct["indexY"][0] == 2) { iY = 2; }

                        if (dct["indexA"][1] == 2) { iA1 = 1; }
                        if (dct["indexA"][0] == 1) { iA = 1; }
                        if (dct["indexA"][0] == 2) { iA = 2; }

                        if (iV >= 1) { ints2.Add(1); }
                        if (iX >= 1) { ints2.Add(2); }
                        if (iY >= 1) { ints2.Add(3); }
                        if (iA >= 1) { ints2.Add(4); }

                        if (ints2.Count > 0){ 
                            Random rnd2 = new Random();
                            int fIndex = rnd2.Next(0, (int)ints2.Count);
                            fIndex = ints2[fIndex];

                            if (fIndex == 2)
                            {
                                if (iV1 == 1)
                                {
                                    Random rnd = new Random();
                                    int index = rnd.Next(1, 3);

                                    if (index == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - 2).ToString()] = "false";
                                    }
                                    else
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) + 2).ToString()] = "false";
                                    }
                                }
                                else if (iV == 1)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[1]) - 2).ToString()] = "false";
                                }
                                else if (iV == 2)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[0]) + 2).ToString()] = "false";
                                }
                            }
                            else if (fIndex == 1)
                            {
                                if (iX1 == 1)
                                {
                                    Random rnd = new Random();
                                    int index = rnd.Next(1, 3);

                                    if (index == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) + countFeilds).ToString()] = "false";
                                    }
                                    else
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - countFeilds * 2).ToString()] = "false";
                                    }
                                }
                                else if (iX == 1)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[1]) + countFeilds).ToString()] = "false";
                                }
                                else if (iX == 2)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[1]) - countFeilds * 2).ToString()] = "false";
                                }
                            }
                            else if (fIndex == 3)
                            {
                                if (iY1 == 1)
                                {
                                    Random rnd = new Random();
                                    int index = rnd.Next(1, 3);

                                    if (index == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) + 1 - countFeilds).ToString()] = "false";
                                    }
                                    else
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - 1 + countFeilds).ToString()] = "false";
                                    }
                                }
                                else if (iY == 1)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[0]) + 1 - countFeilds).ToString()] = "false";
                                }
                                else if (iY == 2)
                                {
                                    Console.WriteLine($"zhopa: {Convert.ToInt32(lst[0]) - 1 + countFeilds}");
                                    DTO.emojiList[(Convert.ToInt32(lst[1]) - 1 + countFeilds).ToString()] = "false";
                                }
                            }
                            else if (fIndex == 4)
                            {
                                if (iA1 == 1)
                                {
                                    Random rnd = new Random();
                                    int index = rnd.Next(1, 3);

                                    if (index == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) - 1 - countFeilds).ToString()] = "false";
                                    }
                                    else
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 + countFeilds).ToString()] = "false";
                                    }
                                }
                                else if (iA == 1)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[0]) - 1 - countFeilds).ToString()] = "false";
                                }
                                else if (iA == 2)
                                {
                                    DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 + countFeilds).ToString()] = "false";
                                }
                            }
                        }
                        else
                        {
                            if (iV == 1)
                            {
                                Console.WriteLine(1);
                                DTO.emojiList[(Convert.ToInt32(lst[1]) - 2).ToString()] = "false";
                            }
                            else if (iV == 2)
                            {
                                Console.WriteLine(2);
                                DTO.emojiList[(Convert.ToInt32(lst[0]) + 2).ToString()] = "false";
                            }
                            else if (iX == 1)
                            {
                                Console.WriteLine(3);
                                DTO.emojiList[(Convert.ToInt32(lst[1]) + countFeilds).ToString()] = "false";
                            }
                            else if (iX == 2)
                            {
                                DTO.emojiList[(Convert.ToInt32(lst[1]) - countFeilds * 2).ToString()] = "false";
                                Console.WriteLine(4);
                            }
                            else if (iY == 1)
                            {
                                DTO.emojiList[(Convert.ToInt32(lst[0]) + 1 - countFeilds).ToString()] = "false";
                                Console.WriteLine(5);
                            }
                            else if (iY == 2)
                            {
                                DTO.emojiList[(Convert.ToInt32(lst[1]) - 1 + countFeilds).ToString()] = "false";
                                Console.WriteLine(6);
                            }
                            else if (iA == 1)
                            {
                                DTO.emojiList[(Convert.ToInt32(lst[0]) - 1 - countFeilds).ToString()] = "false";
                                Console.WriteLine(7);
                            }
                            else if (iA == 2)
                            {
                                Console.WriteLine(8);
                                DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 + countFeilds).ToString()] = "false";
                            }
                        }
                    }

                    if (DTO.val == 0)
                    {
                        List<string> ls = hlp.showAllFalse(DTO.emojiList);
                        Random rnd = new Random();
                        string i = ls[rnd.Next(ls.Count)];
                        DTO.emojiList[i] = "false";
                        var keyboard1 = new InlineKeyboardMarkup(helpLogic.Buttons2(DTO.countFeilds, DTO.emojiList));

                        if (DTO.lastIndexOfMessage != 0)
                        {
                            await botClient.EditMessageTextAsync(
                                messageId: DTO.lastIndexOfMessage,
                                chatId: this.idOfChat,
                                text: "Suck",
                                replyMarkup: keyboard1);
                        }
                        DTO.b = 1;
                    }
                    if (DTO.b == 1)
                    {
                        DTO.b = 0;
                        List<string> ls = hlp.showAllFalse(DTO.emojiList);
                        int iV1 = 0;
                        int iX = 0;
                        int iY = 0;
                        int iA = 0;
                        int iX1 = 0;
                        int iY1 = 0;
                        int iA1 = 0;
                        int iV = 0;

                        foreach (var i in ls)
                        {
                            List<string> s = new List<string>();
                            s.Add(i);
                            List<int> ints2 = new List<int>();

                            foreach (var x in ls)
                            {
                                if(i != x)
                                {
                                    s.Add(x);
                                }
                            }

                            if(s.Count == 2)
                            {
                                Dictionary<string, List<int>> dct = lg.log(s, botClient);
                                int countFeilds = DTO.countFeilds;

                                if (dct["mIndex"][0] >= 1 || dct["mIndex"][1] >= 1 || dct["mIndex"][2] >= 1 || dct["mIndex"][3] >= 1)
                                {
                                    DTO.val = 1;
                                }

                                if (dct["indexV"][1] == 2){iV1 = 1;}
                                else if (dct["indexV"][0] == 1){iV = 1;}
                                else if (dct["indexV"][0] == 2){iV = 2;}

                                if (dct["index"][1] == 2){iX1 = 1;}
                                else if (dct["index"][0] == 1){iX = 1;}
                                else if (dct["index"][0] == 2){iX = 2;}

                                if (dct["indexY"][1] == 2){iY1 = 1;}
                                else if (dct["indexY"][0] == 1){iY = 1;}
                                else if (dct["indexY"][0] == 2){iY = 2;}

                                if (dct["indexA"][1] == 2){iA1 = 1;}
                                else if (dct["indexA"][0] == 1){iA = 1;}
                                else if (dct["indexA"][0] == 2){iA = 2;}

                                if(iV >= 1){ints2.Add(1);}
                                if (iX >= 1){ints2.Add(2);}
                                if (iY >= 1){ints2.Add(3);}
                                if (iA >= 1){ints2.Add(4);}

                                if(ints2.Count > 0)
                                { 
                                    Random rnd2 = new Random();
                                    int fIndex = rnd2.Next(0, (int)ints2.Count);
                                    fIndex = ints2[fIndex];


                                    if(fIndex == 1)
                                    {
                                        if(iV1 == 1)
                                        {
                                            Random rnd = new Random();
                                            int index = rnd.Next(1, 3);

                                            if(index == 1)
                                            {
                                               DTO.emojiList[(Convert.ToInt32(s[1]) - 2).ToString()] = "false";
                                            }
                                            else
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[0]) + 2).ToString()] = "false";
                                            }
                                        }
                                        else if(iV == 1)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[1]) - 2).ToString()] = "false";
                                        }
                                        else if(iV == 2)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[0]) + 2).ToString()] = "false";
                                        }
                                    }
                                    else if (fIndex == 2)
                                    {
                                        if (iX1 == 1)
                                        {
                                            Random rnd = new Random();
                                            int index = rnd.Next(1, 3);

                                            if (index == 1)
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[1]) + countFeilds).ToString()] = "false";
                                            }
                                            else
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[1]) - countFeilds * 2).ToString()] = "false";
                                            }
                                        }
                                        else if (iX == 1)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[1]) + countFeilds).ToString()] = "false";
                                        }
                                        else if (iX == 2)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[1]) - countFeilds * 2).ToString()] = "false";
                                        }
                                    }
                                    else if (fIndex == 3)
                                    {
                                        if (iY1 == 1)
                                        {
                                            Random rnd = new Random();
                                            int index = rnd.Next(1, 3);

                                            if (index == 1)
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[0]) + 1 - countFeilds).ToString()] = "false";
                                            }
                                            else
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[1]) - 1 + countFeilds).ToString()] = "false";
                                            }
                                        }
                                        else if (iY == 1)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[0]) + 1 - countFeilds).ToString()] = "false";
                                        }
                                        else if (iY == 2)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[1]) - 1 + countFeilds).ToString()] = "false";
                                        }
                                    }
                                    else if (fIndex == 4)
                                    {
                                        if (iA1 == 1)
                                        {
                                            Random rnd = new Random();
                                            int index = rnd.Next(1, 3);

                                            if (index == 1)
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[0]) - 1 - countFeilds).ToString()] = "false";
                                            }
                                            else
                                            {
                                                DTO.emojiList[(Convert.ToInt32(s[1]) + 1 + countFeilds).ToString()] = "false";
                                            }
                                        }
                                        else if (iA == 1)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[0]) - 1 - countFeilds).ToString()] = "false";
                                        }
                                        else if (iA == 2)
                                        {
                                            DTO.emojiList[(Convert.ToInt32(s[1]) + 1 + countFeilds).ToString()] = "false";
                                        }
                                    }
                                }
                                else
                                {
                                    if (iV == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - 2).ToString()] = "false";
                                    }
                                    else if (iV == 2)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) + 2).ToString()] = "false";
                                    }
                                    else if (iX == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) + countFeilds).ToString()] = "false";
                                    }
                                    else if (iX == 2)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - countFeilds * 2).ToString()] = "false";
                                    }
                                    else if (iY == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) + 1 - countFeilds).ToString()] = "false";
                                    }
                                    else if (iY == 2)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) - 1 + countFeilds).ToString()] = "false";
                                    }
                                    else if (iA == 1)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[0]) - 1 - countFeilds).ToString()] = "false";
                                    }
                                    else if (iA == 2)
                                    {
                                        DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 + countFeilds).ToString()] = "false";
                                    }
                                }


                            }
                        }
                    }
                }

                if (Win.win(DTO.emojiList) == 0)
                {
                    try
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
                    catch(Exception e) { }
                }
                else if(Win.win(DTO.emojiList) == 1)
                {
                    try
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
                    catch (Exception e) { }

                    await botClient.SendTextMessageAsync(idOfChat, "Bot win!");
                }
                else if (Win.win(DTO.emojiList) == 2)
                {
                    try
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
                    catch (Exception e) { }

                    await botClient.SendTextMessageAsync(idOfChat, "User win!");
                }
            }
        }
    }
}
