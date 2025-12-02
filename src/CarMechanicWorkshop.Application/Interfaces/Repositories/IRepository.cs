namespace CarMechanicWorkshop.Application.Interfaces.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations.
/// Provides asynchronous methods for retrieving, creating, updating,
/// and deleting entities from the data store.
/// </summary>
/// <typeparam name="T">
/// The entity type being managed. Must be a reference type.
/// </typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>
    /// The entity if found; otherwise <c>null</c>.
    /// </returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/> from the data store.
    /// </summary>
    /// <returns>
    /// A collection of all entities.
    /// </returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the data store.
    /// </summary>
    /// <param name="entity">The entity instance to create.</param>
    Task CreateAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the data store.
    /// </summary>
    /// <param name="entity">The modified entity instance to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the data store by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Deletes an entity instance from the data store.
    /// </summary>
    /// <param name="entity">The entity instance to delete.</param>
    Task DeleteAsync(T entity);
}