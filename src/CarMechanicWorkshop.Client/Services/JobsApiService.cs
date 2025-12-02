using System.Net.Http.Json;
using CarMechanicWorkshop.Shared.DTOs.Jobs;

namespace CarMechanicWorkshop.Client.Services;

/// <summary>
/// Service for interacting with the Jobs API endpoints.
/// </summary>
public class JobsApiService
{
    private readonly HttpClient _http;

    public JobsApiService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Retrieves all jobs.
    /// </summary>
    /// <returns> A collection of <see cref="JobDTO"/> objects. </returns>
    public async Task<IEnumerable<JobDTO>?> GetAllAsync() =>
        await _http.GetFromJsonAsync<IEnumerable<JobDTO>>("jobs");

    /// <summary>
    /// Retrieves a job by its ID.
    /// </summary>
    /// <param name="jobId"> The ID of the job to retrieve.</param>
    /// <returns> A <see cref="JobDTO"/> object if found; otherwise, null. </returns>
    public async Task<JobDTO?> GetByIdAsync(int jobId) =>
        await _http.GetFromJsonAsync<JobDTO>($"jobs/{jobId}");

    /// <summary>
    /// Retrieves all jobs by client id filtering.
    /// </summary>
    /// <param name="clientId"> The ID of the client to filter </param>
    /// <returns> A collection of <see cref="JobDTO"/> objects. </returns>
    public async Task<IEnumerable<JobDTO>?> GetAllByClientIdAsync(int clientId) =>
        await _http.GetFromJsonAsync<IEnumerable<JobDTO>>($"jobs?clientId={clientId}");

    /// <summary>
    /// Creates a new job.
    /// </summary>
    /// <param name="dto"> The <see cref="CreateJobDTO"/> data transfer object containing job details.</param>
    /// <returns> True if the job was created successfully; otherwise, false.</returns>
    public async Task<bool> CreateAsync(CreateJobDTO dto)
    {
        var res = await _http.PostAsJsonAsync("jobs", dto);
        return res.IsSuccessStatusCode;
    }

    /// <summary>
    /// Updates an existing job.
    /// </summary>
    /// <param name="jobId"> The ID of the job to update.</param>
    /// <param name="dto"> The <see cref="UpdateJobDTO"/> data transfer object containing updated job details.</param>
    /// <returns> True if the job was updated successfully; otherwise, false.</returns>
    public async Task<bool> UpdateAsync(int jobId, UpdateJobDTO dto)
    {
        var res = await _http.PutAsJsonAsync($"jobs/{jobId}", dto);
        return res.IsSuccessStatusCode;
    }

    /// <summary>
    /// Deletes a job by its ID.
    /// </summary>
    /// <param name="jobId"> The ID of the job to delete.</param>
    /// <returns> True if the job was deleted successfully; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(int jobId)
    {
        var res = await _http.DeleteAsync($"jobs/{jobId}");
        return res.IsSuccessStatusCode;
    }
}
