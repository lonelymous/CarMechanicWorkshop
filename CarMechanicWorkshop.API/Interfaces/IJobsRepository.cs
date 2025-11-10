using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.API.Interfaces
{
    public interface IJobsRepository : IRepository<JobDatabase>
    {
        public Task<IEnumerable<JobDatabase>> GetAllByClientIdAsync(int clientId);
    }
}
