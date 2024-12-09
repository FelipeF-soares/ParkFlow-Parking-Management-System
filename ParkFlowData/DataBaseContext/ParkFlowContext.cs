using Microsoft.EntityFrameworkCore;
using ParkFlowModels.Event;
using ParkFlowModels.Thing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.DataBaseContext;

public class ParkFlowContext : DbContext
{
    public ParkFlowContext(DbContextOptions<ParkFlowContext> options) : base(options)
    {
        
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<EntryExitAccess> EntryExitAccesses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
                    .HasIndex(vehicle => vehicle.LicensePlate)
                    .IsUnique();

    }
}
