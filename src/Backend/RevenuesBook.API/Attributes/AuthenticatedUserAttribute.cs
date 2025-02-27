using Microsoft.AspNetCore.Mvc;
using RevenuesBook.API.Filters;

namespace RevenuesBook.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
