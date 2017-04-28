using Aromato.Domain.Employee;

namespace Aromato.Application.Web.Data
{
    public class EmployeeWebData : IData<long, Employee>
    {
        public EmployeeWebData()
        {

        }

        public EmployeeWebData(string uniqueId, string firstName, string lastName, string middleName, string gender,
           string dateOfBirth, string email, string contactNo)
        {
            UniqueId = uniqueId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            ContactNo = contactNo;
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

        public IData<long, Employee> Fill(Employee entity)
        {
            Id = entity.Id;
            UniqueId = entity.UniqueId;
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