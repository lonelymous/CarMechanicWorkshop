using System.ComponentModel.DataAnnotations;

namespace CarMechanicWorkshop.Shared.Models.Database;

/// <summary>
/// Represents a job in the car mechanic workshop system.
/// </summary>
public class JobDatabase
{
    /// <summary>
    /// Gets or sets the unique identifier for the job.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the client who requested the job.
    /// </summary>
    [Required]
    public int ClientId { get; set; }
    /// <summary>
    /// Gets or sets the <see cref="ClientDatabase"/> client who requested the job.
    /// </summary>
    public ClientDatabase? Client { get; set; }
    /// <summary>
    /// Gets or sets the license plate of the vehicle.
    /// </summary>
    [Required, RegularExpression(@"^[A-Z]{3}-\d{3}$")]
    public string LicensePlate { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the manufacturing year of the vehicle.
    /// </summary>
    [Range(1900, int.MaxValue)]
    public int ManufacturingYear { get; set; }
    /// <summary>
    /// Gets or sets the category of the job.
    /// </summary>
    [Required]
    public JobCategoryEnum Category { get; set; }
    /// <summary>
    /// Gets or sets the description of the job.
    /// </summary>
    [Required, MinLength(5)]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the severity of the job.
    /// </summary>
    [Range(1, 10)]
    public int Severity { get; set; }
    /// <summary>
    /// Gets or sets the status of the job.
    /// </summary>
    public JobStatusEnum Status { get; set; } = JobStatusEnum.Accepted;
    /// <summary>
    /// Gets the estimated hours required to complete the job.
    /// </summary>
    public double EstimatedHours =>
        GetCategoryHours(Category) * GetAgeFactor(ManufacturingYear) * GetSeverityFactor(Severity);

    /// <summary>
    /// Gets the base hours for the given job category.
    /// </summary>
    /// <param name="category"> The job <see cref="JobCategoryEnum"/> category.</param>
    /// <returns> The base hours for the category.</returns>
    private static double GetCategoryHours(JobCategoryEnum category) => category switch
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
    private static double GetAgeFactor(int year)
    {
        int age = DateTime.Now.Year - year;
        return age switch
        {
            <= 5 => 0.5,
            <= 10 => 1,
            <= 20 => 1.5,
            _ => 2
        };
    }

    /// <summary>
    /// Gets the severity factor based on the severity level.
    /// </summary>
    /// <param name="severity"> The severity level.</param>
    /// <returns> The severity factor.</returns>
    private static double GetSeverityFactor(int severity) => severity switch
    {
        <= 2 => 0.2,
        <= 4 => 0.4,
        <= 7 => 0.6,
        <= 9 => 0.8,
        _ => 1
    };
}