using System;
#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public interface IControlLang
    {
        Type SupportType { get; }

        void Bind(in ControlLangBindContext context);
    }
}
