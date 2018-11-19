#第四次作业
------
- 刘欣
##程序实验说明

###Program.cs类
```
主程序的入口，有应用窗口，选择含有带测评文件的文件夹，文件夹选错会有错误提示，文件夹选择正确才可以点击开始评分，
在应用窗口显示最后评分结果。
```
 
###  EWordDocument.cs类

```
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public class EWordDocument
    {
        public List<string> LText;
        public void Open(string path)
        {
            //语句结束后，自动释放WordDocument，这里不让它消毁
            //using (WordDocument = WordprocessingDocument.Open(path, false))
            WordprocessingDocument WordDocument = WordprocessingDocument.Open(path, false);
            Body body = WordDocument.MainDocumentPart.Document.Body;
            if (body.Elements() == null)
                return;

            LText = new List<string>();

            foreach (DocumentFormat.OpenXml.OpenXmlElement obj in WordDocument.MainDocumentPart.Document.Body.Elements())
            {
                if (obj is Paragraph)
                {//段落
                    string PrgText = null;
                    Paragraph paragraph = (Paragraph)obj;
                    string str = null;
                    foreach (Text text in paragraph.Descendants<Text>())
                    {
                        str += text.Text;
                    }
                    if (PrgText == null)
                        PrgText = str;
                    else
                        PrgText += $"\n{str}";
                    LText.Add(PrgText);
                }
                else if (obj is Table)
                {//表格                  
                }
                else if (obj is SectionProperties)
                {//页面属性
                }
            }
        }
    }
}


```

### LCS.cs类

LCS（Longest Common Subsequence），即：最长公共子序列，
 它是求两个字符串最长公共子序列的问题。
 
 最后输出是题目类型：请将文中所有的文字\"{chdisn}\"替换为\"{chdisp}\"，计算总分"


