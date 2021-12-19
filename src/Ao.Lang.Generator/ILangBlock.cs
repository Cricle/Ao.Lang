namespace Ao.Lang.Generator
{
    public interface ILangBlock : ILangIdentity
    {
        CultureStringMapping CultureStringMapping { get; }
    }
}
