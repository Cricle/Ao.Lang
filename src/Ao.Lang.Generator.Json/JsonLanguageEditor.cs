using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Ao.Lang.Generator.Editor
{
    public class JsonLanguageEditor<TLangBlock> : PhysicalLanguageEditor<TLangBlock>
        where TLangBlock : ILangBlock
    {
        public static readonly string JsonDesignFileExtensions = "jsoni18nd";
        public static readonly string JsonDesignFileNameTemplate = "{0}." + JsonDesignFileExtensions;
        public static readonly string JsonCompiledFileExtensions = "jsoni18n";
        public static readonly string JsonCompiledFileNameTemplate = "{0}.{1}." + JsonCompiledFileExtensions;

        public JsonLanguageEditor(DirectoryInfo workspace)
            : base(workspace)
        {
        }

        public override string DesignFileNameTemplate => JsonDesignFileNameTemplate;

        public override string CompiledFileNameTemplate => JsonCompiledFileNameTemplate;

        public override string DesignFileExtensions => JsonDesignFileExtensions;

        public override string CompiledFileExtensions => JsonCompiledFileExtensions;

        protected override void AddCore(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged)
        {
            configurationBuilder.AddJsonFile(filePath, optional, reloadOnChanged);
        }

        protected override ILanguageScope<TLangBlock> CreateScope(FileInfo file, string identity)
        {
            return new JsonLanguageScope(file, identity);
        }

        class JsonLanguageScope : JsonStoreLanguageScope<TLangBlock>
        {
            public JsonLanguageScope(FileInfo physicalFile, string identity)
                : base(physicalFile)
            {
                Identity = identity;
            }

            public string Identity { get; }

            protected override void CompileItem(ILangIdentityCompiler compiler, CultureInfo culture, Dictionary<string, string> datas)
            {
                var content = JsonHelper.Serialize(datas);
                var fn = string.Format(JsonCompiledFileNameTemplate, Identity, culture.Name.ToLower());
                var path = Path.Combine(PhysicalFile.DirectoryName, fn);
                File.WriteAllText(path, content);
            }
        }
    }
}
