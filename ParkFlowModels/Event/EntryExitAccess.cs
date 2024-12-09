using ParkFlowModels.Thing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowModels.Event;

public class EntryExitAccess
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Entrada")]
    public DateTime EntryTime { get; set; }
    [Required]
    [DisplayName("Saída")]
    public DateTime ExitTime { get; set; }
    public bool Status { get; set; }
    public int VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
}
