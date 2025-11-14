using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.Database;

/// <summary>
/// Represents a client in the car mechanic workshop system.
/// </summary>
public class ClientDatabase
{

    /// <summary>
    /// The unique identifier for the client.
    /// </summary>
    public int Id { get; set; }

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
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The phone number of the client.
    /// </summary>
    public List<JobDatabase>? Jobs { get; set; }
}