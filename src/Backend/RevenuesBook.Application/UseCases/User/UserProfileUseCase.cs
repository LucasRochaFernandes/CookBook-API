using AutoMapper;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.Services.LoggedUser;

namespace RevenuesBook.Application.UseCases.User;
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

