namespace Aromato.Application
{
    public interface IEmployeeService<TKey> : IReadService<TKey>
    {
        IData Punch(TKey id);
        void CreateEmployee(IData employeeData);
        void ChangeEmail(TKey id, string email);
        void ChangeContactNo(TKey id, string contactNo);
    }
}