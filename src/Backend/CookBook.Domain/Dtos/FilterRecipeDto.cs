﻿using CookBook.Domain.Enums;

namespace CookBook.Domain.Dtos;
public record FilterRecipeDto
{
    public string? RecipeTitle_Ingredient { get; init; }
    public IList<CookingTime> CookingTimes { get; init; } = [];
    public IList<Difficulty> Difficulties { get; init; } = [];
    public IList<DishType> DishTypes { get; init; } = [];
}
