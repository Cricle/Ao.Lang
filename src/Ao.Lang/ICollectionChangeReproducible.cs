namespace Ao.Lang
{
    public interface ICollectionChangeReproducible : IReproducible
    {
        bool ReBuildIfCollectionChanged { get; set; }
    }

}
