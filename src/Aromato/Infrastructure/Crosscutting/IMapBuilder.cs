namespace Aromato.Infrastructure.Crosscutting
{
    /// <summary>
    /// Fluent interface for mapping.
    /// </summary>
    /// <remarks>Taken from https://gist.github.com/ilyapalkin/8822638 </remarks>
    public interface IMapBuilder
    {
        /// <summary>
        /// Maps the specified source type instance to destination type instance.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <returns>Fluent interface for mapping.</returns>
        IMapBuilder Map(object source);

        /// <summary>
        /// Maps the specified earlier source type instances to destination type instance.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="destination">The destination object.</param>
        /// <returns>
        /// Instance of destination type.
        /// </returns>
        TDestination To<TDestination>(TDestination destination);

        /// <summary>
        /// Maps the specified earlier source type instances to destination type instance.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns>
        /// Instance of destination type.
        /// </returns>
        TDestination To<TDestination>();
    }
}