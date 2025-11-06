using AutoMapper;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Services;

/// <summary>
/// Service for managing ClientDatabase entities
/// </summary>
public class ClientsService : IClientsService
{
    private readonly IRepository<ClientDatabase> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ClientsService(IRepository<ClientDatabase> repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all clients asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{ClientDTO}"/> list of all clients</returns>
    public async Task<IEnumerable<ClientDTO>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClientDTO>>(clients);
    }

    /// <summary>
    /// Get a client by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client </param>
    /// <returns> The <see cref="ClientDTO"/> client if found, otherwise null </returns>
    public async Task<ClientDTO?> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        return _mapper.Map<ClientDTO?>(client);
    }

    /// <summary>
    /// Create a new client asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="CreateClientDTO"/> client DTO to create </param>
    /// <returns> The created <see cref="ClientDTO"/> </returns>
    public async Task<ClientDTO> CreateAsync(CreateClientDTO dto)
    {
        var entity = _mapper.Map<ClientDatabase>(dto);
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        return _mapper.Map<ClientDTO>(entity);
    }

    /// <summary>
    /// Update an existing client asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client to update </param>
    /// <param name="dto"> The <see cref="UpdateClientDTO"/> client DTO to update </param>
    /// <returns> The updated <see cref="ClientDTO"/> if found, otherwise null </returns>
    public async Task<ClientDTO?> UpdateAsync(int id, UpdateClientDTO dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return null;

        _mapper.Map(dto, existing);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _repository.UpdateAsync(existing);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return _mapper.Map<ClientDTO>(existing);
    }

    /// <summary>
    /// Delete a client by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the client to delete </param>
    /// <returns> The success status of the operation </returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _repository.DeleteAsync(existing);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    /// Delete a client asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="ClientDTO"/> client DTO to delete </param>
    /// <returns> The success status of the operation </returns>
    public async Task<bool> DeleteAsync(ClientDTO dto)
    {
        var entity = _mapper.Map<ClientDatabase>(dto);
        await _repository.DeleteAsync(entity);
        return true;
    }
}
