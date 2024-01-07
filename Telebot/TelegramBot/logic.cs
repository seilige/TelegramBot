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
    internal class logic
    {
        private helpLogic hlp;

        private int countFeilds;

        public logic()
        {
            hlp = new helpLogic();
            this.countFeilds = DTO.countFeilds;
        }

        public Dictionary<string, List<int>> log(List<string> lst, TelegramBotClient botClient)
        {
            helpLogic hlp = new helpLogic();

            List<List<string>> ints = hlp.listOfNumber(countFeilds);

            int mainX1 = hlp.traversingANestedArray(ints, lst[0])[0];
            int mainY1 = hlp.traversingANestedArray(ints, lst[0])[1];
            int mainX2 = hlp.traversingANestedArray(ints, lst[1])[0];
            int mainY2 = hlp.traversingANestedArray(ints, lst[1])[1];

            Dictionary<string, List<int>> dct = new Dictionary<string, List<int>>()
            {
                {"indexV", new List<int>(){0,0} },
                {"index", new List<int>(){0,0} },
                {"indexY", new List<int>(){0,0} },
                {"indexA", new List<int>(){0,0} },
                {"mIndex", new List<int>(){0,0,0,0} }
            };

            if (mainY1 - 1 >= 0 && mainY1 + 1 == mainY2 && mainX1 == mainX2 && 1 <= Convert.ToInt32(lst[1]) - 2)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) - 2).ToString()] != "true")
                {
                    dct["indexV"][0] = 1;
                    dct["indexV"][1] += 1;
                }
                else
                {
                    dct["mIndex"][0] += 1;
                }
            }
            if (mainY1 + 1 < countFeilds && mainY1 + 1 == mainY2 && Convert.ToInt32(lst[0]) + 2 <= countFeilds && DTO.emojiList.Count >= Convert.ToInt32(lst[0]) + 2 && mainX1 == mainX2)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[0]) + 2).ToString()] != "true")
                {
                    dct["indexV"][0] = 2;
                    dct["indexV"][1] += 1;
                }
                else
                {
                    dct["mIndex"][0] += 1;
                }
            }

            if (DTO.emojiList.Count >= Convert.ToInt32(lst[1]) + countFeilds && Convert.ToInt32(lst[1]) + countFeilds >= 1 && mainY1 == mainY2 && mainX1 == mainX2 - 1)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) + countFeilds).ToString()] != "true")
                {
                    dct["index"][0] = 1;
                    dct["index"][1] += 1;
                }
                else
                {
                    dct["mIndex"][1] += 1;
                }
            }

            if (1 <= Convert.ToInt32(lst[1]) - countFeilds * 2 && Convert.ToInt32(lst[1]) - countFeilds * 2 < countFeilds && mainY1 == mainY2 && mainX1 == mainX2 - 1)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) - countFeilds * 2).ToString()] != "true")
                {
                    dct["index"][0] = 2;
                    dct["index"][1] += 1;
                }
                else
                {
                    dct["mIndex"][1] += 1;
                }
            }

            if (mainX1 + 1 == mainX2 && mainY1 - 1 == mainY2 && mainX1 < ints[0].Count && mainY1 - 1 > 0 && 1 <= Convert.ToInt32(lst[1]) + 1 - countFeilds)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 - countFeilds).ToString()] != "true")
                {
                    dct["indexY"][0] = 1;
                    dct["indexY"][1] += 1;
                }
                else
                {
                    dct["mIndex"][2] += 1;
                }
            }

            if (mainX1 + 1 == mainX2 && mainY1 - 1 == mainY2 && mainX1 + 1 > ints[0].Count && mainY1 - 1 >= 0 && DTO.emojiList.Count >= Convert.ToInt32(lst[1]) + countFeilds + 1)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 +  countFeilds).ToString()] != "true")
                {
                    dct["indexY"][0] = 2;
                    dct["indexY"][1] += 1;
                }
                else
                {
                    dct["mIndex"][2] += 1;
                }
            }

            if (mainX1 + 1 == mainX2 && mainY1 + 1 == mainY2 && mainX1 + 1 < ints[0].Count && mainY1 < ints[0].Count && 1 <= Convert.ToInt32(lst[0]) - 1 - countFeilds)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[0]) - 1 - countFeilds).ToString()] != "true")
                {
                    dct["indexA"][0] = 1;
                    dct["indexA"][1] += 1;
                }
                else
                {
                    dct["mIndex"][3] += 1;
                }
            }
            if (mainX1 + 1 == mainX2 && mainY1 + 1 == mainY2 && mainX1 + 1 < ints[0].Count && mainY1 < ints[0].Count && DTO.emojiList.Count >= Convert.ToInt32(lst[1]) + 1 + countFeilds)
            {
                if (DTO.emojiList[(Convert.ToInt32(lst[1]) + 1 + countFeilds).ToString()] != "true")
                {
                    dct["indexA"][0] = 2;
                    dct["indexA"][1] += 1;
                }
                else
                {
                    dct["mIndex"][3] += 1;
                }
            }

            return dct;
        }
    }
}
