using ParkFlowModels.Thing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository.Interfaces;

public interface IVehiclePersist
{
    void Update (Vehicle vehicle);
    IEnumerable<Vehicle> GetAllVehicles();
    Vehicle GetVehicleByLicensePlate(string licensePlate);
}
