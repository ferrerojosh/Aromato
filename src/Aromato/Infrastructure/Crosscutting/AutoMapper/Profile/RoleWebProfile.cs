using System.Linq;
using Aromato.Application.Web.Data;
using Aromato.Domain.RoleAgg;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class RoleWebProfile : global::AutoMapper.Profile
    {
        public RoleWebProfile()
        {
            var roleMapper = CreateMap<Role, RoleWebData>();

            roleMapper.ForMember(r => r.Permissions, m => m.MapFrom(r => r.Permissions
                .Select(x => x.Permission)
                .AsEnumerableData<PermissionWebData>()
            ));

            CreateMap<Permission, PermissionWebData>();
        }
    }
}