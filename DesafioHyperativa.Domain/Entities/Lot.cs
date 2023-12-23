using DesafioHyperativa.Domain.Contracts.Common;
using DesafioHyperativa.Domain.Entities.Base;

namespace DesafioHyperativa.Domain.Entities;
public class Lot : Entity, IUser
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public DateTime DateOperation { get; set; }
    public string LotNumber { get; set; }
    public int NumberRecords { get; set; }
    public ICollection<Card> Cards { get; set; }

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
        if (string.IsNullOrEmpty(Name))
            errors.Add("Name the lot not informed.");
        if (DateOperation == DateTime.MinValue)
            errors.Add("Date the lot not informed or with formatting problems.");
        if (string.IsNullOrEmpty(LotNumber))
            errors.Add("Lot number not informed.");
        if (NumberRecords == 0)
            errors.Add("Number the records in the Lot, is not informed.");

        _errors = errors.ToArray();
        _isValid = !_errors.Any();
    }
    #endregion
}
