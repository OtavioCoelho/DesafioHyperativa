using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Test.Mocking;
public static class CardMock
{
    public static string CardOnlyNumberNotRegistred = "4456897922969879";
    public static string CardOnlyNumberValid = "4456897922969999";

    public static List<Card> ListCards = new()
    {
        new ()
        {
            Id = Guid.NewGuid(),
            DtRegister = DateTime.Now,
            DtUpdate = DateTime.Now,
            Number = "4456897912999999"
        },
        new ()
        {
            Id = Guid.NewGuid(),
            DtRegister = DateTime.Now,
            DtUpdate = DateTime.Now,
            Number = "4456897999099999"
        },
        new ()
        {
            Id = Guid.NewGuid(),
            DtRegister = DateTime.Now,
            DtUpdate = DateTime.Now,
            Number = "4456897922969999"
        },
        new ()
        {
            Id = Guid.NewGuid(),
            DtRegister = DateTime.Now,
            DtUpdate = DateTime.Now,
            Number = "4456897919999999"
        },
        new ()
        {
            Id = Guid.NewGuid(),
            DtRegister = DateTime.Now,
            DtUpdate = DateTime.Now,
            Number = "445689799999998"
        },
    };
}
