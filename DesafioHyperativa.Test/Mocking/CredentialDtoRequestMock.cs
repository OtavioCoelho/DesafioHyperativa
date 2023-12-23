using DesafioHyperativa.Domain.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHyperativa.Test.Mocking;
public static class CredentialDtoRequestMock
{

    public static CredentialDtoRequest CredentialsValid = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "Abcd1234"
    };
    public static CredentialDtoRequest CredentialsEmailEmpty = new CredentialDtoRequest()
    {
        Email = "",
        Password = "Abcd1234"
    };
    public static CredentialDtoRequest CredentialsEmailNull = new CredentialDtoRequest()
    {
        Password = "Abcd1234"
    };
    public static CredentialDtoRequest CredentialsPasswordEmpty = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = ""
    };
    public static CredentialDtoRequest CredentialsPasswordNull = new CredentialDtoRequest()
    {
        Email = "01@user.com",
    };
    public static CredentialDtoRequest CredentialsPasswordWithoutUpperLetter = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "abcd1234"
    };
    public static CredentialDtoRequest CredentialsPasswordWithoutLowerLetter = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "ABCD1234"
    };
    public static CredentialDtoRequest CredentialsPasswordWithoutCharacterNumeric = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "Abcdabcd"
    };
    public static CredentialDtoRequest CredentialsPasswordWithoutMinimum8Character = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "Abc12"
    };
    public static CredentialDtoRequest CredentialsPasswordWithoutMaximum16Character = new CredentialDtoRequest()
    {
        Email = "01@user.com",
        Password = "Abcd1234567890123456"
    };
}
