using Aromato.Application.Web.Data;
using Aromato.Domain.EmployeeAgg;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class EmployeeWebProfile : global::AutoMapper.Profile
    {
        public EmployeeWebProfile()
        {
            var mapper = CreateMap<Employee, EmployeeWebData>();

            // entity => web data
            mapper.ForMember(d => d.Id, mc => mc.MapFrom(e => e.Id));
            mapper.ForMember(d => d.UniqueId, mc => mc.MapFrom(e => e.UniqueId));
            mapper.ForMember(d => d.FirstName, mc => mc.MapFrom(e => e.FirstName));
            mapper.ForMember(d => d.MiddleName, mc => mc.MapFrom(e => e.MiddleName));
            mapper.ForMember(d => d.LastName, mc => mc.MapFrom(e => e.LastName));
            mapper.ForMember(d => d.Name, mc => mc.MapFrom(e => e.Name));
            mapper.ForMember(d => d.ContactNo, mc => mc.MapFrom(e => e.ContactNo));
            mapper.ForMember(d => d.Email, mc => mc.MapFrom(e => e.Email));
            mapper.ForMember(d => d.Gender, mc => mc.MapFrom(e => e.Gender.ToString()));
            mapper.ForMember(d => d.DateOfBirth, mc => mc.MapFrom(e => e.DateOfBirth.ToString("MM/dd/yyyy")));

            var punchMapper = CreateMap<Punch, PunchWebData>();

            punchMapper.ForMember(d => d.Type, mc => mc.MapFrom(p => p.Type.ToString()));
            punchMapper.ForMember(d => d.DateAdded, mc => mc.MapFrom(p => p.DateAdded.ToString("dddd, MMMM dd, HH:mm:ss tt")));
        }
    }
}
