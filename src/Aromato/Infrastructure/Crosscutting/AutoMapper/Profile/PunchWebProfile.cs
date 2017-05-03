using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class PunchWebProfile : global::AutoMapper.Profile
    {
        public PunchWebProfile()
        {
            var mapper = CreateMap<Punch, PunchWebData>();

            mapper.ForMember(d => d.Type, mc => mc.MapFrom(p => p.Type.ToString()));
            mapper.ForMember(d => d.DateAdded, mc => mc.MapFrom(p => p.DateAdded.ToString("dddd, MMMM dd, HH:mm:ss tt")));
        }
    }
}
