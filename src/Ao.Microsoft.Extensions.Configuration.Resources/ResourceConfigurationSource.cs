using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration.Resources
{
#if !NET452

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
#else
    public class ResourceStreamConfigurationProvider : ConfigurationProvider
    {
        public ResourceStreamConfigurationProvider(ResourceStreamConfigurataionSource source)
        {
            ConfigurataionSource = source;
        }

        public ResourceStreamConfigurataionSource ConfigurataionSource { get; }

        public override void Load()
        {
            Data = ResourceHelper.GetData(ConfigurataionSource.Stream);
        }
    }
    public class ResourceStreamConfigurataionSource : IConfigurationSource
    {
        public ResourceStreamConfigurataionSource(Stream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public Stream Stream { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResourceStreamConfigurationProvider(this);
        }
    }
#endif
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
