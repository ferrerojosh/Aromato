using Aromato.Infrastructure.Crosscutting.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Aromato.Infrastructure.Crosscutting.Extension
{
    public static class AutoMapperTypeMapperExtension
    {
        /// <summary>
        /// Convenience method for ASP.NET Core applications. You may also manually call <see cref="TypeMapperFactory.UseFactory"/> if
        /// you like.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseAutoMapperTypeAdapter(this IServiceCollection services)
        {
            TypeMapperFactory.UseFactory(new AutoMapperTypeMapperFactory());
            return services;
        }

    }
}
