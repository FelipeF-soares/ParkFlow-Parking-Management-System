using Microsoft.AspNetCore.Mvc;
using ParkFlowData.Repository.Interfaces;

namespace ParkFlowApplication.Areas.Admin.Controllers;
[Area("Admin")]
public class ReportsController : Controller
{
    private readonly IEntryExitAccessPersist persist;

    public ReportsController(IEntryExitAccessPersist persist)
    {
        this.persist = persist;
    }
    public IActionResult GetAllVehiclesToday()
    {
        var listToday = persist.GetVehiclesToday().ToList();
        return View(listToday);
    }
}
