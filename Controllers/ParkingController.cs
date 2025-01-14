using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkingSolution.Entities;
using ParkingSolution.Services;

namespace ParkingSolution.Controllers
{
    // This controller handles HTTP requests related to parking operations.
    [Route("api/v1/parkings")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        // Dependencies: parking repository and AutoMapper.
        private readonly IParkingRepository parkingRepository;
        private readonly IMapper mapper;

        // Constructor to inject dependencies. Throws an exception if either dependency is null.
        public ParkingController(IParkingRepository parkingRepository, IMapper mapper)
        {
            this.parkingRepository = parkingRepository ?? throw new ArgumentNullException(nameof(parkingRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // HTTP GET: Retrieves a list of all parkings.
        // Maps the parking entities from the repository to the model and returns the result.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkings() =>
            Ok(mapper.Map<IEnumerable<Parking>>(await parkingRepository.GetParkingsAsync()));

        // HTTP GET: Retrieves a single parking by its ID.
        // If the parking is found, it's returned, otherwise a 404 Not Found is returned.
        [HttpGet("{parkingId}", Name = "GetParking")]
        public async Task<ActionResult<Parking>> GetParking(Guid parkingId) =>
            await parkingRepository.GetParkingAsync(parkingId) switch
            {
                null => NotFound(),
                var parking => Ok(mapper.Map<Parking>(parking))
            };

        // HTTP POST: Creates a new parking entity.
        // Validates the input model and saves the new parking to the repository.
        [HttpPost]
        public async Task<IActionResult> CreateParking([FromBody] ParkingForCreation parkingForCreation)
        {
            // Check if the input model is null or invalid.
            if (parkingForCreation == null || !ModelState.IsValid)
            {
                return parkingForCreation == null ? BadRequest() : new UnprocessableEntityObjectResult(ModelState);
            }

            // Map the creation model to the parking entity and add it to the repository.
            var parkingEntity = mapper.Map<Parking>(parkingForCreation);
            parkingRepository.AddParking(parkingEntity);
            await parkingRepository.SaveChangesAsync();

            // Return the created parking object.
            return Ok(mapper.Map<Parking>(parkingEntity));
        }

        // HTTP PUT: Updates an existing parking entity by its ID.
        // Validates the input model and updates the parking in the repository.
        [HttpPut("{parkingId}")]
        public async Task<IActionResult> UpdateParking(Guid parkingId, [FromBody] ParkingForUpdate parkingForUpdate)
        {
            // Check if the input model is null or invalid.
            if (parkingForUpdate == null || !ModelState.IsValid)
            {
                return parkingForUpdate == null ? BadRequest() : new UnprocessableEntityObjectResult(ModelState);
            }

            // Retrieve the parking entity to update from the repository.
            var parkingEntity = await parkingRepository.GetParkingAsync(parkingId);
            if (parkingEntity == null) return NotFound();

            // Map the updated model to the entity and update it in the repository.
            mapper.Map(parkingForUpdate, parkingEntity);
            parkingRepository.UpdateParking(parkingEntity);
            await parkingRepository.SaveChangesAsync();

            // Return the updated parking object.
            return Ok(mapper.Map<Parking>(parkingEntity));
        }

        // HTTP DELETE: Deletes a parking entity by its ID.
        // If the parking entity is found, it is deleted, otherwise a 404 Not Found is returned.
        [HttpDelete("{parkingId}")]
        public async Task<IActionResult> DeleteParking(Guid parkingId)
        {
            // Retrieve the parking entity to delete from the repository.
            var parkingEntity = await parkingRepository.GetParkingAsync(parkingId);
            if (parkingEntity == null) return NotFound();

            // Delete the parking entity from the repository.
            parkingRepository.DeleteParking(parkingEntity);
            await parkingRepository.SaveChangesAsync();

            // Return no content to indicate successful deletion.
            return NoContent();
        }
    }
}
