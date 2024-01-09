using Parking_Zone.Enums;
using System.ComponentModel;

namespace Parking_Zone.ViewModels.ParkingSlot
{
    public class ParkingSlotListItemVM
    {
        public ParkingSlotListItemVM(Models.ParkingSlot slot)
        {
            this.Id = slot.Id;
            this.Number = slot.Number;
            this.IsAvailableForBooking = slot.IsAvailableForBooking;
            this.Category = slot.Category;
        }
        public Guid Id { get; set; }
        public int Number { get; set; }
        public bool IsAvailableForBooking { get; set; }
        public SlotCategoryEnum Category { get; set; }
    }
}
