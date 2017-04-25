namespace Aromato.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}