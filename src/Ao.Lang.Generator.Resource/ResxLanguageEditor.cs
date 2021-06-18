using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;

namespace Ao.Lang.Generator.Editor
{
    public class ResxLanguageEditor<TLangBlock> : PhysicalLanguageEditor<TLangBlock>
        where TLangBlock : ILangBlock
    {
        public const string ResxDesignFileExtensions = "resxi18nd";
        public const string ResxDesignFileNameTemplate = "{0}." + ResxDesignFileExtensions;
        public const string ResxCompiledFileExtensions = "resxi18n";
        public const string ResxCompiledFileNameTemplate = "{0}.{1}." + ResxCompiledFileExtensions;

        public ResxLanguageEditor(DirectoryInfo workspace)
            : base(workspace)
        {
        }

        public override string DesignFileNameTemplate => ResxDesignFileNameTemplate;

        public override string CompiledFileNameTemplate => ResxCompiledFileNameTemplate;

        public override string DesignFileExtensions => ResxDesignFileExtensions;

        public override string CompiledFileExtensions => ResxCompiledFileExtensions;

        protected override void AddCore(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged)
        {
            configurationBuilder.AddResourceFile(filePath, optional, reloadOnChanged);
        }

        protected override ILanguageScope<TLangBlock> CreateScope(FileInfo file, string identity)
        {
            return new ResxLanguageScope(file, identity);
        }
        class ResxLanguageScope : JsonStoreLanguageScope<TLangBlock>
        {
            public ResxLanguageScope(FileInfo physicalFile, string identity)
                : base(physicalFile)
            {
                Identity = identity;
            }

            public string Identity { get; }

            protected override void CompileItem(ILangIdentityCompiler compiler, CultureInfo culture, Dictionary<string, string> datas)
            {
                var fn = string.Format(ResxCompiledFileNameTemplate, Identity, culture.Name.ToLower());
                var path = Path.Combine(PhysicalFile.DirectoryName, fn);
                using (var fs = File.Open(path, FileMode.Create))
                using (var rs = new ResourceWriter(fs))
                {
                    foreach (var item in datas)
                    {
                        rs.AddResource(item.Key, item.Value);
                    }
                    rs.Close();
                }
            }
        }
    }
}
