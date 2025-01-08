using Microsoft.AspNetCore.Mvc;
using ParkFlowData.Repository.Interfaces;
using ParkFlowModels.Report;

namespace ParkFlowApplication.Areas.Admin.Controllers;
[Area("Admin")]
public class ReportsController : Controller
{
    private readonly IEntryExitAccessPersist persist;
    private readonly IWebHostEnvironment web;

    public ReportsController(IEntryExitAccessPersist persist, IWebHostEnvironment web)
    {
        this.persist = persist;
        this.web = web;
    }
    public IActionResult GetAllVehiclesToday()
    {
        var listToday = persist.GetVehiclesToday().ToList();
        return View(listToday);
    }
    public IActionResult ReportGetAllVehicleToday()
    {
        var accesses = persist.GetVehiclesToday().ToList();
        string wwwRootPath = web.WebRootPath;
        VehiclesToday reportVehiclesToday = new VehiclesToday(wwwRootPath);
        var path = reportVehiclesToday.ReportPDF(accesses);
        if(!System.IO.File.Exists(path))
        {
            return NotFound("O Arquivo não foi encontrado");
        }
        byte[] fileByte = System.IO.File.ReadAllBytes(path);
        return File(fileByte, "application/pdf", "Relatorio_De_Veiculos.pdf");
    }
}
