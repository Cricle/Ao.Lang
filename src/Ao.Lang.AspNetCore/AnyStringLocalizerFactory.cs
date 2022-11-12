using Ao.Lang;
using Ao.Lang.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AnyStringLocalizerFactory : IStringLocalizerFactory, IHtmlLocalizerFactory
    {
        public AnyStringLocalizerFactory(ILangSectionProvider sectionProvider,
            IHttpContextAccessor httpContextAccessor,
            LanguageManager languageManager)
        {
            HttpContextAccessor = httpContextAccessor;
            SectionProvider = sectionProvider;
            LanguageManager= languageManager;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public ILangSectionProvider SectionProvider { get; }

        public LanguageManager LanguageManager { get; }

        public ILanguageRoot Root
        {
            get
            {
                if (HttpContextAccessor.HttpContext!=null)
                {
                    return HttpContextAccessor.HttpContext.Features.Get<ILanguageRoot>()??LanguageManager.Root;
                }
                return LanguageManager.Root;
            }
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var key = SectionProvider.GetSectionKey(resourceSource);
            return new AnyStringLocalizer(key, Root);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var key = SectionProvider.GetSectionKey(baseName,location);
            return new AnyStringLocalizer(key, Root);
        }

        IHtmlLocalizer IHtmlLocalizerFactory.Create(Type resourceSource)
        {
            var key = SectionProvider.GetSectionKey(resourceSource);
            return new AnyHtmlLocalizer(key, Root);
        }

        IHtmlLocalizer IHtmlLocalizerFactory.Create(string baseName, string location)
        {
            var key = SectionProvider.GetSectionKey(baseName,location);
            return new AnyHtmlLocalizer(key, Root);
        }
    }
}
