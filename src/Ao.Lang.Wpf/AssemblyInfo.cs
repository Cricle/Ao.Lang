#if !UWP_PLATFORM

using System.Runtime.CompilerServices;
using System.Windows.Markup;

[assembly: InternalsVisibleTo("Ao.Lang.Wpf.Test")]

[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang")]
#if WPF_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Wpf")]
#elif UNO_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Uno")]
#elif UWP_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Uwp")]
#endif

#endif