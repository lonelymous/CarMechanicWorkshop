using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// Represents a job returned by the API.
/// </summary>
public class JobDTO
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; } // convenience for UI
    public string LicensePlate { get; set; } = string.Empty;
    public int ManufacturingYear { get; set; }
    public JobCategoryEnum Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Severity { get; set; }
    public JobStatusEnum Status { get; set; }
    public double EstimatedHours { get; set; }
}
