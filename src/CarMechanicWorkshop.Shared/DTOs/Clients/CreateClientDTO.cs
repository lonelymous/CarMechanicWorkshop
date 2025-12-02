using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.DTOs.Clients;

public class CreateClientDTO
{
    /// <summary>
    /// The name of the client.
    /// </summary>
    [Required, MinLength(2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The address of the client.
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// The email of the client.
    /// </summary>
    [Required, EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;
}
