using System.Collections.Generic;
using System.Linq;
using Aromato.Domain;
using Aromato.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public class PostgresRoleRepository : IRoleRepository
    {
        private readonly PostgresUnitOfWork _unitOfWork;

        public PostgresRoleRepository(PostgresUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Role FindById(long id)
        {
            return _unitOfWork.Roles
                    .Include("Permissions.Permission")
                    .FirstOrDefault();
        }

        public IEnumerable<Role> FindAll()
        {
            return _unitOfWork.Roles
                .Include("Permissions.Permission")
                .AsEnumerable();
        }

        public IEnumerable<Role> FindBySpec(ISpecification<long, Role> specification)
        {
            return _unitOfWork.Roles
                .Include("Permissions.Permission")
                .Where(specification.IsSatisified)
                .AsEnumerable();
        }

        public void Add(Role aggregate)
        {
            _unitOfWork.Roles.Add(aggregate);
        }

        public void Remove(Role aggregate)
        {
            _unitOfWork.Roles.Remove(aggregate);
        }

        public void RemoveById(long id)
        {
            Remove(FindById(id));
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}