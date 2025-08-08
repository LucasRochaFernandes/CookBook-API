using Microsoft.EntityFrameworkCore;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.IRepositories;
using System.Linq.Expressions;

namespace RevenuesBook.Infra.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Guid> Create(User entityUser)
    {
        var result = await _appDbContext.Users.AddAsync(entityUser);
        return result.Entity.Id;
    }

    public async Task<User?> FindBy(Expression<Func<User, bool>> condition, bool AsNoTracking = false)
    {
        if (AsNoTracking)
        {
            return await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(condition);
        }
        else
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(condition);
        }

    }
    public async Task Commit()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public void Update(User entityUser)
    {
        entityUser.UpdatedAt = DateTime.UtcNow;
        _appDbContext.Users.Update(entityUser);
    }
}
