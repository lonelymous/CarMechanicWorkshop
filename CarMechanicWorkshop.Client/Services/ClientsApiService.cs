using System.Net.Http.Json;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.Client.Services;

public class ClientsApiService
{
    private readonly HttpClient _http;

    public ClientsApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ClientDTO>?> GetAllAsync() =>
        await _http.GetFromJsonAsync<IEnumerable<ClientDTO>>("clients");

    public async Task<ClientDTO?> GetByIdAsync(int clientId) =>
        await _http.GetFromJsonAsync<ClientDTO>($"clients/{clientId}");

    public async Task<bool> CreateAsync(CreateClientDTO dto)
    {
        var res = await _http.PostAsJsonAsync("clients", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int clientId, UpdateClientDTO dto)
    {
        var res = await _http.PutAsJsonAsync($"clients/{clientId}", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int clientId)
    {
        var res = await _http.DeleteAsync($"clients/{clientId}");
        return res.IsSuccessStatusCode;
    }
}
