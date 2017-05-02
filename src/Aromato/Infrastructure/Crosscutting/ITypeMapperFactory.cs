namespace Aromato.Infrastructure.Crosscutting
{
    /// <summary>
    /// Interface for a type mapper.
    /// </summary>
    public interface ITypeMapperFactory
    {
        /// <summary>
        /// Creates an instance of a type mapper.
        /// </summary>
        /// <returns>The type mapper instance.</returns>
        ITypeMapper CreateMapper();
    }
}
