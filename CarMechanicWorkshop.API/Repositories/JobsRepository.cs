using CarMechanicWorkshop.API.Data;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.API.Repositories;

/// <summary>
/// Repository for managing JobDatabase entities
/// </summary>
public class JobsRepository : IRepository<JobDatabase>
{
    private readonly CarMechanicWorkshopContext _context;
    private readonly DbSet<JobDatabase> _jobs;

    public JobsRepository(CarMechanicWorkshopContext context)
    {
        _context = context;
        _jobs = context.Set<JobDatabase>();
    }

    /// <summary>
    /// Get all jobs asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{JobDatabase}"/> list of all jobs</returns>
    public async Task<IEnumerable<JobDatabase>> GetAllAsync()
        => await _jobs.AsNoTracking().ToListAsync();

    /// <summary>
    /// Get a job by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job </param>
    /// <returns> The <see cref="JobDatabase"/> job if found, otherwise null </returns>
    public async Task<JobDatabase?> GetByIdAsync(int id)
        => await _jobs.AsNoTracking().FirstOrDefaultAsync(j => j.Id == id);

    /// <summary>
    /// Create a new job asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="JobDatabase"/> job entity to create </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task CreateAsync(JobDatabase entity)
    {
        await _jobs.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing job asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="JobDatabase"/> job entity to update </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task UpdateAsync(JobDatabase entity)
    {
        _jobs.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a job by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(int id)
    {
        var job = await GetByIdAsync(id);
        if (job is not null)
        {
            _jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete a job asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="JobDatabase"/> job entity to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(JobDatabase entity)
    {
        _jobs.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
