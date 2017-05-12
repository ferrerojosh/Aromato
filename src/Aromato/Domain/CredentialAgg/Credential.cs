using Aromato.Domain.EmployeeAgg;

namespace Aromato.Domain.CredentialAgg
{
    public class Credential : IAggregateRoot<long>
    {
        public long Id { get; set; }

        public virtual string Username { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual Employee Employee { get; protected set; }
    }
}