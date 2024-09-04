using ParkingSolution.Entities;

namespace ParkingSolution.Services
{
    // Interface defining the contract for the ParkingRepository.
    // This interface abstracts away the data access logic, allowing for a flexible implementation.
    public interface IParkingRepository
    {
        // Retrieves a specific parking entity by its unique identifier.
        // Parameters: 
        // - id: The unique identifier of the parking entity.
        // Returns: A Task representing an asynchronous operation containing the parking entity.
        Task<Parking> GetParkingAsync(Guid id);

        // Retrieves a collection of all parking entities.
        // Returns: A Task representing an asynchronous operation containing a collection of parking entities.
        Task<IEnumerable<Parking>> GetParkingsAsync();

        // Adds a new parking entity to the data source.
        // Parameters:
        // - parkingToAdd: The parking entity to be added.
        void CreateParking(Parking parkingToAdd);

        // Deletes a specific parking entity from the data source.
        // Parameters:
        // - parkingToDelete: The parking entity to be deleted.
        void DeleteParking(Parking parkingToDelete);

        // Saves any pending changes to the data source.
        // Returns: A Task representing an asynchronous operation that completes with a boolean
        // indicating whether the changes were successfully saved.
        Task<bool> SaveChangesAsync();

        // Adds a parking entity. This method is functionally similar to CreateParking,
        // but it may have different internal logic in the repository's implementation.
        // Parameters:
        // - parkingEntity: The parking entity to be added.
        void AddParking(Parking parkingEntity);

        // Updates an existing parking entity in the data source.
        // Parameters:
        // - parkingEntity: The parking entity with updated data.
        void UpdateParking(Parking parkingEntity);
    }
}
