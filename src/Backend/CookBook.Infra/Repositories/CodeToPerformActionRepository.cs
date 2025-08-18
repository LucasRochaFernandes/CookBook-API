using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infra.Repositories;
public class CodeToPerformActionRepository : ICodeToPerformActionRepository
{
    private readonly AppDbContext _appDbContext;

    public CodeToPerformActionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Create(CodeToPerformAction codeToPerformAction)
    {
        var result = await _appDbContext.CodeToPerformActions.AddAsync(codeToPerformAction);
        return result.Entity.Id;
    }

    public async Task Delete(Guid codeToPerformActionId)
    {
        var codeToPerformAction = await _appDbContext.CodeToPerformActions.FindAsync(codeToPerformActionId);
        _appDbContext.CodeToPerformActions.Remove(codeToPerformAction!);
    }

    public async Task<CodeToPerformAction?> GetByCode(string code)
    {
        return await _appDbContext.CodeToPerformActions.AsNoTracking().FirstOrDefaultAsync(c => c.Value.Equals(code));
    }

    public void DeleteAllUserCodes(Guid userId)
    {
        var codesToDelete = _appDbContext.CodeToPerformActions.Where(cd => cd.UserId.Equals(userId));
        _appDbContext.CodeToPerformActions.RemoveRange(codesToDelete);
    }
    public async Task Commit()
    {
        await _appDbContext.SaveChangesAsync();
    }
}
