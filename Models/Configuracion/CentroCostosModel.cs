using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Configuracion
{
    public class RequestCentroCostosModel
    {
        public string? IdArea { get; set; }
        public string? IdAgencia { get; set; }
        public string? Valor { get; set; }
        public string Activo { get; set; }
        //public DateTime FecCre { get; set; }
        //public DateTime FecUpd { get; set; }
    }
    public class CentroCostosModel
    {
        public string bEstado { get; set; }
        public string IdArea { get; set; }
        public string IdAgencia { get; set; }
        public string Valor { get; set; }
        public string Activo { get; set; }
        //public DateTime FecCre { get; set; }
        //public DateTime FecUpd { get; set; }
        public string Area { get; set; }
    }
}
