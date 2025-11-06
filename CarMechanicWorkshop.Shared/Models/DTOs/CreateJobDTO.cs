using System.ComponentModel.DataAnnotations;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// DTO used to create a new job.
/// </summary>
public class CreateJobDTO
{
    [Required]
    public int ClientId { get; set; }

    [Required, RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "License plate must match format AAA-123.")]
    public string LicensePlate { get; set; } = string.Empty;

    [Range(1900, int.MaxValue)]
    public int ManufacturingYear { get; set; }

    [Required]
    public JobCategoryEnum Category { get; set; }

    [Required, MinLength(5)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Severity { get; set; }

    public JobStatusEnum Status { get; set; } = JobStatusEnum.Accepted;
}
