using Ao.Lang;
using Ao.Lang.Lookup;
using Ao.Lang.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LangServiceExtensions
    {
        public static IServiceCollection AddLang(this IServiceCollection services,Action<LangOptions> langOptions=null)
        {
            langOptions ??= _ => { };
            services.AddOptions();
            services.Configure(langOptions);
            services.AddHttpContextAccessor();
            services.AddSingleton(x=> x.GetRequiredService<IOptions<LangOptions>>().Value.LanguageManager);
            services.AddScoped(x => x.GetRequiredService<LanguageManager>().LangService);
            services.AddScoped(x =>
            {
                var opt = x.GetRequiredService<IOptions<LangOptions>>();
                var langSer = opt.Value.LanguageManager.LangService;
                var httpAsstor = x.GetRequiredService<IHttpContextAccessor>();
                var selectCulture = opt.Value.Default;
                var ctx = httpAsstor.HttpContext;
                if (ctx!=null)
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
                return langSer.GetRoot(selectCulture) ??
                (opt.Value.NotFoundUseDefault ? langSer.GetRoot(opt.Value.Default) : null);
            });
            return services;
        }
    }
}
