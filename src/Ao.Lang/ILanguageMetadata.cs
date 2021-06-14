using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Ao.Lang
{
    public interface ILanguageMetadata:ICultureIdentity,IEnumerable<IConfigurationSource>
    {
    }
}
