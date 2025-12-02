using CarMechanicWorkshop.Domain.Entities;

namespace CarMechanicWorkshop.Application.Interfaces.Repositories;

/// <summary>
/// 
/// </summary>
public interface IJobsRepository : IRepository<Job>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public Task<IEnumerable<Job>> GetAllByClientIdAsync(int clientId);
}
