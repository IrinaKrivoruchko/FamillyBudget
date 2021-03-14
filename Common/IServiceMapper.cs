namespace Common
{
    public interface IServiceMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
