using Microsoft.Extensions.Configuration;

namespace Ao.Lang.Generator.Editor
{
    public interface ILangSourceProvider
    {
        void Add(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged);
    }
}
