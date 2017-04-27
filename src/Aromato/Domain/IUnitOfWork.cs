using System;

namespace Aromato.Domain
{
    /// <summary>
    /// Interface for the Unit of Work pattern. This hides the implementation
    /// of the Unit of Work to apply persistence ignorance to the entire domain.
    /// layer. Please see https://www.martinfowler.com/eaaCatalog/unitOfWork.html
    /// for more details.
    /// </summary>
    /// <remarks>
    /// If using Entity Framework, you can extend <see cref="Microsoft.EntityFrameworkCore.DbContext"/> and
    /// implement this interface.
    /// </remarks>
    /// <example>
    /// If using Entity Framework, see sample below.
    /// <code>
    /// public class MyContext : DbContext, IUnitOfWork {
    ///
    ///     public void Commit() {
    ///         SaveChanges();
    ///     }
    ///
    ///     public void Rollback() {
    ///         Dispose();
    ///     }
    ///
    /// }
    /// </code>
    /// </example>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persist changes to the underlying database implementation.
        /// </summary>
        /// <remarks>
        /// If any concurrency problem occurs or a transaction in the underlying database failed,
        /// this method should throw an exception.
        /// </remarks>
        void Commit();
        /// <summary>
        /// Rollback any uncommited changes in the underlying database implementation.
        /// </summary>
        void Rollback();
    }
}