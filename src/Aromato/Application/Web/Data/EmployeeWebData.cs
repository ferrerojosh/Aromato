using Aromato.Domain.Employee;

namespace Aromato.Application.Web.Data
{
    public class EmployeeWebData : IData<long, Employee>
    {
        public EmployeeWebData()
        {

        }

        public EmployeeWebData(string firstName, string lastName, string middleName, string gender,
           string dateOfBirth, string email, string contactNo)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            ContactNo = contactNo;
        }

        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string Name { get; private set; }
        public string ContactNo { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public string DateOfBirth { get; private set; }

        public IData<long, Employee> Fill(Employee entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            MiddleName = entity.MiddleName;
            Name = entity.Name;
            ContactNo = entity.ContactNo;
            Email = entity.Email;
            Gender = entity.Gender.ToString();
            DateOfBirth = entity.DateOfBirth.ToString("MM/dd/yyyy");
            return this;
        }
    }
}