using CarMechanicWorkshop.Application.Interfaces.Repositories;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Client entities
/// </summary>
public class ClientsRepository : IRepository<Client>
{
    private readonly CarMechanicWorkshopContext _context;
    private readonly DbSet<Client> _clients;

    public ClientsRepository(CarMechanicWorkshopContext context)
    {
        _context = context;
        _clients = context.Set<Client>();
    }

    /// <summary>
    /// Get all clients asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{Client}"/> list of all clients</returns>
    public async Task<IEnumerable<Client>> GetAllAsync()
        => await _clients.Include(c => c.Jobs).AsNoTracking().ToListAsync();

    /// <summary>
    /// Get a client by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client </param>
    /// <returns> The <see cref="Client"/> client if found, otherwise null </returns>
    public async Task<Client?> GetByIdAsync(int id)
        => await _clients.Include(c => c.Jobs).FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Create a new client asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="Client"/> client entity to create </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task CreateAsync(Client entity)
    {
        await _clients.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing client asynchronously
    /// </summary>
    /// <param name="entity"> The <see cref="Client"/> client entity to update </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task UpdateAsync(Client entity)
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
    /// <param name="entity"> The <see cref="Client"/> client entity to delete </param>
    /// <returns> A task representing the asynchronous operation </returns>
    public async Task DeleteAsync(Client entity)
    {
        _clients.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
