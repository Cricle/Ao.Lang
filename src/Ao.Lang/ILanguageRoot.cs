using Microsoft.Extensions.Configuration;

namespace Ao.Lang
{
    public interface ILanguageRoot : IConfigurationRoot, ICultureIdentity
    {
        string this[string key, params object[] args] { get; }
    }
}
