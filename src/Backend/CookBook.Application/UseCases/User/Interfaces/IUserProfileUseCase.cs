using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.User.Interfaces;
public interface IUserProfileUseCase
{
    public Task<UserProfileResponse> Execute();
}
