using Microsoft.EntityFrameworkCore;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.API.Data;

// TODO: useless
namespace CarMechanicWorkshop.API.Repositories
{
    /// <summary>
    /// Generic repository for managing entities of type T
    /// </summary>
    /// <typeparam name="T"> The type of the entity </typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CarMechanicWorkshopContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(CarMechanicWorkshopContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Get all entities asynchronously
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> list of all entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        /// <summary>
        /// Get an entity by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the entity </param>
        /// <returns> The entity of type T if found, otherwise null </returns>
        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        /// <summary>
        /// Create a new entity asynchronously
        /// </summary>
        /// <param name="entity"> The entity to create </param>
        /// <returns> A task representing the asynchronous operation </returns>
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an existing entity asynchronously
        /// </summary>
        /// <param name="entity"> The entity to update </param>
        /// <returns> A task representing the asynchronous operation </returns>
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an entity by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the entity to delete </param>
        /// <returns> A task representing the asynchronous operation </returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="entity"> The entity to delete </param>
        /// <returns> A task representing the asynchronous operation </returns>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
