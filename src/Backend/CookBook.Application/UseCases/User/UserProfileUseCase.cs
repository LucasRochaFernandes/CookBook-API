using AutoMapper;
using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Communication.Responses;
using CookBook.Domain.Services.LoggedUser;

namespace CookBook.Application.UseCases.User;
public class UserProfileUseCase : IUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public UserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Execute()
    {
        var user = await _loggedUser.User();
        return _mapper.Map<UserProfileResponse>(user);
    }
}

