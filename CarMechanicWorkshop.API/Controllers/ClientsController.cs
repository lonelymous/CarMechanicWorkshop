using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanicWorkshop.API.Controllers;


/// <summary>
/// API controller for managing clients
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> _logger;
    private readonly IClientsService _service;

    public ClientsController(ILogger<ClientsController> logger, IClientsService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Create a new client asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="CreateClientDTO"/> client DTO to create </param>
    /// <returns> The created <see cref="ClientDTO"/> </returns>
    /// <response code="201">Returns the created client</response>
    /// <response code="400">If the client data is invalid</response>
    [HttpPost]
    public async Task<ActionResult<ClientDTO>> CreateClient([FromBody] CreateClientDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdClient = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetClientById), new { clientId = createdClient.Id }, createdClient);
    }

    /// <summary>
    /// Get all clients asynchronously
    /// </summary>
    /// <returns> A <see cref="IEnumerable{ClientDTO}"/> list of clients </returns>
    /// <response code="200">Returns the list of clients</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
    {
        var clients = await _service.GetAllAsync();
        return Ok(clients);
    }

    /// <summary>
    /// Get a client by its ID asynchronously
    /// </summary>
    /// <param name="clientId"> The ID of the client </param>
    /// <returns> The <see cref="ClientDTO"/> client if found, otherwise null </returns>
    /// <response code="200">Returns the requested client</response>
    /// <response code="404">If the client is not found</response>
    [HttpGet("{clientId:int}")]
    public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
    {
        var client = await _service.GetByIdAsync(clientId);
        return client is null ? NotFound() : Ok(client);
    }

    /// <summary>
    /// Update an existing client by its ID asynchronously
    /// </summary>
    /// <param name="clientId"> The ID of the client to update </param>
    /// <param name="dto"> The <see cref="UpdateClientDTO"/> client DTO to update </param>
    /// <returns> The updated <see cref="ClientDTO"/> if found, otherwise null </returns>
    /// <response code="200">Returns the updated client</response>
    /// <response code="400">If the client data is invalid</response>
    /// <response code="404">If the client is not found</response>
    [HttpPut("{clientId:int}")]
    public async Task<ActionResult<ClientDTO>> UpdateClientById(int clientId, [FromBody] UpdateClientDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(clientId, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Delete a client by its ID asynchronously
    /// </summary>
    /// <param name="clientId"> The ID of the client to delete </param>
    /// <returns> The success status of the operation </returns>
    /// <response code="204">Indicates that the client was successfully deleted</response>
    /// <response code="404">If the client is not found</response>
    [HttpDelete("{clientId:int}")]
    public async Task<IActionResult> DeleteClientById(int clientId)
    {
        var success = await _service.DeleteAsync(clientId);
        return success ? NoContent() : NotFound();
    }
}
