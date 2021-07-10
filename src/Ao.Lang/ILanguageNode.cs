using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace Ao.Lang
{
    public interface ILanguageNode : IConfigurationBuilder, ICollectionChangeReproducible, INotifyCollectionChanged, IList<ILanguageMetadata>, ICollection<ILanguageMetadata>, IEnumerable<ILanguageMetadata>, ICultureIdentity
    {
        bool IsBuilt { get; }
        ILanguageRoot Root { get; }

        event Action<ILanguageNode, ILanguageRoot> Rebuilt;
    }
}