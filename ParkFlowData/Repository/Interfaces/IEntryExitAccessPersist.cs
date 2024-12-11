using ParkFlowModels.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository.Interfaces;

public interface IEntryExitAccessPersist
{
    IEnumerable<EntryExitAccess> GetAllAccess();
    IEnumerable<EntryExitAccess> GetVehiclesParked();
    EntryExitAccess GetExitAccessForId(int id);

}
