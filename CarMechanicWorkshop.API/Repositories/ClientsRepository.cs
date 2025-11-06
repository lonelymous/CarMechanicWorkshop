using CarMechanicWorkshop.API.Data;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.API.Repositories;

/// <summary>
/// Repository for managing ClientDatabase entities
/// </summary>
public class ClientsRepository : IRepository<ClientDatabase>
{
    private readonly CarMechanicWorkshopContext _context;
    private readonly DbSet<ClientDatabase> _clients;

    public ClientsRepository(CarMechanicWorkshopContext context)
    {
        _context = context;
        _clients = context.Set<ClientDatabase>();
    }

    /// <summary>
    /// Get all clients asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{ClientDatabase}"/> list of all clients</returns>
    public async Task<IEnumerable<ClientDatabase>> GetAllAsync()
        => await _clients.Include(c => c.Jobs).AsNoTracking().ToListAsync();

    /// <summary>
    /// Get a client by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client </param>
    /// <returns> The <see cref="ClientDatabase"/> client if found, otherwise null </returns>
    public async Task<ClientDatabase?> GetByIdAsync(int id)
        => await _clients.Include(c => c.Jobs).FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Create a new client asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="ClientDatabase"/> client entity to create </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task CreateAsync(ClientDatabase entity)
    {
        await _clients.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing client asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="ClientDatabase"/> client entity to update </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task UpdateAsync(ClientDatabase entity)
    {
        _clients.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a client by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(int id)
    {
        var client = await GetByIdAsync(id);
        if (client is not null)
        {
            _clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete a client asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="ClientDatabase"/> client entity to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(ClientDatabase entity)
    {
        _clients.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
