using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace Ao.Lang
{
    public class LanguageNode : ObservableCollection<ILanguageMetadata>, IConfigurationBuilder, ICultureIdentity, ILanguageNode
    {
        public LanguageNode(CultureInfo culture)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
            CollectionChanged += LanguageNode_CollectionChanged;
            propertites = new Dictionary<string, object>();
            ReBuild();
        }

        private void LanguageNode_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Move && ReBuildIfCollectionChanged)
            {
                ReBuild();
            }
        }
        private readonly Dictionary<string, object> propertites;
        private Lazy<ILanguageRoot> root;
        public ILanguageRoot Root => root.Value;
        public bool IsBuilt => root.IsValueCreated;
        public CultureInfo Culture { get; }
        public bool ReBuildIfCollectionChanged { get; set; }
#if NETSTANDARD2_0||NET461
        public IDictionary<string, object> Properties => propertites;

        public IList<IConfigurationSource> Sources => this.SelectMany(x => x).ToArray();
#else
        public Dictionary<string, object> Properties => propertites;

        public IEnumerable<IConfigurationSource> Sources => this.SelectMany(x => x).ToArray();

#endif


        public void ReBuild()
        {
            root = new Lazy<ILanguageRoot>(Build, true);
        }

        private ILanguageRoot Build()
        {
            var builder = new LanguageBuilder(Culture);
            var sources = this.SelectMany(s => s).ToArray();
            foreach (var item in sources)
            {
                builder.Add(item);
            }
            return builder.Build();
        }

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            var m = new DefaultLanguageMetadata(Culture) { source };
            m.TrimExcess();
            Add(m);
            return this;
        }

        IConfigurationRoot IConfigurationBuilder.Build()
        {
            throw new NotSupportedException("Please use Root property!");
        }
    }
}
