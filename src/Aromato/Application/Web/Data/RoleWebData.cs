using System.Collections.Generic;

namespace Aromato.Application.Web.Data
{
    public class RoleWebData : IData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PermissionWebData> Permissions { get; set; }
    }
}