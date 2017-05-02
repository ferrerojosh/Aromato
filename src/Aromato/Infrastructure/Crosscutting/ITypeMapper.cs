namespace Aromato.Infrastructure.Crosscutting
{
    /// <summary>
    /// Interface for a type mapper. It maps any object to another representation. For example,
    /// a data-transfer-object to an aggregate and vice-versa.
    /// </summary>
    /// <remarks>
    /// You can use the auto mapper library for this.
    /// </remarks>
    public interface ITypeMapper
    {
        /// <summary>
        /// Maps <typeparamref name="TSource"></typeparamref> to the specified <typeparamref name="TDestination"></typeparamref>.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The mapped type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped object.</returns>
        TDestination MapTo<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps the unknown source object to the specified <typeparam name="TTarget"></typeparam>.
        /// </summary>
        /// <typeparam name="TTarget">The mapped type.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped object.</returns>
        TTarget MapTo<TTarget>(object source);
    }
}
