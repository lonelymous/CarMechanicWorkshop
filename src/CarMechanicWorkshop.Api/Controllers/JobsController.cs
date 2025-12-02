using CarMechanicWorkshop.Application.Interfaces.Services;
using CarMechanicWorkshop.Shared.DTOs.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanicWorkshop.Api.Controllers;

/// <summary>
/// API controller for managing jobs
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly ILogger<JobsController> _logger;
    private readonly IJobsService _service;

    public JobsController(ILogger<JobsController> logger, IJobsService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Create a new job asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="CreateJobDTO"/> job DTO to create </param>
    /// <returns> The created <see cref="JobDTO"/> </returns>
    /// <response code="201">Returns the created job</response>
    /// <response code="400">If the job data is invalid</response>
    [HttpPost]
    public async Task<ActionResult<JobDTO>> CreateJob([FromBody] CreateJobDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var createdJob = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetJobById), new { jobId = createdJob.Id }, createdJob);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get all jobs asynchronously
    /// </summary>
    /// <returns> A <see cref="IEnumerable{JobDTO}"/> list of jobs </returns>
    /// <response code="200">Returns the list of jobs</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllJobs([FromQuery] int? clientId)
    {
        return Ok(clientId.HasValue ? await _service.GetAllByClientIdAsync(clientId.Value) : await _service.GetAllAsync());
    }

    /// <summary>
    /// Get a job by its ID asynchronously
    /// </summary>
    /// <param name="jobId"> The ID of the job </param>
    /// <returns> The <see cref="JobDTO"/> job if found, otherwise null </returns>
    /// <response code="200">Returns the requested job</response>
    /// <response code="404">If the job is not found</response>
    [HttpGet("{jobId:int}")]
    public async Task<ActionResult<JobDTO>> GetJobById(int jobId)
    {
        var job = await _service.GetByIdAsync(jobId);
        return job is null ? NotFound() : Ok(job);
    }

    /// <summary>
    /// Update an existing job by its ID asynchronously
    /// </summary>
    /// <param name="jobId"> The ID of the job to update </param>
    /// <param name="dto"> The <see cref="UpdateJobDTO"/> job DTO to update </param>
    /// <returns> The updated <see cref="JobDTO"/> if found, otherwise null </returns>
    /// <response code="200">Returns the updated job</response>
    /// <response code="400">If the job data is invalid</response>
    /// <response code="404">If the job is not found</response>
    [HttpPut("{jobId:int}")]
    public async Task<ActionResult<JobDTO>> UpdateJobById(int jobId, [FromBody] UpdateJobDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(jobId, dto);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a job by its ID asynchronously
    /// </summary>
    /// <param name="jobId"> The ID of the job to delete </param>
    /// <returns> The success status of the operation </returns>
    /// <response code="204">Indicates that the job was successfully deleted</response>
    /// <response code="404">If the job is not found</response>
    [HttpDelete("{jobId:int}")]
    public async Task<IActionResult> DeleteJobById(int jobId)
    {
        var success = await _service.DeleteAsync(jobId);
        return success ? NoContent() : NotFound();
    }
}
