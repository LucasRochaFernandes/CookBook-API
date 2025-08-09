using AutoMapper;
using CookBook.Communication.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class AutoMapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

    }
}


