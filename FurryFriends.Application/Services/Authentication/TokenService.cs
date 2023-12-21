using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Services.Authentication.Models;
using FurryFriends.Application.Shared.Models.Base;
using FurryFriends.Common.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FurryFriends.Application.Services.Authentication;

public class TokenService : ITokenService
{
    private readonly Jwt _jwt;
    public TokenService(IOptions<Jwt> jwt)
    {
        _jwt = jwt.Value;
    }

    public async Task<ServiceResponse> GenerateTokenAsync(AuthenticatedUserModel authenticatedUser)
    {
        var claims = new List<Claim>
        {
            new Claim("UserId", authenticatedUser.UserId.ToString()),
            new Claim("Username", authenticatedUser.Username),
            new Claim("Email", authenticatedUser.Email),
            new Claim("PersonId", authenticatedUser.PersonId.ToString()),
            new Claim("PersonTypeId", authenticatedUser.PersonTypeId.ToString()),
            new Claim("PersonType", authenticatedUser.PersonType),
            new Claim("Name", authenticatedUser.Name),
            new Claim("Surname", authenticatedUser.Surname),
            new Claim("FullName", authenticatedUser.FullName),
            new Claim("RoleId", authenticatedUser.RoleId.ToString()),
            new Claim("RoleName", authenticatedUser.RoleName)
        };

        return await Task.FromResult(new ServiceResponse(GenerateToken(claims)));
    }

    #region Private Methods
    private string GenerateToken(List<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwt.MinutesToExpire),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
    #endregion
}