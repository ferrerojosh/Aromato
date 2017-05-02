using System.Collections.Generic;
using Aromato.Application;
using Aromato.Domain;

namespace Aromato.Infrastructure.Crosscutting.Extension
{
    /// <summary>
    /// Data projection extensions 
    /// </summary>
    public static class DataProjectionExtension
    {
        /// <summary>
        /// An extension method for entity types to be presented as an instance of <see cref="IData"/>.
        /// </summary>
        /// <typeparam name="TData">The type of data implementing <see cref="IData"/></typeparam>
        /// <param name="item">The item to be presented.</param>
        /// <returns>The presented data.</returns>
        public static TData AsData<TData>(this IEntity<long> item)
            where TData : IData
        {
            return TypeMapperFactory.Instance.CreateMapper().MapTo<TData>(item);
        }

        /// <summary>
        /// An extension method for entity types to be presented as an instance of <see cref="ICollection{TData}"/>
        /// </summary>
        /// <typeparam name="TData">The type of data implementing <see cref="IData"/></typeparam>
        /// <param name="items">The collection of items to be presented implementing <see cref="ICollection{TData}"/></param>
        /// <returns></returns>
        public static ICollection<TData> AsCollectionData<TData>(this ICollection<IEntity<long>> items)
            where TData : IData
        {
            return TypeMapperFactory.Instance.CreateMapper().MapTo<ICollection<TData>>(items);
        }

        /// <summary>
        /// An extension method for entity types to be presented as an instance of <see cref="IEnumerable{TData}"/>
        /// </summary>
        /// <typeparam name="TData">The type of data implementing <see cref="IData"/></typeparam>
        /// <param name="items">The collection of items to be presented implementing <see cref="IEnumerable{TData}"/></param>
        /// <returns></returns>
        public static IEnumerable<TData> AsEnumerableData<TData>(this IEnumerable<IEntity<long>> items)
            where TData : IData
        {
            return TypeMapperFactory.Instance.CreateMapper().MapTo<ICollection<TData>>(items);
        }
    }
}
