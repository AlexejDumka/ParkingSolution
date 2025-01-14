using System.ComponentModel.DataAnnotations;

namespace ParkingSolution.Entities
{
    public class ParkingForUpdate
    {
        [Key]
        public Guid Id { get { return Id; } set { Id = Guid.NewGuid(); } }

        [Required]
        public string? ParkingSpot { get; set; }

        public DateTime ReservationDateFrom { get; set; }

        public DateTime ReservationDateTo { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
