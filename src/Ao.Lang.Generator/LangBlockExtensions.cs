using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ao.Lang.Generator
{
    public static class LangBlockExtensions
    {
        public static IReadOnlyDictionary<CultureInfo, IReadOnlyDictionary<TLangBlock, string>> ToCultureMap<TLangBlock>(this IEnumerable<TLangBlock> blocks)
            where TLangBlock : ILangBlock
        {
            if (blocks is null)
            {
                throw new ArgumentNullException(nameof(blocks));
            }

            var map = new Dictionary<string, Dictionary<TLangBlock, string>>();
            foreach (var item in blocks)
            {
                foreach (var cultrueMap in item.CultureStringMapping)
                {
                    if (!map.TryGetValue(cultrueMap.Key, out var langMap))
                    {
                        langMap = new Dictionary<TLangBlock, string>();
                        map.Add(cultrueMap.Key, langMap);
                    }
                    langMap[item] = cultrueMap.Value;
                }
            }
            return map.ToDictionary(x => new CultureInfo(x.Key), x => (IReadOnlyDictionary<TLangBlock, string>)(x.Value));
        }
    }
}
