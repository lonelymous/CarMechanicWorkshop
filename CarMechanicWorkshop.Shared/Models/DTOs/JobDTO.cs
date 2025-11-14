using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// Represents a job returned by the API.
/// </summary>
public class JobDTO
{
    /// <summary>
    /// The unique identifier for the job.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ID of the client who requested the job.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Name of the client who requested the job.
    /// </summary>
    public string? ClientName { get; set; } // convenience for UI

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// The manufacturing year of the vehicle.
    /// </summary>
    public int ManufacturingYear { get; set; }

    /// <summary>
    /// The category of the job.
    /// </summary>
    public JobCategoryEnum Category { get; set; }

    /// <summary>
    /// The description of the job.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The severity of the job.
    /// </summary>
    public int Severity { get; set; }

    /// <summary>
    /// The status of the job.
    /// </summary>
    public JobStatusEnum Status { get; set; }

    /// <summary>
    /// The estimated hours required to complete the job.
    /// </summary>
    public double EstimatedHours { get; set; }
}
