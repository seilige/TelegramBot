using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class Win
    {
        public static int win(Dictionary<string, string> emojiList)
        {
            int index = 0;

            helpLogic hlp = new helpLogic();

            List<string> lsFalse = hlp.findFalse(emojiList, "false");
            List<string> lsTrue = hlp.findFalse(emojiList, "true");

            foreach(var i in lsFalse)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();

            foreach (var i in lsFalse)
            {
                foreach (var x in lsFalse)
                {
                    foreach (var q in lsFalse)
                    {
                        if (q != x && x != i && i != q)
                        {
                            if (Convert.ToInt32(x) + 1 + DTO.countFeilds < emojiList.Count && Convert.ToInt32(i) + 1 + DTO.countFeilds < emojiList.Count && emojiList[(Convert.ToInt32(i) + 1 + DTO.countFeilds).ToString()] == emojiList[x] && emojiList[(Convert.ToInt32(x) + 1 + DTO.countFeilds).ToString()] == emojiList[q])
                            {
                                index = 1;
                            }
                            else if (Convert.ToInt32(x) - 1 + DTO.countFeilds < DTO.countFeilds && Convert.ToInt32(i) - 1 + DTO.countFeilds < DTO.countFeilds
                                && emojiList[(Convert.ToInt32(i) + DTO.countFeilds - 1).ToString()] == emojiList[x] && emojiList[(Convert.ToInt32(x) - 1 + DTO.countFeilds).ToString()] == emojiList[q])
                            {
                                index = 1;
                            }
                            else if (Convert.ToInt32(i) + 1 < DTO.countFeilds && Convert.ToInt32(x) + 1 < DTO.countFeilds && emojiList[(Convert.ToInt32(i) + 1).ToString()] == emojiList[x] && emojiList[(Convert.ToInt32(x) + 1).ToString()] == emojiList[q])
                            {
                                index = 1;
                            }
                            else if (Convert.ToInt32(x) - DTO.countFeilds >= 1 && Convert.ToInt32(q) - DTO.countFeilds >= 1 && emojiList[i] == emojiList[(Convert.ToInt32(x) - DTO.countFeilds).ToString()] && emojiList[x] == emojiList[(Convert.ToInt32(q) - DTO.countFeilds).ToString()])
                            {
                                index = 1;
                            }
                        }
                    }
                }

                foreach (var i1 in lsTrue)
                {
                    foreach (var x in lsTrue)
                    {
                        foreach (var q in lsTrue)
                        {
                            if (q != x && x != i1 && i1 != q)
                            {
                                if (Convert.ToInt32(x) + 1 + DTO.countFeilds < emojiList.Count && Convert.ToInt32(i) + 1 + DTO.countFeilds < emojiList.Count && 
                                    emojiList[(Convert.ToInt32(i1) + 1 + DTO.countFeilds).ToString()] == emojiList[x] && emojiList[(Convert.ToInt32(x) + 1 + DTO.countFeilds).ToString()] == emojiList[q])
                                {
                                    Console.WriteLine("1");
                                    index = 2;
                                }
                                else if (Convert.ToInt32(x) - 1 + DTO.countFeilds < DTO.countFeilds && Convert.ToInt32(i1) - 1 + DTO.countFeilds < DTO.countFeilds
                                    && emojiList[(Convert.ToInt32(i1) + DTO.countFeilds - 1).ToString()] == emojiList[x] && emojiList[(Convert.ToInt32(x) - 1 + DTO.countFeilds).ToString()] == emojiList[q])
                                {
                                    Console.WriteLine("2");
                                    index = 2;
                                }
                                else if (Convert.ToInt32(i1) + 1 < DTO.countFeilds && Convert.ToInt32(x) + 1 < DTO.countFeilds && emojiList[(Convert.ToInt32(i1) + 1).ToString()] == emojiList[x]
                                    && emojiList[(Convert.ToInt32(x) + 1).ToString()] == emojiList[q])
                                {
                                    Console.WriteLine("3");
                                    index = 2;
                                }
                                else if (Convert.ToInt32(x) - DTO.countFeilds >= 1 && Convert.ToInt32(q) - DTO.countFeilds >= 1 && emojiList[i1] == emojiList[(Convert.ToInt32(x) - DTO.countFeilds).ToString()] 
                                    && emojiList[x] == emojiList[(Convert.ToInt32(q) - DTO.countFeilds).ToString()])
                                {
                                    Console.WriteLine("4");
                                    index = 2;
                                }
                            }
                        }
                    }
                }
            }

            return index;
        }
    }
}
