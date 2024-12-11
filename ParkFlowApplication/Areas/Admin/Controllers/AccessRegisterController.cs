using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkFlowData.Repository.Interfaces;
using ParkFlowModels.Event;
using ParkFlowModels.Thing;

namespace ParkFlowApplication.Areas.Admin.Controllers;
[Area("Admin")]
public class AccessRegisterController : Controller
{
    private readonly IGenericPersist<Vehicle> vehicleGenericPersist;
    private readonly IGenericPersist<EntryExitAccess> entryExitAccessPersist;
    private readonly IVehiclePersist vehiclePersist;
    private readonly IEntryExitAccessPersist accessPersist;

    public AccessRegisterController
      (
        IGenericPersist<Vehicle> vehicleGenericPersist,
        IGenericPersist<EntryExitAccess> entryExitAccessPersist,
        IVehiclePersist vehiclePersist, 
        IEntryExitAccessPersist accessPersist
      )
    {
        this.vehicleGenericPersist = vehicleGenericPersist;
        this.entryExitAccessPersist = entryExitAccessPersist;
        this.vehiclePersist = vehiclePersist;
        this.accessPersist = accessPersist;
    }
    

    [HttpGet]
    public IActionResult Index(string? licensePlate)
    {
        EntryExitAccess entryExitAccess = new EntryExitAccess();
        if (licensePlate == null)
        {
            return View(entryExitAccess);
        }
        var vehicle = vehiclePersist.GetVehicleByLicensePlate(licensePlate);
        if(vehicle == null)
        {
            TempData["error"] = "Não Localizado!";
            return View(entryExitAccess);
        }
        entryExitAccess.Vehicle = vehicle;
        entryExitAccess.VehicleId = vehicle.Id;

        return View(entryExitAccess);
    }
    [HttpGet]
    public IActionResult AddVehicle()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddVehicle(Vehicle vehicle)
    {
        if(ModelState.IsValid)
        {
            try
            {
                vehicleGenericPersist.Add(vehicle);
                vehicleGenericPersist.SaveChanges();
                TempData["success"] = "Veículo cadastrado com sucesso!";
                return RedirectToAction("Index", "AccessRegister", new { licensePlate = vehicle.LicensePlate});
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("LicensePlate","Veículo já Cadastrado Verifique!");
            }
        }
        return View(vehicle);
    }

    [HttpPost]
    public IActionResult Register(EntryExitAccess entryExitAccess) 
    {
        try
        {
            entryExitAccess.EntryTime = DateTime.Now;
            entryExitAccess.Status = true;
            entryExitAccess.VehicleId = entryExitAccess.VehicleId;
            entryExitAccess.Vehicle = null;
            entryExitAccessPersist.Add(entryExitAccess);
            entryExitAccessPersist.SaveChanges();
            TempData["success"] = "Entrada Registrada com sucesso!";
        }
        catch (DbUpdateException)
        {
            TempData["error"] = "Erro! Entrada não registrada!";
        }
        
        return RedirectToAction("Index");
    }
}
