using Parking_Zone.Data;
using Parking_Zone.Models;

namespace Parking_Zone.Repositories
{
    public class ParkingSlotRepository : Repository<ParkingSlot>, IParkingSlotRepository
    {
        public ParkingSlotRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
