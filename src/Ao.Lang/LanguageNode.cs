using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace Ao.Lang
{
    public class LanguageNode : ObservableCollection<ILanguageMetadata>, IConfigurationBuilder, ILanguageNode
    {
        public LanguageNode(CultureInfo culture)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
            CollectionChanged += LanguageNode_CollectionChanged;
            propertites = new Dictionary<string, object>(0);
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
        public IDictionary<string, object> Properties => propertites;

        public IList<IConfigurationSource> Sources => this.SelectMany(x => x).ToArray();

        public event Action<ILanguageNode, ILanguageRoot> Rebuilt;

        public void ReBuild()
        {
            root = new Lazy<ILanguageRoot>(Build, true);
        }

        private ILanguageRoot Build()
        {
            if (root.IsValueCreated && root.Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
            var builder = new LanguageBuilder(Culture);
            foreach (var item in this.SelectMany(s => s))
            {
                builder.Add(item);
            }
            var rt = builder.Build();
            Rebuilt?.Invoke(this, rt);
            return rt;
        }

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            var m = new DefaultLanguageMetadata(Culture, 1) { source };
            Add(m);
            return this;
        }

        IConfigurationRoot IConfigurationBuilder.Build()
        {
            throw new NotSupportedException("Please ILanguageRoot Build()!");
        }
    }
}