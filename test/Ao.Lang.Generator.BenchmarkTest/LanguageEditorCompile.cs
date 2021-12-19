using Ao.Lang.Generator.Editor;
using BenchmarkDotNet.Attributes;

namespace Ao.Lang.Generator.BenchmarkTest
{
    [MemoryDiagnoser]
    public class LanguageEditorCompile : LanguageBenchmarkBase
    {
        public LanguageEditorCompile()
            : base(100)
        {
        }

        [Benchmark(Baseline = true)]
        public void JsonCompile()
        {
            new JsonLanguageEditor<LangBlock>(Folder).GetScope("testjson").Compile(LangIdentityCompiler.Default);
        }
        [Benchmark]
        public void ResxCompile()
        {
            new ResxLanguageEditor<LangBlock>(Folder).GetScope("testresx").Compile(LangIdentityCompiler.Default);
        }
    }
}
