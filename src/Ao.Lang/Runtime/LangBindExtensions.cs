using System;
using System.Collections;
using System.Reflection;
using LExpression = System.Linq.Expressions;

namespace Ao.Lang.Runtime
{
    public static class LangBindExtensions
    {
        /// <summary>
        /// Binds a property of an object instance to a language key.
        /// </summary>
        public static ILangStrBox BindTo<T>(this LanguageManager langMgr, string key,
            T instance,
            LExpression.Expression<Func<T, object>> propertySelector,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            if (propertySelector is null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            if (propertySelector.Body is LExpression.MemberExpression me && me.Member is PropertyInfo info)
            {
                return BindTo(langMgr, key, instance, info, args, defaultValue, fixedCulture, noUpdate);
            }
            if (propertySelector.Body is LExpression.UnaryExpression ue &&
                ue.Operand is LExpression.MemberExpression ueme &&
                ueme.Member is PropertyInfo ueinfo)
            {
                return BindTo(langMgr, key, instance, ueinfo, args, defaultValue, fixedCulture, noUpdate);
            }
            throw new NotSupportedException($"Not support expression {propertySelector}");
        }

        /// <summary>
        /// Binds a property to a language key using a PropertyInfo instance.
        /// </summary>
        public static ILangStrBox BindTo(this LanguageManager langMgr, string key,
            object instance,
            PropertyInfo property,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var mul = new LangBindBox(property, instance);

            return CreateLangBox(langMgr, key, mul, args, defaultValue, fixedCulture, noUpdate);
        }

        /// <summary>
        /// Binds an IMulLang instance to a language key.
        /// </summary>
        public static ILangStrBox BindTo(this LanguageManager langMgr, string key, IMulLang lang,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            return CreateLangBox(langMgr, key, lang, args, defaultValue, fixedCulture, noUpdate);
        }

        /// <summary>
        /// Gets the current value associated with a language key.
        /// </summary>
        public static string GetValue(this LanguageManager langMgr, string key,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null)
        {
            return CreateLangBox(langMgr, key, args, defaultValue, fixedCulture, noUpdate: true).Value;
        }

        /// <summary>
        /// Creates a LangStrBox with the given parameters.
        /// </summary>
        public static ILangStrBox CreateLangBox(this LanguageManager langMgr, string key,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            return CreateLangBox(langMgr, key, null, args, defaultValue, fixedCulture, noUpdate);
        }

        /// <summary>
        /// Internal method for creating a LangStrBox with the given parameters and an optional IMulLang instance.
        /// </summary>
        private static ILangStrBox CreateLangBox(this LanguageManager langMgr, string key, IMulLang mulLang,
            IList args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            if (langMgr is null)
            {
                throw new ArgumentNullException(nameof(langMgr));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var box = new LangStrBox
            {
                Args = args,
                Key = key,
                DefaultValue = defaultValue,
                FixedCulture = fixedCulture,
                LangMgr = langMgr,
                MulLang = mulLang,
                NoUpdate = noUpdate
            };
            box.Init();
            return box;
        }
    }
}