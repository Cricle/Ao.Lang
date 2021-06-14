using Microsoft.Extensions.Configuration;

namespace Ao.Lang
{
    public interface ILanguageProvider : IConfigurationProvider, ICultureIdentity
    {
    }
}
