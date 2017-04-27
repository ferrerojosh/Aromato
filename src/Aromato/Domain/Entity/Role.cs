namespace Aromato.Domain.Entity
{
    public class Role : IEntity
    {
        protected Role()
        {
            // Required for Entity Framework
        }

        public long Id { get; set; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }

        //public IEnumerable<Employee> Employees { get; protected set; }
    }
}