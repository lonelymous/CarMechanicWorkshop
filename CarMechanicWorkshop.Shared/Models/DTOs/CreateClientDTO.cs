using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

public class CreateClientDTO
{
    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    [Required, MinLength(2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the client.
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the client.
    /// </summary>
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
