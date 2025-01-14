using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSolution.Entities
{
    [Table("Parking")]
    public class Parking
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? ParkingSpot { get; set; }

        public DateTime ReservationDateFrom { get; set; }

        public DateTime ReservationDateTo { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
