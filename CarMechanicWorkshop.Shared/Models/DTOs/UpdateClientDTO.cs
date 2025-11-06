using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

public class UpdateClientDTO
{
    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the client.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the client.
    /// </summary>
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
