namespace Parking_Zone.Models
{
    public class ParkingZone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }
    }
}