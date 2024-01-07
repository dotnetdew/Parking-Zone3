using Parking_Zone.Models;
using Parking_Zone.Repositories;

namespace Parking_Zone.Services
{
    public class ParkingSlotService : Service<ParkingSlot>, IParkingSlotService
    {
        private IParkingSlotRepository _parkingSlotRepository;
        public ParkingSlotService(IParkingSlotRepository repository) : base(repository)
        {
            _parkingSlotRepository = repository;
        }

        public override void Insert(ParkingSlot slot)
        {
            slot.Id = Guid.NewGuid();
            base.Insert(slot);
        }

        public IEnumerable<ParkingSlot> GetByParkingZoneId(Guid parkingZoneId)
        { 
            return _parkingSlotRepository.GetAll().Where(x => x.ParkingZoneId == parkingZoneId);
        }
    }
}
