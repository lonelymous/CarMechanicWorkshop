namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// Represents a client returned by the API.
/// </summary>
public class ClientDTO
{
    /// <summary>
    /// The unique identifier for the client.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the client.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The address of the client.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// The email of the client.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}