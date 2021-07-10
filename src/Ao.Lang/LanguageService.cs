using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Ao.Lang
{
    public class LanguageService : ILanguageService
    {
        private bool reBuildIfCollectionChanged;
        private readonly Dictionary<CultureInfo, ILanguageNode> cultureToLangs = new Dictionary<CultureInfo, ILanguageNode>();
        private readonly List<ILanguageMetadata> sources = new List<ILanguageMetadata>();
        public static ILanguageService Default { get; } = new LanguageService();

        public string this[string key]
        {
            get => GetRoot(CultureInfo.CurrentCulture)?[key];
        }
        public string this[string key, params object[] args]
        {
            get
            {
                var val = GetRoot(CultureInfo.CurrentCulture)?[key];
                if (args == null || string.IsNullOrEmpty(val))
                {
                    return val;
                }
                return string.Format(val, args);
            }
        }
        public bool ReBuildIfCollectionChanged
        {
            get => reBuildIfCollectionChanged;
            set
            {
                reBuildIfCollectionChanged = value;
                foreach (var item in cultureToLangs.Values)
                {
                    item.ReBuildIfCollectionChanged = value;
                }
                ReBuildIfCollectionChangedValueChanged?.Invoke(this, value);
            }
        }
        public IReadOnlyCollection<CultureInfo> SupportCultures => cultureToLangs.Keys.ToArray();

        public int Count => sources.Count;

        public event Action<LanguageService, bool> ReBuildIfCollectionChangedValueChanged;

        public ILanguageRoot GetRoot(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (cultureToLangs.TryGetValue(cultureInfo, out var node))
            {
                return node.Root;
            }
            return null;
        }
        public bool IsBuilt(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (cultureToLangs.TryGetValue(cultureInfo, out var node))
            {
                return node.IsBuilt;
            }
            return false;
        }
        public bool CultureIsSupport(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            return cultureToLangs.ContainsKey(cultureInfo);
        }
        public void ReBuild()
        {
            foreach (var item in cultureToLangs.Values)
            {
                item.ReBuild();
            }
        }
        public void Add(ILanguageMetadata item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var node = EnsureGetLangNode(item.Culture);
            node.Add(item);
            sources.Add(item);
        }
        public ILanguageNode GetLangNode(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (cultureToLangs.TryGetValue(cultureInfo, out var node))
            {
                return node;
            }
            return null;
        }
        public ILanguageNode EnsureGetLangNode(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            var node = GetLangNode(cultureInfo);
            if (node == null)
            {
                node = new LanguageNode(cultureInfo)
                {
                    ReBuildIfCollectionChanged = reBuildIfCollectionChanged
                };
                cultureToLangs.Add(cultureInfo, node);
            }
            return node;
        }

        public void Clear()
        {
            cultureToLangs.Clear();
            sources.Clear();
            sources.TrimExcess();
        }

        public bool Remove(ILanguageMetadata item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (cultureToLangs.TryGetValue(item.Culture, out var node))
            {
                node.Remove(item);
                if (node.Count == 0)
                {
                    cultureToLangs.Remove(item.Culture);
                }
            }
            return sources.Remove(item);
        }

        public IEnumerator<ILanguageMetadata> GetEnumerator()
        {
            return sources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sources.GetEnumerator();
        }
    }
}
