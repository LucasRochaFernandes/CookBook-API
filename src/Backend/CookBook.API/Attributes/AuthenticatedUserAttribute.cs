using Microsoft.AspNetCore.Mvc;
using CookBook.API.Filters;

namespace CookBook.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
