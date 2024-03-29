﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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

            if (!property.DeclaringType.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Property {property} declare type is not assignable from {instance}");
            }

            if (property.PropertyType != typeof(string))
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

            var exp = Expression.Call(Expression.Convert(par1, property.DeclaringType), property.SetMethod, par2);

            return Expression.Lambda<StringSetter>(exp, par1, par2)
                .Compile();
        }
    }
}