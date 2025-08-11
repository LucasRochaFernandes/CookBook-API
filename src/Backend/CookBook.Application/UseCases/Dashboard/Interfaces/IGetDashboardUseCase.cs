using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Dashboard.Interfaces;
public interface IGetDashboardUseCase
{
    public Task<RecipesResponse> Execute();
}
