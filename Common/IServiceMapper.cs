namespace Common
{
    public interface IServiceMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Merge<TSource, TDestination>(TSource src, TDestination dest);
    }
}
