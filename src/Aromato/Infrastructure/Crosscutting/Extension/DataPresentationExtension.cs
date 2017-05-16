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
                cfg.AddProfile<InventoryWebProfile>();
                cfg.AddProfile<RoleWebProfile>();
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
    }
}
