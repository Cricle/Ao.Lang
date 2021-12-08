using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
#if !NETSTANDARD1_1
using FastExpressionCompiler;
#endif

namespace Ao.Lang.Runtime
{
    public delegate void StringSetter(object instance, string value);

    public class LangBindBox : IMulLang
    {
        private static readonly Dictionary<PropertyInfo, StringSetter> propertySetters =
            new Dictionary<PropertyInfo, StringSetter>();

        public LangBindBox(PropertyInfo property, object instance)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));

#if !NETSTANDARD1_1
            if (!property.DeclaringType.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Property {property} declare type is not assignable from {instance}");
            }
#else
            var type= property.DeclaringType;
            var objType = typeof(object);
            var instanceType=instance.GetType();
            var ok = false;
            while (type!=null)
            {
                if (type== instanceType)
                {
                    ok= true;
                    break;
                }
                type = type.DeclaringType;
            }
            if (!ok)
            {
                throw new ArgumentException($"Property {property} declare type is not assignable from {instance}");
            }
#endif

            if (property.PropertyType!=typeof(string))
            {
                throw new ArgumentException($"Property {property} type is not string");
            }

            if (!propertySetters.TryGetValue(property, out var setter))
            {
                setter = BuildSetter(property);
                propertySetters[property] = setter;
            }
            StringSetter = setter;
        }

        public StringSetter StringSetter { get; }

        public PropertyInfo Property { get; }

        public object Instance { get; }

        public void ReceivedChanged(ILangStrBox box, LangValueChangeEventArgs args)
        {
            StringSetter(Instance, args.New);
        }

        private static StringSetter BuildSetter(PropertyInfo property)
        {
            var par1 = Expression.Parameter(typeof(object));
            var par2 = Expression.Parameter(typeof(string));

            var exp = Expression.Call( Expression.Convert(par1,property.DeclaringType),property.SetMethod, par2);
#if !NETSTANDARD1_1
            return Expression.Lambda(exp, par1, par2).CompileFast<StringSetter>();
#else
            return Expression.Lambda<StringSetter>(exp, par1, par2).Compile();
#endif
        }
    }
}
