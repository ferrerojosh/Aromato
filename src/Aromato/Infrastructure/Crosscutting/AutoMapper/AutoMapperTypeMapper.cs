using AutoMapper;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper
{

    public class AutoMapperTypeMapper : ITypeMapper
    {
        public TDestination MapTo<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TTarget MapTo<TTarget>(object source)
        {
            return Mapper.Map<TTarget>(source);
        }
    }
}
