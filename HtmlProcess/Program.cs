using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Ivony.Html;

namespace HtmlProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = null;

            if (File.Exists("d:\\x.txt"))
            {
                fs = new FileStream("d:\\x.txt", FileMode.Append);
            }
            else
            {
                fs = new FileStream("d:\\x.txt", FileMode.Create);
            }

            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);


            string l_baseUrl = "http://www.42xs.com/read/0/27/";
            var l_nextUrl = "5779.html";

            string url = "";
            string txt = "";
            string title = "";

            while (!string.IsNullOrEmpty(l_nextUrl))
            {
                try
                {
                    url = l_baseUrl + l_nextUrl;

                    var doc = new Ivony.Html.Parser.JumonyParser().LoadDocument(url);
                    var titleDom = doc.FindFirst("#center > div.title > h1");
                    title = titleDom.InnerText();

                    var dom = doc.FindFirst("#content");
                    txt = dom.InnerText();

                    var domNext = doc.FindFirst("#container > div:nth-child(3) > div > div.jump > a:nth-child(6)");
                    l_nextUrl = domNext.Attribute("href").Value();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} failed", url);
                    l_nextUrl = "";
                }

                Console.WriteLine(title);
                sw.WriteLine("");
                sw.WriteLine(title);
                sw.WriteLine("");
                sw.WriteLine(txt);
            }

            Console.Write("The End. Press any key to exit...");
            Console.ReadKey();

            sw.Close();
            fs.Close();
        }
    }
}
