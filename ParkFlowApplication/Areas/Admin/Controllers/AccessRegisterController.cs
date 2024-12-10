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
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string? licensePlate)
    {
        EntryExitAccess entryExitAccess = new EntryExitAccess();
        var vehicle = vehiclePersist.GetVehicleByLicensePlate(licensePlate);
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
                return RedirectToAction("Index", "AccessRegister", new { licensePlate = vehicle.LicensePlate });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("LicensePlate","Veículo já Cadastrado Verifique!");
            }
        }
        return View(vehicle);
    }
}
