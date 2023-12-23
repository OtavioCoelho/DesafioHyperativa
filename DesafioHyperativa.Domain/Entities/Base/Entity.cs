using DesafioHyperativa.Domain.Contracts.Common;

namespace DesafioHyperativa.Domain.Entities.Base;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
    public DateTime DtRegister { get; set; }
    public DateTime DtUpdate { get; set; }
}
