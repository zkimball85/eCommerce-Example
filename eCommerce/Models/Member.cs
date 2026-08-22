using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;


/// <summary>
/// Represents a member of the Zac's Smoke Shop website.
/// </summary>
public class Member
{
    // The Unique identifier for the member.
    [Key]
    public int MemberId { get; set; }

    // The username of the member. 
    // AlphaNumeric characters only, no spaces or special characters allowed.
    [Required]
    [StringLength(25)]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters.")]
    public required string Username { get; set; }

    // The email address of the member.
    public required string Email { get; set; }

    // The password of the member.
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters.")]
    public required string Password { get; set; }

    // The date of birth of the member.

    public DateOnly DateOfBirth { get; set; }
}


/// <summary>
/// Represents the view model for member registration, 
/// containing the necessary fields for user input during the registration process.
/// </summary>
public class RegistrationViewModel
{

    [Required]
    [StringLength(25)]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain alphanumeric characters.")]
    public required string Username { get; set; }


    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }


    [Required]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }


    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}
