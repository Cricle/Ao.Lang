using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using System;
using System.Globalization;

namespace Ao.Lang.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
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
                if (l=="zh")
                {
                    Console.WriteLine(zhRoot["Title"]);
                    Console.WriteLine(zhRoot["Name"]);
                }
                else if(l=="en")
                {
                    Console.WriteLine(enRoot["Title"]);
                    Console.WriteLine(enRoot["Name"]);
                }
            }
        }
    }
}
