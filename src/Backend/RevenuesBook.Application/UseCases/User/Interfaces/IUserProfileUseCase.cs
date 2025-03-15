using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.User.Interfaces;
public interface IUserProfileUseCase
{
    public Task<UserProfileResponse> Execute();
}
