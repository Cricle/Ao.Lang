namespace Ao.Lang.Generator
{
    public class LangBlock : LangIdentity, ILangBlock
    {
        public LangBlock()
        {
            CultureStringMapping = new CultureStringMapping();
        }

        public CultureStringMapping CultureStringMapping { get; }

    }
}
