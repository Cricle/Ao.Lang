using System.IO;

namespace Microsoft.Extensions.Configuration.Resources
{
    public class ResourceStreamConfigurationProvider : StreamConfigurationProvider
    {
        public ResourceStreamConfigurationProvider(StreamConfigurationSource source)
            : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = ResourceHelper.GetData(stream);
        }
    }

    public class ResourceStreamConfigurataionSource : StreamConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResourceStreamConfigurationProvider(this);
        }
    }

    public class ResourceConfigurationProvider : FileConfigurationProvider
    {
        public ResourceConfigurationProvider(FileConfigurationSource source)
            : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = ResourceHelper.GetData(stream);
        }
    }

    public class ResourceConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResourceConfigurationProvider(this);
        }
    }
}