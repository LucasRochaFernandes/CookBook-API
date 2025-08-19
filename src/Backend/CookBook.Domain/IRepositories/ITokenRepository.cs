using CookBook.Domain.Entities;

namespace CookBook.Domain.IRepositories;
public interface ITokenRepository
{
    public Task<RefreshToken?> Get(string refreshToken);
    public Task SaveNewRefreshToken(RefreshToken refreshToken);
    public Task Commit();
}
