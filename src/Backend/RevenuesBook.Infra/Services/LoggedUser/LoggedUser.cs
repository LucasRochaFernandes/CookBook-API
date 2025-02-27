using Microsoft.EntityFrameworkCore;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Domain.Services.LoggedUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RevenuesBook.Infra.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly AppDbContext _appDbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(AppDbContext appDbContext, ITokenProvider tokenProvider)
    {
        _appDbContext = appDbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var tokenValue = _tokenProvider.Value();
        var securityTokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = securityTokenHandler.ReadJwtToken(tokenValue);
        var userId = Guid.Parse(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        var user = await _appDbContext.Users.AsNoTracking().FirstAsync(u => u.Id.Equals(userId));
        return user;
    }
}
