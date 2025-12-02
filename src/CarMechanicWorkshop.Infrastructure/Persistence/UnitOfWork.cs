using CarMechanicWorkshop.Application.Interfaces;
using CarMechanicWorkshop.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarMechanicWorkshop.Infrastructure.Persistence;

/// <summary>
/// Implements the Unit of Work pattern by coordinating changes across multiple repositories.
/// Manages database transactions and ensures that all operations execute within a controlled
/// and consistent scope.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// The Entity Framework database context used to interact with the underlying data store.
    /// Shared across all repositories within this Unit of Work instance.
    /// </summary>
    private readonly CarMechanicWorkshopContext _context;

    /// <summary>
    /// The currently active database transaction, if any.
    /// Controls commit and rollback behavior for grouped operations.
    /// </summary>
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// Handles lifecycle management for the DbContext and database transactions.
    /// </summary>
    /// <param name="context">The application's EF Core database context.</param>
    public UnitOfWork(CarMechanicWorkshopContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Provides a repository instance for a specific entity type.
    /// A new repository instance is returned on each call.
    /// </summary>
    /// <typeparam name="T">The entity type the repository will manage.</typeparam>
    /// <returns>An instance of <see cref="IRepository{T}"/> configured for the given entity type.</returns>
    public IRepository<T> Repository<T>() where T : class
    {
        return new Repositories.Repository<T>(_context);
    }

    /// <summary>
    /// Starts a new database transaction.  
    /// Use when multiple operations must succeed or fail together.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commits all changes made during the current transaction.
    /// Data becomes permanently stored in the database if commit succeeds.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    /// <summary>
    /// Rolls back the current transaction, undoing all uncommitted changes.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }

    /// <summary>
    /// Saves all pending changes in the DbContext to the database.
    /// Does not automatically commit a transaction unless one is active.
    /// </summary>
    /// <returns>A task representing the asynchronous save operation.</returns>
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    /// <summary>
    /// Releases resources used by the UnitOfWork, including the DbContext and any active transaction.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}