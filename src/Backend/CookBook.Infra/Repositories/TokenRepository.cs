using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infra.Repositories;
public class TokenRepository : ITokenRepository
{
    private readonly AppDbContext _appDbContext;

    public TokenRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await _appDbContext.RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    }

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _appDbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);
        _appDbContext.RefreshTokens.RemoveRange(tokens);
        await _appDbContext.RefreshTokens.AddAsync(refreshToken);
    }
    public async Task Commit()
    {
        await _appDbContext.SaveChangesAsync();
    }
}
