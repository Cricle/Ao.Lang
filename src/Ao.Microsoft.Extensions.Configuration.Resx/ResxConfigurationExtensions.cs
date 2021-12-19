using Microsoft.Extensions.Configuration.Resx;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
namespace Microsoft.Extensions.Configuration
{
    public static class ResxConfigurationExtensions
    {
        public static IConfigurationBuilder AddResxFile(this IConfigurationBuilder builder, Action<ResxConfigurationSource> configureSource)
        {
#if NETSTANDARD2_0
            return builder.Add(configureSource);
#else
            var s = new ResxConfigurationSource();
            configureSource?.Invoke(s);
            builder.Add(s);
            return builder;
#endif
        }
        public static IConfigurationBuilder AddResxFile(this IConfigurationBuilder builder, string path)
        {
            return AddResxFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
        }
        public static IConfigurationBuilder AddResxFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddResxFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
        }
        public static IConfigurationBuilder AddResxFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddResxFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
        }
        public static IConfigurationBuilder AddResxStream(this IConfigurationBuilder builder, Stream stream)
        {
#if NET452
            var config = new ResxStreamConfigurataionSource(stream);
#else
            var config = new ResxStreamConfigurataionSource { Stream = stream };
#endif
            return builder.Add(config);
        }
        public static IConfigurationBuilder AddResxFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            return builder.AddResxFile(s =>
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

