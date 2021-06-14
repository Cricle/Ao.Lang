using Microsoft.Extensions.Configuration;

namespace Ao.Lang
{
    public interface ILanguageBuilder : IConfigurationBuilder, ICultureIdentity
    {

    }
}
