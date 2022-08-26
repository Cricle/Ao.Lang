#if !UWP_PLATFORM
#if AVALONIAUI_PLATFORM
using Avalonia.Metadata;
#else
using System.Windows.Markup;
#endif
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Ao.Lang.Wpf.Test")]

[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang")]
#if !AVALONIAUI_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Runtime", AssemblyName ="Ao.Lang")]
#endif
#if WPF_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Wpf")]
#elif UNO_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Uno")]
#elif UWP_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.Uwp")]
#elif AVALONIAUI_PLATFORM
[assembly: XmlnsDefinition("https://github.com/Cricle/Ao.Lang", "Ao.Lang.AvaloniaUI")]
#endif

#endif