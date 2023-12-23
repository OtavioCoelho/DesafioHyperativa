using DesafioHyperativa.Domain.Dto.Requests.Base;

namespace DesafioHyperativa.Domain.Dto.Requests;
public class CardDtoRequest : BaseRequest
{
    public string Number { get; set; }
    public override void Valid()
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(Number))
            errors.Add("Number card is not informed.");
        if (Number.Any(x => !char.IsNumber(x)) || Number.Length < 13 || Number.Length > 19)
            errors.Add("Number card is invalid.");

        SetErrors(errors.ToArray());
        SetIsValid(!this.Errors.Any());
    }
}
