using DesafioHyperativa.Domain.Dto.Requests.Base;
using Microsoft.AspNetCore.Http;

namespace DesafioHyperativa.Domain.Dto.Requests;
public class LotDtoRequest : BaseRequest
{
    public IFormFile File { get; set; }

    public override void Valid()
    {
        var errors = new List<string>();

        if (File.Length <= 0)
            errors.Add("Enter valid file.");

        SetErrors(errors.ToArray());
        SetIsValid(!this.Errors.Any());
    }
}
