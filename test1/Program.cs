using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("请输入第一个字符串：");
            String s1 = Console.ReadLine();
            Console.WriteLine("请输入第二个字符串：");
            String s2 = Console.ReadLine();
            Console.WriteLine("得到的LCS值是：");
            getLCS(s1, s2);
            Console.ReadKey();

        }

        public static void getLCS(String str1, String str2)
        {
            char[] x = str1.ToCharArray();// 将字符串对象中的字符转换为对象数组
            char[] y = str2.ToCharArray();
            int[,] c = new int[x.Length + 1, y.Length + 1];// 定义c为输入的两个字符串转换的两个对象数组组成的二维数组，长度定义为两个字符串数组的长度+1
            /*
             这里利用两个循环嵌套，用第一个字符串数组依次去和第二个字符串数组比对
             */
            for (int i = 1; i <= x.Length; ++i)
            {
                for (int j = 1; j <= y.Length; ++j)
                {
                    // 当x[i - 1] == y[j - 1]时，说明x[i - 1]与y[j - 1] 一定在最长公共子序列中，
                    // 所以lcs (i , j) 是由lcs(i-1,j -1)之前的值决定的；即 lcs(i,j) = lcs(i-1, j-1) + 1
                    if (x[i - 1] == y[j - 1])
                        c[i, j] = c[i - 1, j - 1] + 1;
                    // 设在最长公共子序列中的最后一个值为zk，zk == xi, 那么最长公共子序列取决于去掉yj的y字符串数组与x字符串数组的最长公共子序列， 即lcs(i, j) = lcs(i, j-1)；
                    else if (c[i - 1, j] < c[i, j - 1])
                        c[i, j] = c[i, j - 1];
                    // 若yj在最长公共子序列中，那么lcs(i, j) = lcs(i - 1, j)
                    else
                        c[i, j] = c[i - 1, j];
                }
            }
            printLCS(c, x, y, x.Length, y.Length);
        }

        public static void printLCS(int[,] c, char[] x, char[] y, int i, int j)
        {
            if (i == 0 || j == 0)
                return;
            if (x[i - 1] == y[j - 1])
            {
                printLCS(c, x, y, i - 1, j - 1);
                Console.Write(x[i - 1]);
            }
            else if (c[i - 1, j] >= c[i, j - 1])
                printLCS(c, x, y, i - 1, j);
            else
                printLCS(c, x, y, i, j - 1);
        }
    }
}
