using System.Net.Http.Json;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.Client.Services;

/// <summary>
/// Service for interacting with the Clients API endpoints.
/// </summary>
public class ClientsApiService
{
    private readonly HttpClient _http;

    public ClientsApiService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Retrieves all clients.
    /// </summary>
    /// <returns> A collection of <see cref="ClientDTO"/> objects. </returns>
    public async Task<IEnumerable<ClientDTO>?> GetAllAsync() =>
        await _http.GetFromJsonAsync<IEnumerable<ClientDTO>>("clients");

    /// <summary>
    /// Retrieves a client by its ID.
    /// </summary>
    /// <param name="clientId">The ID of the client to retrieve.</param>
    /// <returns> A <see cref="ClientDTO"/> object if found; otherwise, null. </returns>
    public async Task<ClientDTO?> GetByIdAsync(int clientId) =>
        await _http.GetFromJsonAsync<ClientDTO>($"clients/{clientId}");

    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <param name="dto">The <see cref="CreateClientDTO"/> data transfer object containing client details.</param>
    /// <returns>True if the client was created successfully; otherwise, false.</returns>
    public async Task<bool> CreateAsync(CreateClientDTO dto)
    {
        var res = await _http.PostAsJsonAsync("clients", dto);
        return res.IsSuccessStatusCode;
    }

    /// <summary>
    /// Updates an existing client.
    /// </summary>
    /// <param name="clientId">The ID of the client to update.</param>
    /// <param name="dto">The <see cref="UpdateClientDTO"/> data transfer object containing updated client details.</param>
    /// <returns>True if the client was updated successfully; otherwise, false.</returns>
    public async Task<bool> UpdateAsync(int clientId, UpdateClientDTO dto)
    {
        var res = await _http.PutAsJsonAsync($"clients/{clientId}", dto);
        return res.IsSuccessStatusCode;
    }

    /// <summary>
    /// Deletes a client by its ID.
    /// </summary>
    /// <param name="clientId">The ID of the client to delete.</param>
    /// <returns>True if the client was deleted successfully; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(int clientId)
    {
        var res = await _http.DeleteAsync($"clients/{clientId}");
        return res.IsSuccessStatusCode;
    }
}
