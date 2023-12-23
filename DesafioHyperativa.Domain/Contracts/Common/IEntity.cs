namespace DesafioHyperativa.Domain.Contracts.Common;
public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime DtRegister { get; set; }
    public DateTime DtUpdate { get; set; }
}
