using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace exam
{
    class Program
    {
        static void Main(string[] args)
        {
            Print print = new Print("国考_原题", "国考_标准答案1");
            Print print1 = new Print("国考_原题", "国考_标准答案2");
            Print print2 = new Print("国考_原题", "国考_标准答案3");
            Print print3 = new Print("国考_原题", "国考_原题");


            //Print print2 = new Print("国考_原题", "国考_标准答案3");
            //Program.Print(@"F:\workspacezwd\CSharp\CSharp\test3\国考_原题.docx", @"F:\workspacezwd\CSharp\CSharp\test3\国考_标准答案1.docx");


            Console.ReadLine();
        }
    }
}
