﻿using Ao.Lang.Lookup;
using Microsoft.Extensions.Configuration;
using System;

namespace Ao.Lang.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var h = CultureInfoHelper.IsAvaliableCulture("zh");
            var langSer = new LanguageService();
            langSer.EnsureGetLangNode("zh-cn")
                .AddJsonFile("lang.zh-cn.json");
            langSer.EnsureGetLangNode("en-us")
                .AddJsonFile("lang.en-us.json");
            var zhRoot = langSer.GetRoot("zh-cn");
            var enRoot = langSer.GetRoot("en-us");
            while (true)
            {
                var l = Console.ReadLine();
                if (l == "zh")
                {
                    Console.WriteLine(zhRoot["Title"]);
                    Console.WriteLine(zhRoot["Name"]);
                }
                else if (l == "en")
                {
                    Console.WriteLine(enRoot["Title"]);
                    Console.WriteLine(enRoot["Name"]);
                }
            }
        }
    }
}
