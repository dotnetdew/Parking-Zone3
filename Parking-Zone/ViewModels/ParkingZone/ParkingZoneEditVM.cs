using Parking_Zone.Models;
namespace Parking_Zone.ViewModels.ParkingZone
{
    public class ParkingZoneEditVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public ParkingZoneEditVM()
        {
            
        }
        public ParkingZoneEditVM(Models.ParkingZone zone)
        {
            this.Id = zone.Id;
            this.Name = zone.Name;
            this.Address = zone.Address;
            this.Description = zone.Description;
        }
        public Models.ParkingZone MapToModel(Models.ParkingZone zone)
        {
            zone.Name = Name;
            zone.Address = Address;
            zone.Description = Description;

            return zone;
        }
    }
}
