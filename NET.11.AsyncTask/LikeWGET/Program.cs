using LikeWGETLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeWGET
{
    class Program
    {
        static void Main(string[] args)
        {
            Conditions conditions = new Conditions(new Uri("http://komvak.by/"))
            {
                Domain = "by",
                Extensions = new List<string>()
            };

            LocalCloner cloner = new LocalCloner(conditions, new DownloadService());
            //Console.WriteLine(cloner.DownLoadByUri("https://stackoverflow.com/questions/599275/how-can-i-download-html-source-in-c-sharp"));
            //cloner.Clone("https://bipbap.ru/wp-content/uploads/2017/10/0_8eb56_842bba74_XL-640x400.jpg", @"D:\WORKSHOP_HW\Async_await\Net.10.AsyncTask\Downloads");
            //cloner.Clone("https://pixabay.com/ru/", @"D:\WORKSHOP_HW\Async_await\Net.10.AsyncTask\Downloads2");
            //Console.WriteLine(  Path.GetExtension("https://pixabay.png") );
            //cloner.UseExtension("https://pixabay.com/ru/");

            var task = cloner.Clone("http://komvak.by/", @"D:\WORKSHOP_HW\Async_await\Net.10.AsyncTask\", 0);
            Console.ReadLine();
        }


    }
}
