using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Ao.Lang
{
    public interface ILanguageNode : IConfigurationBuilder, ICollectionChangeReproducible, INotifyCollectionChanged, IList<ILanguageMetadata>, ICultureIdentity
    {
        bool IsBuilt { get; }
        ILanguageRoot Root { get; }

        event Action<ILanguageNode, ILanguageRoot> Rebuilt;
    }
}