using Parking_Zone.Models;

namespace Parking_Zone.Services
{
    public interface IParkingSlotService : IService<ParkingSlot>
    {
        public IEnumerable<ParkingSlot> GetByParkingZoneId(Guid parkingZoneId);
    }
}
