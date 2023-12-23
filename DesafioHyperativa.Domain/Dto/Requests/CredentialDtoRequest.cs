using DesafioHyperativa.Domain.Dto.Requests.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DesafioHyperativa.Domain.Dto.Requests;

public class CredentialDtoRequest : BaseRequest
{
    public string Email { get; set; }

    public string Password { get; set; }

    public override void Valid()
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(Email))
            errors.Add("Enter your e-mail.");
        if (string.IsNullOrEmpty(Password))
            errors.Add("Enter your password.");
        else
        {
            if (Password.Length < 8 || Password.Length > 16)
                errors.Add("The password must be between 8 and 16 characters long.");
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
            if (!regex.IsMatch(Password))
                errors.Add("The password must contain at least 1 uppercase letter, 1 lowercase letter and 1 number.");
        }

        SetErrors(errors.ToArray());
        SetIsValid(!this.Errors.Any());
    }
}
