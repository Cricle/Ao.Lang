using Ao.Lang.Generator.Editor;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Ao.Lang.Generator.BenchmarkTest
{
    [MemoryDiagnoser]
    public class LoadLanguage : LanguageBenchmarkBase
    {
        public LoadLanguage()
            : base(100)
        {
        }

        [Benchmark(Baseline = true)]
        public void ReadJson()
        {
            var fi = File.ReadAllText(jsonCompiledFile);
            _ = JsonHelper.Deserialize<Dictionary<string, string>>(fi);
        }
        [Benchmark]
        public void ReadResx()
        {
            var dic = new Dictionary<string, string>();
            using (var sr = new ResourceReader(resxCompiledFile))
            {
                var enu = sr.GetEnumerator();
                while (enu.MoveNext())
                {
                    dic.Add(enu.Key.ToString(), enu.Value.ToString());
                }
            }
        }
    }
}
