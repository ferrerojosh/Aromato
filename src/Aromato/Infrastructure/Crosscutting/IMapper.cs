namespace Aromato.Infrastructure.Crosscutting
{
    /// <summary>
    /// Type mapping api
    /// </summary>
    /// <remarks>Taken from https://gist.github.com/ilyapalkin/8822638 </remarks>
    public interface IMapper
    {
        /// <summary>
        /// Maps the specified source type instance to destination type instance.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Destination type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// Instance of destination type.
        /// </returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps the specified source type instance to destination type instance.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination instance.</typeparam>
        /// <param name="source">The source instance.</param>
        /// <returns>
        /// Instance of destination type.
        /// </returns>
        TDestination MapTo<TDestination>(object source);

        /// <summary>
        /// Maps the specified source type instance to destination type instance.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <returns>Fluent interface for mapping.</returns>
        IMapBuilder Map(object source);
    }

}