﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string wordPathStr = @"C:\科研细则.docx";// 定义文档地址
            using (WordprocessingDocument doc = WordprocessingDocument.Open(wordPathStr, true))
            {
                Body body = doc.MainDocumentPart.Document.Body;
                foreach (var paragraph in body.Elements<Paragraph>())// 遍历输出文档内容
                {
                    Console.WriteLine(paragraph.InnerText);
                }
            }
            Console.ReadLine();
        }
    }
}