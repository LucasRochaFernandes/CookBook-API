using System.ComponentModel.DataAnnotations;

namespace RevenuesBook.Communication.Responses;
public sealed record RegisterUserResponse(
    [Required]
    string UserId
    );
