using DesafioHyperativa.Domain.Dto.Responses;

namespace DesafioHyperativa.Test.Mocking;
public static class CredentialDtoResponseMock
{
    public static CredentialDtoResponse CredentialsValid = new()
    {
        AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJiYzYzZDU3Ni0wNDA4LTQyNzAtYjMxZS1iNGMxNTQxZWEwM2QiLCJzdWIiOiJBUEkgZGVzYWZpbyBIeXBlcmF0aXZhLiIsImp0aSI6IjgyMGRiMjMwLTNjN2ItNGFkNC04NzhmLWIyZmU2NjBkZWVkYSIsIm5iZiI6MTcwMzM0MDM1NSwiaWF0IjoxNzAzMzQwMzU1LCJleHAiOjE3MDMzNDEyNTV9.vtCZ4aLe4jp6hD58QpIQn40wI1YNrAg4-Z3gewFitBc",
        ExpiresIn = DateTime.UtcNow.AddMinutes(15)
    };

}
