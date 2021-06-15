using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration.Resources;

namespace Microsoft.Extensions.Configuration
{
    public static class ResourceConfigurationExtensions
    {
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, Action<ResourceConfigurationSource> configureSource)
        {
#if !NET452
            return builder.Add(configureSource);
#else
            var s=new ResourceConfigurationSource();
            configureSource?.Invoke(s);
            builder.Add(s);
            return builder;
#endif
        }
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, string path)
        {
            return AddResourceFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
        }
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddResourceFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
        }
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddResourceFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
        }
#if NET452
        public static IConfigurationBuilder AddResourceStream(this IConfigurationBuilder builder,Stream stream)
        {
            var source = new ResourceStreamConfigurataionSource(stream);
            return builder.Add(source);
        }
#else
        public static IConfigurationBuilder AddResourceStream(this IConfigurationBuilder builder, Stream stream)
        {
            return builder.Add<ResourceStreamConfigurataionSource>(x => x.Stream = stream);
        }
#endif
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            return builder.AddResourceFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }
    }
}

