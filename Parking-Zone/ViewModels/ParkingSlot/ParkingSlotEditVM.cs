using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Enums;
using Parking_Zone.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.ViewModels.ParkingSlot
{
    public class ParkingSlotEditVM
    {
        public ParkingSlotEditVM()
        {
            
        }
        public ParkingSlotEditVM(Models.ParkingSlot slot)
        {
            this.Id = slot.Id;
            this.Number = slot.Number;
            this.ParkingZoneId = slot.ParkingZoneId;
            this.IsAvailableForBooking = slot.IsAvailableForBooking;
            this.Category = slot.Category;
            this.ParkingZoneName = slot.ParkingZone.Name;
        }
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public bool IsAvailableForBooking { get; set; }
        [Required]
        public SlotCategoryEnum Category { get; set; }
        public Guid ParkingZoneId { get; set; }
        public string? ParkingZoneName { get; set; }

        public Models.ParkingSlot MapToModel(Models.ParkingSlot slot)
        {
            slot.Number = this.Number;
            slot.ParkingZoneId = this.ParkingZoneId;
            slot.Category = this.Category;
            slot.IsAvailableForBooking = this.IsAvailableForBooking;
            return slot;
        }
    }
}
