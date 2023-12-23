using DesafioHyperativa.Domain.Contracts.Common;
using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Domain.Entities;
public class Card : Entity, IUser
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string LineIdentifier { get; set; }
    public string LotNumber { get; set; }
    public string Number { get; set; }
    public Guid? LotId { get; set; }
    public Lot Lot { get; set; }

    #region Methods Validation
    private bool _isValid;
    private string[] _errors = null;
    public bool IsValid()
    {
        Validate();
        return _isValid;
    }
    public string[] GetErros()
    {
        Validate();
        return _errors;
    }
    private void Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrEmpty(LineIdentifier))
            errors.Add("Line Identifier the card not informed.");
        if (string.IsNullOrEmpty(LotNumber))
            errors.Add("Lot number the card not informed.");
        if (string.IsNullOrEmpty(Number))
            errors.Add("Number card is not informed.");
        if (Number.Any(x => !char.IsNumber(x)) || Number.Length < 13 || Number.Length > 19)
            errors.Add("Number card is invalid.");
        
        _errors = errors.ToArray();
        _isValid = !_errors.Any();
    }
    #endregion
}
