using AutoMapper;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Domain.Entities;

namespace RevenuesBook.Communication.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }
}
