using Ao.Lang;
using Ao.Lang.Lookup;
using Ao.Lang.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class DefaultLangSectionProvider : ILangSectionProvider
    {
        public static readonly DefaultLangSectionProvider Instance = new DefaultLangSectionProvider();

        public string GetSectionKey(Type type)
        {
            return type.FullName;
        }

        public string GetSectionKey(string baseName, string location)
        {
            return baseName + "," + location;
        }
    }
    public static class LangServiceExtensions
    {
        public static IServiceCollection AddLangLocalization(this IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizerFactory, AnyStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            return services;
        }
        public static IMvcBuilder AddLangMvcLocalization(this IMvcBuilder mvc, Action<MvcDataAnnotationsLocalizationOptions> setupAction=null)
        {
            mvc.Services.TryAddSingleton<IHtmlLocalizerFactory>(x => x.GetRequiredService<AnyStringLocalizerFactory>());
            mvc.Services.TryAdd(ServiceDescriptor.Transient(typeof(IHtmlLocalizer<>), typeof(AnyHtmlLocalizer<>)));
            mvc.Services.TryAdd(ServiceDescriptor.Transient<IViewLocalizer, ViewLocalizer>());
            //if (setupAction != null)
            //{
            //    mvc.Services.Configure(setupAction);
            //}
            //else
            //{
            //    mvc.Services.TryAddEnumerable(
            //        ServiceDescriptor.Transient
            //        <IConfigureOptions<MvcDataAnnotationsLocalizationOptions>,
            //        AnyMvcDataAnnotationsLocalizationOptionsSetup>());
            //}
            return mvc;
        }
        public static IServiceCollection AddLang(this IServiceCollection services,Action<LangOptions> langOptions=null)
        {
            langOptions ??= _ => { };
            services.AddOptions();
            services.Configure(langOptions);
            services.AddHttpContextAccessor();
            services.AddSingleton(x=>
            {
                var opt = x.GetRequiredService<IOptions<LangOptions>>().Value;
                var mgr = opt.LanguageManager;
                if (string.IsNullOrEmpty(opt.Default))
                {
                    mgr.CultureInfo = mgr.LangService.SupportCultures[0];
                }
                else
                {
                    mgr.SetCulture(opt.Default);
                }
                return mgr;
            });
            services.AddScoped(x => x.GetRequiredService<LanguageManager>().LangService);
            services.AddScoped(GetRoot);

            services.AddSingleton<ILangSectionProvider>(DefaultLangSectionProvider.Instance);

            return services;
        }

        private static ILanguageRoot GetRoot(IServiceProvider x)
        {
            var opt = x.GetRequiredService<IOptions<LangOptions>>();
            var langSer = opt.Value.LanguageManager.LangService;
            var httpAsstor = x.GetRequiredService<IHttpContextAccessor>();
            var selectCulture = opt.Value.Default;
            var ctx = httpAsstor.HttpContext;
            if (ctx != null)
            {
                var ok = false;
                if (!string.IsNullOrEmpty(opt.Value.QueryKey) &&
                    ctx.Request.Query.TryGetValue(opt.Value.QueryKey, out var query) &&
                    CultureInfoHelper.IsAvaliableCulture(query.ToString()))
                {
                    selectCulture = query.ToString();
                    ok = true;
                }
                if (!ok)
                {
                    if (opt.Value.UseAcceptLanguage)
                    {
                        var provider = new AcceptLanguageHeaderRequestCultureProvider();
                        var data = provider.DetermineProviderCultureResult(ctx).GetAwaiter().GetResult();
                        for (int i = 0; i < data.Cultures.Count; i++)
                        {
                            var value = data.Cultures[i].Value;
                            if (langSer.CultureIsSupport(value))
                            {
                                selectCulture = value;
                                ok = true;
                                break;
                            }
                        }
                    }
                    if (!ok)
                    {
                        if (!string.IsNullOrEmpty(opt.Value.RouteKey) &&
                            ctx.Request.RouteValues.TryGetValue(opt.Value.RouteKey, out var routeLang) &&
                            routeLang != null &&
                            CultureInfoHelper.IsAvaliableCulture(routeLang.ToString()))
                        {
                            selectCulture = routeLang.ToString();
                        }
                    }
                }
            }
            var root= langSer.GetRoot(selectCulture) ??
            (opt.Value.NotFoundUseDefault ? langSer.GetRoot(opt.Value.Default) : null);

            httpAsstor.HttpContext.Features.Set(root);
            return root;
        }
    }
    internal sealed class AnyMvcDataAnnotationsLocalizationOptionsSetup : IConfigureOptions<MvcDataAnnotationsLocalizationOptions>
    {
        /// <inheritdoc />
        public void Configure(MvcDataAnnotationsLocalizationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.DataAnnotationLocalizerProvider = (modelType, stringLocalizerFactory) =>
                stringLocalizerFactory.Create(modelType);
        }
    }
}
