using CarMechanicWorkshop.Application.Interfaces.Repositories;

namespace CarMechanicWorkshop.Application.Interfaces;

/// <summary>
/// Represents the Unit of Work pattern, which coordinates the work
/// of multiple repositories under a single database transaction.
/// Ensures that changes are saved together, promoting data consistency
/// and atomic operations.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Retrieves a repository instance for the specified entity type.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the entity for which the repository is requested.
    /// Must be a reference type.
    /// </typeparam>
    /// <returns>
    /// A repository instance that provides CRUD operations for
    /// the specified entity type.
    /// </returns>
    IRepository<T> Repository<T>() where T : class;

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the currently active transaction, saving all pending changes.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rolls back the currently active transaction, discarding all pending changes.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RollbackTransactionAsync();

    /// <summary>
    /// Persists all pending changes to the database without handling transaction logic.
    /// Typically used outside explicit transactions.
    /// </summary>
    /// <returns>A task representing the asynchronous save operation.</returns>
    Task SaveAsync();
}
