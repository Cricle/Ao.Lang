using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Ao.Lang
{
    public interface ILanguageService : IEnumerable<ILanguageMetadata>, ICollectionChangeReproducible
    {
        string this[string key] { get; }
        string this[string key, params object[] args] { get; }
        IReadOnlyCollection<CultureInfo> SupportCultures { get; }
        ILanguageRoot GetRoot(CultureInfo cultureInfo);
        bool CultureIsSupport(CultureInfo cultureInfo);
        bool IsBuilt(CultureInfo cultureInfo);
        void Add(ILanguageMetadata item);
        void Clear();
        bool Remove(ILanguageMetadata item);

        ILanguageNode GetLangNode(CultureInfo cultureInfo);
        ILanguageNode EnsureGetLangNode(CultureInfo cultureInfo);
    }
}
