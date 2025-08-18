using CookBook.Domain.Entities;

namespace CookBook.Domain.IRepositories;
public interface ICodeToPerformActionRepository
{
    public Task<Guid> Create(CodeToPerformAction codeToPerformAction);
    public Task Delete(Guid codeToPerformActionId);
    public Task<CodeToPerformAction?> GetByCode(string code);
    public void DeleteAllUserCodes(Guid userId);
    public Task Commit();
}
