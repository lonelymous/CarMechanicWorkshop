using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanicWorkshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClientService _service;

    public ClientController(ILogger<ClientController> logger, IClientService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ClientDTO>> CreateClient([FromBody] CreateClientDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
    {
        var clients = await _service.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{clientId:int}")]
    public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
    {
        var client = await _service.GetByIdAsync(clientId);
        return client is null ? NotFound() : Ok(client);
    }

    [HttpPut("{clientId:int}")]
    public async Task<ActionResult<ClientDTO>> UpdateClientById(int clientId, [FromBody] UpdateClientDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(clientId, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{clientId:int}")]
    public async Task<IActionResult> DeleteClientById(int clientId)
    {
        var success = await _service.DeleteAsync(clientId);
        return success ? NoContent() : NotFound();
    }
}
