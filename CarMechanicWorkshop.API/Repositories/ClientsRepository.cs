using CarMechanicWorkshop.API.Data;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.API.Repositories;

public class ClientsRepository : IRepository<ClientDatabase>
{
    private readonly CarMechanicWorkshopContext _context;
    private readonly DbSet<ClientDatabase> _clients;

    public ClientsRepository(CarMechanicWorkshopContext context)
    {
        _context = context;
        _clients = context.Set<ClientDatabase>();
    }

    public async Task<IEnumerable<ClientDatabase>> GetAllAsync()
        => await _clients.Include(c => c.Jobs).AsNoTracking().ToListAsync();

    public async Task<ClientDatabase?> GetByIdAsync(int id)
        => await _clients.Include(c => c.Jobs).FirstOrDefaultAsync(c => c.Id == id);

    public async Task CreateAsync(ClientDatabase entity)
    {
        await _clients.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ClientDatabase entity)
    {
        _clients.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await GetByIdAsync(id);
        if (client is not null)
        {
            _clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(ClientDatabase entity)
    {
        _clients.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
