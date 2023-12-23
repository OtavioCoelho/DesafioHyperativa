using DesafioHyperativa.Domain.Contracts.Services;
using DesafioHyperativa.Domain.Dto.Responses;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using DesafioHyperativa.Domain.Utility;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioHyperativa.API.Infra;

public class TokenService : ITokenService
{
    private readonly IUserService _serviceUser;

    public TokenService(IUserService serviceUser)
    {
        _serviceUser = serviceUser;
    }

    public async Task<CredentialDtoResponse> GenerateTokenAsync(User user)
    {
        await _serviceUser.ValidateUserGetByLogin(user);

        User userEntity = await _serviceUser.GetByLoginAsync(user);

        if (userEntity == null)
            throw new UnauthorizedException("Invalid credentials.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.SecretKey);

        var identity = new ClaimsIdentity(new[] {
                  new Claim(CustomClaims.UserId, Convert.ToString(userEntity.Id)),
                  new Claim(JwtRegisteredClaimNames.Sub, "API desafio Hyperativa."),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                  new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(15).ToString()),
            });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new CredentialDtoResponse
        {
            AccessToken = tokenString,
            ExpiresIn = token.ValidTo
        };
    }


}
