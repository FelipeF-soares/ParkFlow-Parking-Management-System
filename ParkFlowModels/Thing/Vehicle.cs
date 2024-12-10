using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowModels.Thing;

public class Vehicle
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Campo obrigatório")]
    [DisplayName("Placa")]
    public string LicensePlate { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [DisplayName("Marca")]
    public string Brand { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [DisplayName("Modelo")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [DisplayName("Cor")]
    public string Color { get; set; }
}
