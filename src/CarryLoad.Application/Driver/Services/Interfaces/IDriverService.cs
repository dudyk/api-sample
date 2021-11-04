using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarryLoad.Application.Driver.Services.Interfaces
{
    public interface IDriverService
    {
        void AddDriverVehicles(Models.Entities.Driver driver, IEnumerable<Models.Entities.Vehicle> vehicles, IEnumerable<int> vehicleIds);
        Task AddVehicleAssignedClaim(Models.Entities.User user);
        Task SetVehicleAssignedStatus(Models.Entities.User user, bool status);
    }
}
