using System.Collections.Generic;
using Aromato.Application;
using Aromato.Domain;
using Aromato.Infrastructure.Crosscutting.AutoMapper.Profile;

namespace Aromato.Infrastructure.Crosscutting.Extension
{
    /// <summary>
    /// Data projection extensions.
    /// </summary>
    public static class DataPresentationExtension
    {

        private static readonly Mapper Mapper = new Mapper();

        static DataPresentationExtension()
        {
            global::AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EmployeeWebProfile>();
                cfg.AddProfile<PunchWebProfile>();
                cfg.AddProfile<InventoryWebProfile>();
                cfg.AddProfile<ItemWebProfile>();
            });
        }

        /// <summary>
        /// An extension method for entity types to be presented as an instance of <see cref="IData"/>.
        /// </summary>
        /// <typeparam name="TData">The type of data implementing <see cref="IData"/></typeparam>
        /// <param name="item">The item to be presented.</param>
        /// <returns>The presented data.</returns>
        public static TData AsData<TData>(this IEntity<long> item)
            where TData : IData
        {
            return Mapper.MapTo<TData>(item);
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
            return Mapper.MapTo<ICollection<TData>>(items);
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
            return Mapper.MapTo<IEnumerable<TData>>(items);
        }

        /// <summary>
        /// An extension method for data transfer object types to be presented as an instance of <see cref="IEntity{TKey}"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity implementing <see cref="IEntity{TKey}"/></typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="item">The presentation data implementing <see cref="IData"/></param>
        /// <returns>The entity created from the presentation data.</returns>
        public static TEntity AsEntity<TKey, TEntity>(this IData item)
            where TEntity : IEntity<TKey>
        {
            return Mapper.MapTo<TEntity>(item);
        }

        /// <summary>
        /// An extension method for data transfer object types to be presented as an instance of <see cref="ICollection{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity implementing <see cref="IEntity{TKey}"/></typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="items">The presentation data implementing <see cref="ICollection{IData}"/></param>
        /// <returns>A collection of entities created from the presentation collection.</returns>
        public static ICollection<TEntity> AsCollectionEntity<TKey, TEntity>(this ICollection<IData> items)
            where TEntity : IEntity<TKey>
        {
            return Mapper.MapTo<ICollection<TEntity>>(items);
        }

        /// <summary>
        /// An extension method for data transfer object types to be presented as an instance of <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity implementing <see cref="IEntity{TKey}"/></typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="items">The presentation data implementing <see cref="IEnumerable{IData}"/></param>
        /// <returns>An enumerable entities created from the presentation enumerable.</returns>
        public static IEnumerable<TEntity> AsEnumerableEntity<TKey, TEntity>(this IEnumerable<IData> items)
            where TEntity : IEntity<TKey>
        {
            return Mapper.MapTo<IEnumerable<TEntity>>(items);
        }
    }
}
