namespace Aromato.Application
{
    public interface IEmployeeService : IReadService<long>
    {
        IData Punch(string uniqueId);
        void CreateEmployee(IData employeeData);
        void ChangeEmail(long id, string email);
        void ChangeContactNo(long id, string contactNo);
        IData RetrieveByUniqueId(string uniqueId);
    }
}