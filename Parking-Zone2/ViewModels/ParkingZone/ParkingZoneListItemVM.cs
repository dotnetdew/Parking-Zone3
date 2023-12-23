namespace Parking_Zone.ViewModels.ParkingZone
{
    public class ParkingZoneListItemVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfEstablishment { get; set; }

        public ParkingZoneListItemVM(Models.ParkingZone zone)
        {
            Id = zone.Id;
            Name = zone.Name;
            Address = zone.Address;
            DateOfEstablishment = zone.DateOfEstablishment;
        }
    }
}
