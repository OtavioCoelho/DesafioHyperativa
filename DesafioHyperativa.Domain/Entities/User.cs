using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Domain.Entities;
public class User : Entity
{
    public string Email { get; set; }
    public string Password { get; set; }


}
