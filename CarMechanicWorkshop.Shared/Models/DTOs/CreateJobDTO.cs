using System.ComponentModel.DataAnnotations;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Shared.Models.DTOs;

/// <summary>
/// DTO used to create a new job.
/// </summary>
public class CreateJobDTO
{
    [Required(ErrorMessage = "Client is required.")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "License plate is required.")]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "License plate must match format AAA-123.")]
    public string LicensePlate { get; set; } = string.Empty;

    [Range(1900, int.MaxValue, ErrorMessage = "The year of manufacture must not be earlier than 1900.")]
    public int ManufacturingYear { get; set; }

    [Required]
    public JobCategoryEnum Category { get; set; }

    [Required, MinLength(5, ErrorMessage = "The description must be at least 5 characters long.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "The severity should be between 1 and 10.")]
    public int Severity { get; set; }

    public JobStatusEnum Status { get; set; } = JobStatusEnum.Accepted;
}
