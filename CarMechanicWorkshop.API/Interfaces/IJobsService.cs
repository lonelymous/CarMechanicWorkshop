using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Interfaces
{
    public interface IJobsService
    {
        /// <summary>
        /// Get all jobs asynchronously
        /// </summary>
        /// <returns> A <see cref="IEnumerable{JobDTO}"/> list of jobs </returns>
        public Task<IEnumerable<JobDTO>> GetAllAsync();

        /// <summary>
        /// Get a job by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the job </param>
        /// <returns> The <see cref="JobDTO"/> job if found, otherwise null </returns>
        public Task<JobDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Create a new job asynchronously
        /// </summary>
        /// <param name="job"> The <see cref="CreateJobDTO"/> job entity to create </param>
        /// <returns> The created <see cref="JobDTO"/> </returns>
        public Task<JobDTO> CreateAsync(CreateJobDTO job);

        /// <summary>
        /// Update an existing job asynchronously
        /// </summary>
        /// <param name="job"> The <see cref="UpdateJobDTO"/> job entity to update </param>
        /// <returns> The updated <see cref="JobDTO"/> if found, otherwise null </returns>
        public Task<JobDTO?> UpdateAsync(int id, UpdateJobDTO job);

        /// <summary>
        /// Delete a job by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the job to delete </param>
        /// <returns> The success status of the operation </returns>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Delete a job asynchronously
        /// </summary>
        /// <param name="entity"> The <see cref="JobDTO"/> job entity to delete </param>
        /// <returns> The success status of the operation </returns>
        public Task<bool> DeleteAsync(JobDTO entity);
    }
}
