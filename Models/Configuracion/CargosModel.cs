using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Configuracion
{
    public class requestCargosModel
    {
        public string IdCargo { get; set; }
        public string Cargo { get; set; }
        public string IdUser { get; set; }
        public string Activo { get; set; }
    }

    public class CargosDBModel
    {
        public string bEstado { get; set; }
        public string IdCargo { get; set; }
        public string Cargo { get; set; }
        public string Activo { get; set; }
    }
}
