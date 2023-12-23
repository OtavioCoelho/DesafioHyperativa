using DesafioHyperativa.Domain.Attributes;
using DesafioHyperativa.Domain.Contracts.Dto;
using Newtonsoft.Json;

namespace DesafioHyperativa.Domain.Dto.Requests.Base;
public abstract class BaseRequest : IBaseRequest
{
    private bool? _isValid = null;
    private string[] _errors = null;

    [SwaggerIgnore]
    public bool IsValid
    {
        get
        {
            if (!_isValid.HasValue)
                Valid();
            return _isValid.GetValueOrDefault();
        }
    }
    [SwaggerIgnore]
    public string[] Errors
    {
        get
        {
            if (_errors == null)
                Valid();
            return _errors;
        }
    }

    public abstract void Valid();

    protected void SetIsValid(bool isValid)
        => _isValid = isValid;

    protected void SetErrors(string[] errors)
        => _errors = errors;
}
