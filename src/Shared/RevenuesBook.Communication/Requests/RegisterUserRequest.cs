using System.ComponentModel.DataAnnotations;

namespace RevenuesBook.Communication.Requests;
public sealed record RegisterUserRequest(
    [Required]
    string Name,
    [Required]
    string Email,
    [Required]
    string Password
    );
