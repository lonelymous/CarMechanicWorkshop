using System.ComponentModel.DataAnnotations;
using CarMechanicWorkshop.Domain.Enums;

namespace CarMechanicWorkshop.Shared.DTOs.Jobs;

/// <summary>
/// DTO used to update an existing job.
/// </summary>
public class UpdateJobDTO
{
    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "License plate must match format AAA-123.")]
    public string? LicensePlate { get; set; }

    /// <summary>
    /// The manufacturing year of the vehicle.
    /// </summary>
    [Range(1900, int.MaxValue)]
    public int? ManufacturingYear { get; set; }

    /// <summary>
    /// The category of the job.
    /// </summary>
    public JobCategoryEnum? Category { get; set; }

    /// <summary>
    /// The description of the job.
    /// </summary>
    [MinLength(5)]
    public string? Description { get; set; }

    /// <summary>
    /// The severity of the job.
    /// </summary>
    [Range(1, 10)]
    public int? Severity { get; set; }

    /// <summary>
    /// The status of the job.
    /// </summary>
    public JobStatusEnum? Status { get; set; }
}
