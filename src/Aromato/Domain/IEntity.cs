using System;

namespace Aromato.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}