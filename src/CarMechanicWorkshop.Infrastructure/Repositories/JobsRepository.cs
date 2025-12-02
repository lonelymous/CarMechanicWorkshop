using CarMechanicWorkshop.Application.Interfaces.Repositories;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Job entities
/// </summary>
public class JobsRepository : IRepository<Job>, IJobsRepository
{
    private readonly CarMechanicWorkshopContext _context;
    private readonly DbSet<Job> _jobs;

    public JobsRepository(CarMechanicWorkshopContext context)
    {
        _context = context;
        _jobs = context.Set<Job>();
    }

    /// <summary>
    /// Get all jobs asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{Job}"/> list of all jobs</returns>
    public async Task<IEnumerable<Job>> GetAllAsync()
        => await _jobs.AsNoTracking().ToListAsync();

    /// <summary>
    /// Get all jobs by client id asynchronously
    /// </summary>
    /// <param name="clientId"> The ID of the client </param>
    /// <returns>A <see cref="IEnumerable{Job}"/> list of all jobs by client</returns>
    public async Task<IEnumerable<Job>> GetAllByClientIdAsync(int clientId) => await _context.Jobs
            .Include(j => j.Client)
            .Where(j => j.ClientId == clientId)
            .ToListAsync();

    /// <summary>
    /// Get a job by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job </param>
    /// <returns> The <see cref="Job"/> job if found, otherwise null </returns>
    public async Task<Job?> GetByIdAsync(int id)
        => await _jobs.AsNoTracking().FirstOrDefaultAsync(j => j.Id == id);

    /// <summary>
    /// Create a new job asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="Job"/> job entity to create </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task CreateAsync(Job entity)
    {
        await _jobs.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing job asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="Job"/> job entity to update </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task UpdateAsync(Job entity)
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
    /// <param name="entity"> The <see cref="Job"/> job entity to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(Job entity)
    {
        _jobs.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
