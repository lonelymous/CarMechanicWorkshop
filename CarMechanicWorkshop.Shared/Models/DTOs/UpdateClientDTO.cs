using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

public class UpdateClientDTO
{
    /// <summary>
    /// The name of the client.
    /// </summary>
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The address of the client.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// The email of the client.
    /// </summary>
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
