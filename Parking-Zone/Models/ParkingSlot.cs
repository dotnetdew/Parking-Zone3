using Parking_Zone.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Parking_Zone.Models
{
    public class ParkingSlot
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public bool IsAvailableForBooking { get; set; }
        public SlotCategoryEnum Category { get; set; }
        public ParkingZone ParkingZone { get; set; }
        [ForeignKey("ParkingZone")]
        public Guid ParkingZoneId { get; set; }
    }
}
