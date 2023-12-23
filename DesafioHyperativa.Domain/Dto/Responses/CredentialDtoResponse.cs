namespace DesafioHyperativa.Domain.Dto.Responses;

public class CredentialDtoResponse
{
    public string AccessToken { get; set; }
    public string TokenType => "Bearer";
    public DateTime ExpiresIn { get; set; }

}
