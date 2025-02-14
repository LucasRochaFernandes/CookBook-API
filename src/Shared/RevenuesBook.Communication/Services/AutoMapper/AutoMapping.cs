using AutoMapper;
using RevenuesBook.Communication.Requests;

namespace RevenuesBook.Communication.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {

    }
}