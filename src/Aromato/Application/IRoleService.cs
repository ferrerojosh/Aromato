using System.Collections.Generic;

namespace Aromato.Application
{
    public interface IRoleService : IReadService<long>
    {
        IEnumerable<IData> RetrieveRolesByUniqueId(string uniqueId);
    }
}