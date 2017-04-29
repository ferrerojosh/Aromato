namespace Aromato.Domain.Employee
{
    public class Role : IEntity<long>
    {
        /// <summary>
        /// Empty constructor as required by Entity Framework or any OR/M libraries.
        /// </summary>
        protected Role()
        {
        }

        public long Id { get; set; }
        public virtual string Name { get; private set; }
    }
}