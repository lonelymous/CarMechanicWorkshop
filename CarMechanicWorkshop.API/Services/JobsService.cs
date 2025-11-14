using AutoMapper;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Services;

/// <summary>
/// Service for managing JobsDatabase entities
/// </summary>
public class JobsService : IJobsService
{
    private readonly IJobsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public JobsService(IJobsRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all jobs asynchronously
    /// </summary>
    /// <returns>A <see cref="IEnumerable{JobDTO}"/> list of all jobs</returns>
    public async Task<IEnumerable<JobDTO>> GetAllAsync()
    {
        var jobs = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<JobDTO>>(jobs);
    }

    /// <summary>
    /// Get all jobs asynchronously
    /// </summary>
    /// <param name="clientId"> The ID of the client </param>
    /// <returns>A <see cref="IEnumerable{JobDTO}"/> list of all jobs by client</returns>
    public async Task<IEnumerable<JobDTO>> GetAllByClientIdAsync(int clientId)
    {
        var jobs = await _repository.GetAllByClientIdAsync(clientId);
        return _mapper.Map<IEnumerable<JobDTO>>(jobs);
    }

    /// <summary>
    /// Get a job by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job </param>
    /// <returns> The <see cref="JobDTO"/> job if found, otherwise null </returns>
    public async Task<JobDTO?> GetByIdAsync(int id)
    {
        var job = await _repository.GetByIdAsync(id);
        return _mapper.Map<JobDTO?>(job);
    }

    /// <summary>
    /// Create a new job asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="CreateJobDTO"/> job DTO to create </param>
    /// <returns> The created <see cref="JobDTO"/> </returns>
    public async Task<JobDTO> CreateAsync(CreateJobDTO dto)
    {
        // Validation: ManufacturingYear <= 1900
        if (dto.ManufacturingYear <= 1900)
            throw new ArgumentException("The manufacturing year cannot be less than 1900.");

        var entity = _mapper.Map<JobDatabase>(dto);
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
        return _mapper.Map<JobDTO>(entity);
    }

    /// <summary>
    /// Update an existing job asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job to update </param>
    /// <param name="dto"> The <see cref="UpdateJobDTO"/> job DTO to update </param>
    /// <returns> The updated <see cref="JobDTO"/> if found, otherwise null </returns>
    public async Task<JobDTO?> UpdateAsync(int id, UpdateJobDTO dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return null;

        // Validation: ManufacturingYear <= 1900
        if (dto.ManufacturingYear.HasValue && dto.ManufacturingYear.Value < 1900)
            throw new ArgumentException("The manufacturing year cannot be less than 1900.");

        // Validation: Status Progress
        if (dto.Status.HasValue && (existing.Status >= dto.Status.Value)) throw new ArgumentException($"Invalid status transition: {existing.Status} → {dto.Status}. Only forward transitions are allowed.");

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

        return _mapper.Map<JobDTO>(existing);
    }

    /// <summary>
    /// Delete a job by its ID asynchronously
    /// </summary>
    /// <param name="id"> The ID of the job to delete </param>
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
    /// Delete a job asynchronously
    /// </summary>
    /// <param name="dto"> The <see cref="JobDTO"/> job DTO to delete </param>
    /// <returns> The success status of the operation </returns>
    public async Task<bool> DeleteAsync(JobDTO dto)
    {
        var entity = _mapper.Map<JobDatabase>(dto);
        await _repository.DeleteAsync(entity);
        return true;
    }
}
