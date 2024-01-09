using Parking_Zone.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.ViewModels.ParkingSlot
{
    public class ParkingSlotCreateVM
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public bool IsAvailableForBooking { get; set; }
        [Required]
        public SlotCategoryEnum Category { get; set; }
        public Guid ParkingZoneId { get; set; }
        public string? ParkingZoneName { get; set; }

        public Models.ParkingSlot MapToModel()
        {
            return new Models.ParkingSlot()
            {
                Number = this.Number,
                Category = this.Category,
                ParkingZoneId = this.ParkingZoneId,
                IsAvailableForBooking = this.IsAvailableForBooking
            };
        }
    }
}