### Grade.cs类

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    class Grade
    {
        public string title = "";
        public int grade = 0;
        /// <summary>
        /// 从index开始在strLCS中往后搜索替换字符串
        /// </summary>
        /// <param name="index">开始搜索的位置</param>
        /// <param name="strLCS">LCS串</param>
        /// <param name="strBefore">返回：原字符串，如果未找到替换，strBefore为空</param>
        /// <param name="strAfter">返回：替换目标字符串</param>
        /// <returns>返回最后的Item的下一个Item的位置</returns>
        static int GetNextReplace(int index, LCS<char> strLCS, ref string strBefore, ref string strAfter)
        {
            //ITEM_MODE.X表示源文件中的原始文字，这里是原题，ITEM_MODE.Y表示目标文件中的替换后的新文字，这里是答案。
            //本实验只有两种情况
            //情况1，全文替换文字：先出现ITEM_MODE.Y后出现ITEM_MODE.X
            //情况2，全文删除文字：直接出现ITEM_MODE.X
            //出现ITEM_MODE.Y后没有出现ITEM_MODE.X表示增加文字，不是本实验研究范围。
            strBefore = null;
            strAfter = null;
            int i;
            for (i = index; i < strLCS.Items.Length; i++)
            {
                Item<char> item = strLCS.Items[i];
                if (item.Mode == ITEM_MODE.Y)
                {
                    //Console.WriteLine(item); //测试用
                    //如果遇到下一组替换，本次替换结束
                    if (strBefore != null)
                        break;
                    strAfter += item.Value;
                }
                else if (item.Mode == ITEM_MODE.X)
                {
                    //Console.WriteLine(item);//测试用
                    strBefore += item.Value;
                }
                else
                {
                    if (strBefore != null)
                        break;
                    else if (strAfter != null)//如果只是增加，不认为是替换，继续往后找
                        strAfter = null;
                }
            }
            return i;
        }
        public Grade(string url1,string url2)
        {
            //替换前的字符串
            string strBefore = null;
            //替换后的字符串
            string strAfter = null;

            //替换出现的次数
            int Count = 0;

            EWordDocument eWordDocument原题 = new EWordDocument();
            eWordDocument原题.Open(url1);

            EWordDocument eWordDocument答案 = new EWordDocument();
            eWordDocument答案.Open(url2);
            int idx;
            for (int i = 0; i < eWordDocument原题.LText.Count(); i++)
            {
                char[] arrayX = eWordDocument原题.LText[i].ToArray();
                char[] arrayY = eWordDocument答案.LText[i].ToArray();
                LCS<char> strLCS = new LCS<char>(arrayX, arrayY);
                idx = 0;
                string strBefore_ = null;
                string strAfter_ = null;
                while (idx < strLCS.Items.Length)
                {
                    //如果未找到替换，strBefore为空
                    if (strBefore == null)
                    {
                        idx = GetNextReplace(idx, strLCS, ref strBefore, ref strAfter);
                        if (strBefore != null)
                            Count++;
                    }
                    else
                    {
                        idx = GetNextReplace(idx, strLCS, ref strBefore_, ref strAfter_);
                        if (strBefore == strBefore_ && strAfter == strAfter_)
                            Count++;
                    }
                }
            }
            if (strBefore != null)
            {
                grade = Count;
                if (strAfter != null)
                {
                    
                    title = "替换题：请将文中所有的文字" + strBefore + "替换为" + strAfter + "。总分：" + Count + "分";
                }
                //Console.WriteLine("替换题：请将文中所有的文字“{0}”替换为“{1}”。总分：{2}分", strBefore, strAfter, Count);
                else
                    title = "替换题：请删除文中所有的文字" + strBefore + "总分：" + Count + "分";
                //Console.WriteLine("替换题：请删除文中所有的文字“{0}”。总分：{1}分", strBefore, Count);
            }
            else
                title = "没有替换题！";
            //Console.WriteLine("没有替换题！");
        }
    }
}
```

### Gradfile.cs类

存储和读取读取文件

```
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public class Student
    {
        public string Name;
        public string Number;
        public int Grade;
        public Student(string name,string number, int grade)
        {
            Name = name;
            Number = number;
            Grade = grade;
        }
    }
    class MyFile
    {
        public FileStream F;
        public MyFile(FileStream F)
        {
            this.F = F;
        }
        public void WriteInt(int i)
        {
            byte[] intBuff = BitConverter.GetBytes(i); // 将 int 转换成字节数组      
            F.Write(intBuff, 0, 4);
        }
        public void WriteString(string str)
        {
            byte[] strArray = System.Text.Encoding.Default.GetBytes(str);
            WriteInt(strArray.Length);
            F.Write(strArray, 0, strArray.Length);
        }
        public int ReadInt()
        {
            byte[] intArray = new byte[4];
            F.Read(intArray, 0, 4);
            int iRead = BitConverter.ToInt32(intArray, 0);
            return iRead;
        }
        public string ReadString()
        {
            int len = ReadInt();
            byte[] strArray = new byte[len];
            F.Read(strArray, 0, len);
            string strRead = System.Text.Encoding.Default.GetString(strArray);
            return strRead;
        }
    }
    class GradeFile
    {
        public GradeFile(List<Student> students)
        {
            FileStream F = new FileStream("F:\\工作\\github\\CSharp\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            string strWrite = "";
            //考号、学生姓名、分数
            //Student student = new Student(name,number,grade);
            //strWrite = JsonConvert.DeserializeObject(students);
            foreach (Student student in students)
            {
                //stuStrs.Add(student);
                strWrite += "name:" + student.Name + ",number:" + student.Number + ",grade:" + student.Grade + ";";
            }
            MyFile.WriteString(strWrite);
            
            F.Position = 0;
            string strRead = MyFile.ReadString();
            F.Close();
        }
       
    }
    class ReadGradeFile
    {
        public string[] grades;
        public ReadGradeFile()
        {
            FileStream F = new FileStream("F:\\工作\\github\\CSharp\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            //考号、学生姓名、分数
            //Student student = new Student(name,number,grade);
            F.Position = 0;
            string strRead = MyFile.ReadString();
            //int intRead = MyFile.ReadInt();
            char[] separator = { ';' };
            grades = strRead.Split(separator);

            F.Close();
        }

    }
}

```

## 运行结果
```
替换题：请将文中所有的文字“国考”替换为“GK”。总分：9分
考试结果：
1001    张三    9
1002    李思思  7
1003    王五    6
```
## 参考资料

第一次实验，第二次实验，第三次实验


https://blog.csdn.net/rrrfff/article/details/7523437

[OpenXmlElement类示例](https://msdn.microsoft.com/zh-cn/library/office/documentformat.openxml.openxmlelement.aspx)

