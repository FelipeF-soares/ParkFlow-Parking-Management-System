using ParkFlowData.DataBaseContext;
using ParkFlowData.Repository.Interfaces;
using ParkFlowModels.Thing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository;

public class VehiclePersist : IVehiclePersist
{
    private readonly ParkFlowContext context;

    public VehiclePersist(ParkFlowContext context)
    {
        this.context = context;
    }
    public IEnumerable<Vehicle> GetAllVehicles()
    {
        var vehicles = context.Vehicles;
        return vehicles;
    }

    public Vehicle GetVehicleByLicensePlate(string licensePlate)
    {
       var vehicle = context.Vehicles.FirstOrDefault(vehicle => vehicle.LicensePlate == licensePlate);
        return vehicle;
    }

    public void Update(Vehicle vehicle)
    {
        context.Vehicles.Update(vehicle);
    }
}
