using System.ComponentModel.DataAnnotations;
using CarMechanicWorkshop.Domain.Enums;

namespace CarMechanicWorkshop.Shared.DTOs.Jobs;

/// <summary>
/// DTO used to create a new job.
/// </summary>
public class CreateJobDTO
{
    /// <summary>
    /// The id of the client
    /// </summary>
    [Required]
    public int ClientId { get; set; }

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [Required, RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "License plate must match format AAA-123.")]
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// The manufacturing year of the vehicle.
    /// </summary>
    [Range(1900, int.MaxValue)]
    public int ManufacturingYear { get; set; }

    /// <summary>
    /// The category of the job.
    /// </summary>
    [Required]
    public JobCategoryEnum Category { get; set; }

    /// <summary>
    /// The description of the job.
    /// </summary>
    [Required, MinLength(5)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The severity of the job.
    /// </summary>
    [Range(1, 10)]
    public int Severity { get; set; }

    /// <summary>
    /// The status of the job.
    /// </summary>
    public JobStatusEnum Status { get; set; } = JobStatusEnum.Accepted;
}
