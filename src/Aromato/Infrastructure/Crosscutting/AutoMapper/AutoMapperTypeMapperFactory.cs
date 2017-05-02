using Aromato.Infrastructure.Crosscutting.AutoMapper.Profile;
using AutoMapper;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper
{
    public class AutoMapperTypeMapperFactory : ITypeMapperFactory
    {
        public AutoMapperTypeMapperFactory()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EmployeeWebProfile>();
                cfg.AddProfile<PunchWebProfile>();
            });
        }

        public ITypeMapper CreateMapper()
        {
            return new AutoMapperTypeMapper();
        }
    }
}
