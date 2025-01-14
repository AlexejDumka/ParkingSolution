using ParkingSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace ParkingSolution.Context
{
    public class ParkingSolutionContext : DbContext
    {
       public ParkingSolutionContext(DbContextOptions<ParkingSolutionContext>
    options) : base(options)
        {

        }

        public DbSet<Parking> Parkings { get; set; }


    }
}