using CarMechanicWorkshop.API.Data;
using CarMechanicWorkshop.Shared.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.API.Controllers;

/// <summary>
/// Controller for managing clients in the car mechanic workshop system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    /// <summary>
    /// The logger instance for logging information and errors.
    /// </summary>
    private readonly ILogger<ClientController> _logger;
    /// <summary>
    /// The database context for accessing client data.
    /// </summary>
    private readonly CarMechanicWorkshopContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientController"/> class.
    /// </summary>
    /// <param name="context"> The database context.</param>
    /// <param name="logger"> The logger instance.</param>
    public ClientController(CarMechanicWorkshopContext context, ILogger<ClientController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Gets all clients.
    /// </summary>
    /// <returns> An enumerable of clients.</returns>
    [HttpGet]
    public async Task<IEnumerable<ClientDatabase>> Get() => await _context.Clients.Include(u => u.Jobs).ToListAsync();

    /// <summary>
    /// Gets a specific client by ID.
    /// </summary>
    /// <param name="id"> The ID of the client.</param>
    /// <returns> The <see cref="ClientDatabase"/> client with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDatabase>> Get(int id)
    {
        var client = await _context.Clients.Include(u => u.Jobs).FirstOrDefaultAsync(u => u.Id == id);
        return client is null ? NotFound() : client;
    }

    [HttpPost]
    public async Task<ActionResult> Post(ClientDatabase client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ClientDatabase client)
    {
        if (id != client.Id) return BadRequest();
        _context.Entry(client).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client is null) return NotFound();
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
