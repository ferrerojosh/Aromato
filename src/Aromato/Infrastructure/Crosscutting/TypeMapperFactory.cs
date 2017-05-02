namespace Aromato.Infrastructure.Crosscutting
{
    public class TypeMapperFactory
    {
        public static ITypeMapperFactory Instance { get; private set; }

        public static void UseFactory(ITypeMapperFactory mapperFactory)
        {
            Instance = mapperFactory;
        }
    }
}