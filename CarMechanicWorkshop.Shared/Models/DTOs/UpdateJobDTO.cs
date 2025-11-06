using System.ComponentModel.DataAnnotations;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// DTO used to update an existing job.
/// </summary>
public class UpdateJobDTO
{
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "License plate must match format AAA-123.")]
    public string? LicensePlate { get; set; }

    [Range(1900, int.MaxValue)]
    public int? ManufacturingYear { get; set; }

    public JobCategoryEnum? Category { get; set; }

    [MinLength(5)]
    public string? Description { get; set; }

    [Range(1, 10)]
    public int? Severity { get; set; }

    public JobStatusEnum? Status { get; set; }
}
