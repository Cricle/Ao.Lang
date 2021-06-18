using System.Collections.Generic;

namespace Ao.Lang.Generator.Test.Editor
{
    internal static class DataContains
    {
        public static IList<LangBlock> CreateLangBlocks(int count)
        {
            var list = new List<LangBlock>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(new LangBlock
                {
                    D = "d" + i,
                    C = "f" + i,
                    B = "m" + i,
                    A = "t" + i,
                    CultureStringMapping =
                    {
                        ["zh-cn"]="啊"+i,
                        ["en-us"]="a"+i
                    }
                });
            }
            return list;
        }
    }
}
