using DesafioHyperativa.Domain.Entities;

namespace DesafioHyperativa.Test.Mocking;
public static class UserMock
{
    public static User UserRegistred = new User()
    {
        Id = Guid.Parse("bc63d576-0408-4270-b31e-b4c1541ea03d"),
        DtRegister = new DateTime(2023, 12, 23),
        DtUpdate = new DateTime(2023, 12, 23),
        Email = "01@user.com",
        Password = "Abcd1234"
    };

    public static User UserNotRegistred = new User()
    {
        Id = Guid.Empty,
        Email = "03@user.com",
        Password = "Oc156281"
    };


    public static IList<User> ListUser = new List<User>()
    {
        new User()
        {
            Id = Guid.Parse("bc63d576-0408-4270-b31e-b4c1541ea03d"),
            DtRegister = new DateTime(2023, 12, 23),
            DtUpdate = new DateTime(2023, 12, 23),
            Email = "01@user.com",
            Password = "Abcd1234"
        },
        new User()
        {
            Id = Guid.Parse("ce07edbb-721d-4693-a0f2-cd01fe469acf"),
            DtRegister = new DateTime(2023, 12,22),
            DtUpdate = new DateTime(2023, 12,22),
            Email = "02@user.com",
            Password = "Efgh5678"
        }
    };
}
