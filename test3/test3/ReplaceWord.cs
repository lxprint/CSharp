using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    class ReplaceWord
    {
        public string resStr1 = "";//存储原题需要替换字段
        public string resStr2 = "";//存储标准答案替换字段
        /// <summary>
        /// charList1 原题文章字符数组
        /// charList2 标准答案字符数组
        /// </summary>
        /// <param name="charList1"></param>
        /// <param name="charList2"></param>
        public ReplaceWord(char[] charList1, char[] charList2)
        {
            //char[] charList1 = readWord1.strList.ToCharArray();
            List<string> TextList1 = new List<string>();
            foreach (char i in charList1)
            {
                TextList1.Add(i.ToString());
            }
            string[] strList1 = TextList1.ToArray();
            //char[] charList2 = readWord2.strList.ToCharArray();
            List<string> TextList2 = new List<string>();
            foreach (char i in charList2)
            {
                TextList2.Add(i.ToString());
            }
            string[] strList2 = TextList2.ToArray();
            //string[] strList2 = { readWord2.TextList[0] };

            LCS<string> strLCS = new LCS<string>(strList1, strList2);

            string str1 = "";
            string str2 = "";
            for (var i = 0; i < strLCS.Items.Length; i++)
            {
                if (strLCS.Items[i].ToString().IndexOf("-") == 0)
                {
                    str1 += strLCS.Items[i].ToString().Substring(1);
                }
                else if (strLCS.Items[i].ToString().IndexOf("+") == 0)
                {
                    str2 += strLCS.Items[i].ToString().Substring(1);
                }

            }
            char[] res1 = str1.ToCharArray();
            char[] res2 = str2.ToCharArray();

            
            List<string> resList1 = new List<string>();
            List<string> resList2 = new List<string>();
            for (var j = 0; j < res1.Length; j++)
            {
                if (resStr1.IndexOf(res1[j]) == -1)
                {
                    resStr1 += res1[j];
                }
            }
            for (var k = 0; k < res2.Length; k++)
            {
                if (resStr2.IndexOf(res2[k]) == -1)
                {
                    resStr2 += res2[k];
                }
            }
        }
    }
}
