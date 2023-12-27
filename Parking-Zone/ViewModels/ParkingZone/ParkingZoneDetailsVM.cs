namespace Parking_Zone.ViewModels.ParkingZone
{
    public class ParkingZoneDetailsVM
    {
        public ParkingZoneDetailsVM()
        {
            
        }
        public ParkingZoneDetailsVM(Models.ParkingZone zone)
        {
            Id = zone.Id;
            Name = zone.Name;
            Address = zone.Address;
            Description = zone.Description;
            DateOfEstablishment = zone.DateOfEstablishment;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEstablishment { get; set; }
    }
}
