using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface ILangSectionProvider
    {
        string GetSectionKey(Type type);

        string GetSectionKey(string baseName,string location);
    }
}
