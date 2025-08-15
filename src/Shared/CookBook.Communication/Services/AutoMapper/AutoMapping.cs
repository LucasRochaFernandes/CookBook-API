using AutoMapper;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.Entities;

namespace CookBook.Communication.Services.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestInstruction, Instruction>();

        CreateMap<RegisterRecipeFormDataRequest, Recipe>()
            .ForMember(dest => dest.Instructions, opt => opt.Ignore())
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
                src.Ingredients.Select(item => new Ingredient { Item = item })))
            .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(src =>
                src.DishTypes.Select(dishTypeEnum => new DishType { Type = dishTypeEnum })));
        CreateMap<Domain.Enums.DishType, DishType>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
    }

    private void DomainToResponse()
    {
        CreateMap<User, UserProfileResponse>();
        CreateMap<Recipe, RegisterRecipeResponse>()
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Recipe, RecipeResponse>()
            .ForMember(dest => dest.DishTypes, opt => opt.MapFrom(src => src.DishTypes.Select(d => d.Type)));

        CreateMap<Ingredient, IngredientResponse>();
        CreateMap<Instruction, InstructionResponse>();
        CreateMap<Recipe, RecipeShortResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AmountIngredients, opt => opt.MapFrom(src => src.Ingredients.Count));
    }
}