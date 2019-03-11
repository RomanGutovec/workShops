using LikeWGETLib;
using System;
using System.Collections.Generic;

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

            var task = cloner.Clone("http://komvak.by/", @"D:\WORKSHOP_HW\Async_await\Net.10.AsyncTask\", 0);
            Console.ReadLine();
        }
    }
}
