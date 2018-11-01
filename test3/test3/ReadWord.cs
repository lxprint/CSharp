using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    class ReadWord
    {
        public List<string> TextList = new List<string>();
        public string strList;
        public void ChangeToStr(List<string> text)
        {

        }
        public ReadWord(string filename)
        {
            try
            {
                using (WordprocessingDocument wordprocessingDocument =
                WordprocessingDocument.Open(filename, true))
                {
                    // 获取主体
                    DocumentFormat.OpenXml.Wordprocessing.Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                    // 获取所有元素
                    IEnumerable<DocumentFormat.OpenXml.OpenXmlElement> elements = body.Elements<DocumentFormat.OpenXml.OpenXmlElement>();
                    //遍历元素列表，输出内容
                    foreach (DocumentFormat.OpenXml.OpenXmlElement element in elements)
                    {
                        //Text += element.InnerText+ "\r\n";
                        //Console.WriteLine(element.InnerText);
                        strList += element.InnerText.ToString();
                        TextList.Add(element.InnerText);

                    }
                }
            }
            catch
            {
                Console.WriteLine("没有找到文件");
                Console.ReadKey();
            }
        }
    }
}
