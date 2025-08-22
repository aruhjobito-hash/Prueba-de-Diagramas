using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Configuracion
{
    public class requestAreasModel
    {
        public string IdArea { get; set; }
        public string Area { get; set; }
        public string IdUser { get; set; }
        public string Activo { get; set; }
    }
    public class AreasModel
    {
        public string bEstado { get; set; }
        public string IdArea { get; set; }
        public string Area { get; set; }
        public string Activo { get; set; }
        //public string? idAgencia { get; set; }
        //public string? Codigo { get; set; }
    }
}
