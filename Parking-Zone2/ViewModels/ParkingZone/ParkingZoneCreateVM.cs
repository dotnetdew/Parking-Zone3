using Parking_Zone.Models;
namespace Parking_Zone.ViewModels.ParkingZone
{
    public class ParkingZoneCreateVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Models.ParkingZone MapToModel()
        {
            return new Models.ParkingZone
            {
                Name = this.Name,
                Address = this.Address,
                Description = this.Description,
            };
        }
    }
}
