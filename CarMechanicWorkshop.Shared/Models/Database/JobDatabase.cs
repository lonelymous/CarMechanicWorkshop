using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.Database;

/// <summary>
/// Represents a job in the car mechanic workshop system.
/// </summary>
public class JobDatabase
{
    /// <summary>
    /// The unique identifier for the job.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ID of the client who requested the job.
    /// </summary>
    [Required]
    public int ClientId { get; set; }

    /// <summary>
    /// The <see cref="ClientDatabase"/> client who requested the job.
    /// </summary>
    public ClientDatabase? Client { get; set; }

    /// <summary>
    /// The license plate of the vehicle.
    /// </summary>
    [Required, RegularExpression(@"^[A-Z]{3}-\d{3}$")]
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

    /// <summary>
    /// Gets the estimated hours required to complete the job.
    /// </summary>
    public decimal EstimatedHours =>
        GetCategoryHours(Category) * GetAgeFactor(ManufacturingYear) * GetSeverityFactor(Severity);

    /// <summary>
    /// Gets the base hours for the given job category.
    /// </summary>
    /// <param name="category"> The job <see cref="JobCategoryEnum"/> category.</param>
    /// <returns> The base hours for the category.</returns>
    private static decimal GetCategoryHours(JobCategoryEnum category) => category switch
    {
        JobCategoryEnum.Chassis => 3,
        JobCategoryEnum.Engine => 8,
        JobCategoryEnum.Drivetrain => 6,
        JobCategoryEnum.Brakes => 4,
        _ => 1
    };

    /// <summary>
    /// Gets the age factor based on the manufacturing year.
    /// </summary>
    /// <param name="year"> The manufacturing year.</param>
    /// <returns> The age factor.</returns>
    private static decimal GetAgeFactor(int year)
    {
        int age = DateTime.Now.Year - year;
        return age switch
        {
            <= 5 => 0.5M,
            <= 10 => 1,
            <= 20 => 1.5M,
            _ => 2
        };
    }

    /// <summary>
    /// Gets the severity factor based on the severity level.
    /// </summary>
    /// <param name="severity"> The severity level.</param>
    /// <returns> The severity factor.</returns>
    private static decimal GetSeverityFactor(int severity) => severity switch
    {
        <= 2 => 0.2M,
        <= 4 => 0.4M,
        <= 7 => 0.6M,
        <= 9 => 0.8M,
        _ => 1
    };
}