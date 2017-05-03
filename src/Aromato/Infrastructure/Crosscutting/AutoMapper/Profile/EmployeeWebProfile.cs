using System;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class EmployeeWebProfile : global::AutoMapper.Profile
    {
        public EmployeeWebProfile()
        {
            var mapperRight = CreateMap<Employee, EmployeeWebData>();

            // entity => web data
            mapperRight.ForMember(d => d.Id, mc => mc.MapFrom(e => e.Id));
            mapperRight.ForMember(d => d.UniqueId, mc => mc.MapFrom(e => e.UniqueId));
            mapperRight.ForMember(d => d.FirstName, mc => mc.MapFrom(e => e.FirstName));
            mapperRight.ForMember(d => d.MiddleName, mc => mc.MapFrom(e => e.MiddleName));
            mapperRight.ForMember(d => d.LastName, mc => mc.MapFrom(e => e.LastName));
            mapperRight.ForMember(d => d.Name, mc => mc.MapFrom(e => e.Name));
            mapperRight.ForMember(d => d.ContactNo, mc => mc.MapFrom(e => e.ContactNo));
            mapperRight.ForMember(d => d.Email, mc => mc.MapFrom(e => e.Email));
            mapperRight.ForMember(d => d.Gender, mc => mc.MapFrom(e => e.Gender.ToString()));
            mapperRight.ForMember(d => d.DateOfBirth, mc => mc.MapFrom(e => e.DateOfBirth.ToString("MM/dd/yyyy")));

            // entity <= web data

            var mapperLeft = CreateMap<EmployeeWebData, Employee>();

//            mapperLeft.ForMember(e => e.Id, mc => mc.MapFrom(d => d.Id));
            mapperLeft.ForMember(e => e.UniqueId, mc => mc.MapFrom(d => d.UniqueId));
            mapperLeft.ForMember(e => e.FirstName, mc => mc.MapFrom(d => d.FirstName));
            mapperLeft.ForMember(e => e.MiddleName, mc => mc.MapFrom(d => d.MiddleName));
            mapperLeft.ForMember(e => e.LastName, mc => mc.MapFrom(d => d.LastName));
            mapperLeft.ForMember(e => e.ContactNo, mc => mc.MapFrom(d => d.ContactNo));
            mapperLeft.ForMember(e => e.Email, mc => mc.MapFrom(d => d.Email));
            mapperLeft.ForMember(e => e.Gender, mc => mc.MapFrom(d => Enum.Parse(typeof(Gender), d.Gender)));
            mapperLeft.ForMember(e => e.DateOfBirth, mc => mc.MapFrom(d => DateTime.Parse(d.DateOfBirth)));
        }
    }
}
