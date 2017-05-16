using System.Collections.Generic;
using Aromato.Application.Web.Data;
using Aromato.Domain.RoleAgg;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Application.Web
{
    public class RoleWebService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleWebService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IData RetrieveById(long id)
        {
            return _roleRepository.FindById(id).AsData<RoleWebData>();
        }

        public IEnumerable<IData> RetrieveAll()
        {
            return _roleRepository.FindAll().AsEnumerableData<RoleWebData>();
        }

        public IEnumerable<IData> RetrieveRolesByUniqueId(string uniqueId)
        {
            return _roleRepository.FindBySpec(new RolesByUniqueId(uniqueId)).AsEnumerableData<RoleWebData>();
        }
    }
}