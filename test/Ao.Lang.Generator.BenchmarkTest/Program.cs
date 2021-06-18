using System;

namespace Ao.Lang.Generator.BenchmarkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run();
        }
    }
}
