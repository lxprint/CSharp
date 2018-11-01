using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    class Print
    {
        public Print(string title1, string title2)
        {
            string path1 = @"F:\工作\github\CSharp\test3\"+title1+".docx";
            string path2 = @"F:\工作\github\CSharp\test3\" + title2 + ".docx";
            ReadWord readWord1 = new ReadWord(path1);
            char[] charList1 = readWord1.strList.ToCharArray();

            ReadWord readWord2 = new ReadWord(path2);
            char[] charList2 = readWord2.strList.ToCharArray();

            ReplaceWord replaceword1 = new ReplaceWord(charList1, charList2);
            string resStr1 = replaceword1.resStr1.Trim();
            string resStr2 = replaceword1.resStr2.Trim();
            if (resStr1 == "" && resStr2 == "")
            {
                Console.WriteLine("输入1：" + title1 + ".docx，输入2：" + title2 + ".docx，输出为：");
                Console.WriteLine("没有替换题！");
                return;
            }
            if (resStr1 != "" && resStr2 == "")
            {
                int count1 = 0;
                StringMatch stringMatch1 = new StringMatch(readWord1.strList, resStr1);
                count1 = stringMatch1._count;
                Console.WriteLine("输入1：" + title1 + ".docx，输入2：" + title2 + ".docx，输出为：");
                Console.WriteLine("替换题：请删除文中所有的文字“" + resStr1 + "”。总分：" + count1 + "分");
                return;
            }
            int count = 0;
            StringMatch stringMatch = new StringMatch(readWord2.strList, resStr2);
            count = stringMatch._count;
            Console.WriteLine("输入1："+title1+".docx，输入2："+title2+".docx，输出为：");
            Console.WriteLine("替换题：请将文中所有的文字“" + resStr1 + "”替换为“" + resStr2 + "”。总分：" + count + "分");
        }
    }
}
