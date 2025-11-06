namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// Represents a client DTO in the car mechanic workshop system.
/// </summary>
public class ClientDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the client.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the client.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}