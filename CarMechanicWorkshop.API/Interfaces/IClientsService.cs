using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Interfaces
{
    public interface IClientsService
    {
        /// <summary>
        /// Get all clients asynchronously
        /// </summary>
        /// <returns> A <see cref="IEnumerable{ClientDTO}"/> list of clients </returns>
        public Task<IEnumerable<ClientDTO>> GetAllAsync();

        /// <summary>
        /// Get a client by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the client </param>
        /// <returns> The <see cref="ClientDTO"/> client if found, otherwise null </returns>
        public Task<ClientDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Create a new client asynchronously
        /// </summary>
        /// <param name="client"> The client entity to create </param>
        /// <returns> The created <see cref="ClientDTO"/> </returns>
        public Task<ClientDTO> CreateAsync(CreateClientDTO client);

        /// <summary>
        /// Update an existing client asynchronously
        /// </summary>
        /// <param name="client"> The client entity to update </param>
        /// <returns> The updated <see cref="ClientDTO"/> if found, otherwise null </returns>
        public Task<ClientDTO?> UpdateAsync(int id, UpdateClientDTO client);

        /// <summary>
        /// Delete a client by its ID asynchronously
        /// </summary>
        /// <param name="id"> The ID of the client to delete </param>
        /// <returns> The success status of the operation </returns>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Delete a client asynchronously
        /// </summary>
        /// <param name="entity"> The client entity to delete </param>
        /// <returns> The success status of the operation </returns>
        public Task<bool> DeleteAsync(ClientDTO entity);
    }
}
