namespace Aromato.Application
{
    public interface IEmployeeService : IReadService<long>
    {
        IData Punch(long id);
        void CreateEmployee(IData employeeData);
        void ChangeEmail(long id, string email);
        void ChangeContactNo(long id, string contactNo);
    }
}