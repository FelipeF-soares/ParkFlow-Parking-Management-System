using Microsoft.EntityFrameworkCore;
using ParkFlowData.DataBaseContext;
using ParkFlowData.Repository.Interfaces;
using ParkFlowModels.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository;

public class EntryExitAccessPersist : IEntryExitAccessPersist
{
    private readonly ParkFlowContext context;

    public EntryExitAccessPersist(ParkFlowContext context)
    {
        this.context = context;
    }
    public IEnumerable<EntryExitAccess> GetAllAccess()
    {
        var access = context.EntryExitAccesses.Include(vehicle => vehicle.Vehicle)
                                              .AsNoTracking();
        return access;
    }

    public IEnumerable<EntryExitAccess> GetVehiclesParked()
    {
        var parked = context.EntryExitAccesses.Include(vehicle => vehicle.Vehicle)
                                              .Where(parked => parked.Status == true)
                                              .AsNoTracking();
        return parked;
    }
    public EntryExitAccess GetExitAccessForId(int id)
    {
        return context.EntryExitAccesses.FirstOrDefault(access => access.Id == id);
    }
}
