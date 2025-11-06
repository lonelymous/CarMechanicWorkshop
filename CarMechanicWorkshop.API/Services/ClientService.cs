using AutoMapper;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Services;

public class ClientService : IClientService
{
    private readonly IRepository<ClientDatabase> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IRepository<ClientDatabase> repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ClientDTO>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClientDTO>>(clients);
    }

    public async Task<ClientDTO?> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        return _mapper.Map<ClientDTO?>(client);
    }

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

    public async Task<bool> DeleteAsync(ClientDTO dto)
    {
        var entity = _mapper.Map<ClientDatabase>(dto);
        await _repository.DeleteAsync(entity);
        return true;
    }
}
