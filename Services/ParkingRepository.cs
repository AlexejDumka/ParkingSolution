using ParkingSolution.Context;
using ParkingSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace ParkingSolution.Services
{
    public class ParkingRepository : IParkingRepository, IDisposable
    {
        private ParkingSolutionContext parkingSolutionContext;
        private ILogger<ParkingRepository> logger;
       
       
        
        public ParkingRepository(ParkingSolutionContext parkingSolutionContext, ILogger<ParkingRepository> logger)
        {
            this.parkingSolutionContext = parkingSolutionContext ?? throw new ArgumentNullException(nameof(parkingSolutionContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Parking> GetParkingAsync(Guid parkingId)
        {
            try
            {

                return await parkingSolutionContext.Parkings.FirstOrDefaultAsync(m => m.Id == parkingId);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message}");
                throw;
            }
        }
            

        public async Task<IEnumerable<Parking>> GetParkingsAsync()
        {
            try
            {

                return await parkingSolutionContext.Parkings.ToListAsync();
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message}");
                throw;
            }
        }

        public void CreateParking(Parking parkingToAdd)
        {
            if (parkingToAdd == null)
            {
                throw new ArgumentNullException(nameof(parkingToAdd));
            }
            try
            {
                parkingSolutionContext.Add(parkingToAdd);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message}");
                throw;
            }
        }

        public void DeleteParking(Parking parkingToDelete)
        {
            if (parkingToDelete == null)
            {
                throw new ArgumentNullException(nameof(parkingToDelete));
            }
            try
            {
                parkingSolutionContext.Remove(parkingToDelete);
        }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message}");
                throw;
            }
}

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await parkingSolutionContext.SaveChangesAsync() > 0);
            }
            catch (Exception exception)
            {
                logger.LogError($"{exception.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (parkingSolutionContext != null)
                {
                    parkingSolutionContext.Dispose();
                    parkingSolutionContext = null;
                }
              
            }
        }

        public void AddParking(Parking parkingEntity)
        {
            parkingSolutionContext.Add(parkingEntity); 
        }

        public void UpdateParking(Parking parkingEntity)
        {
            parkingSolutionContext.Update(parkingEntity);
        }
    }
}

