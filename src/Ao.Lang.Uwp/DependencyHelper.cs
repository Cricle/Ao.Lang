using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Xaml;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    internal static class DependencyHelper
    {
        private static readonly Dictionary<Type, Dictionary<string, DependencyProperty>> dependencyProperties = new Dictionary<Type, Dictionary<string, DependencyProperty>>();

        public static DependencyProperty Get(Type type,string name)
        {
            if (!dependencyProperties.TryGetValue(type,out var props))
            {
                props = new Dictionary<string, DependencyProperty>();
                dependencyProperties[type] = props;
            }
            if (!props.TryGetValue(name,out var prop))
            {
                var propAct = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public);
                if (propAct != null)
                {
                    prop = (DependencyProperty)propAct.GetValue(null);
                    props[name] = prop;
                }
            }
            return prop;
        }
    }
}
