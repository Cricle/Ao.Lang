using Microsoft.Extensions.Configuration.Resources;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Microsoft.Extensions.Configuration
{
    public static class ResourceConfigurationExtensions
    {
        public static IConfigurationBuilder AddResourceFile(this IConfigurationBuilder builder, Action<ResourceConfigurationSource> configureSource)
        {
            var s = new ResourceConfigurationSource();
            configureSource?.Invoke(s);
            builder.Add(s);
            return builder;
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

        public static IConfigurationBuilder AddResourceStream(this IConfigurationBuilder builder, Stream stream)
        {
            return builder.Add<ResourceStreamConfigurataionSource>(x => x.Stream = stream);
        }

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