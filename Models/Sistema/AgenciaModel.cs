using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Sistema
{

    public class AgenciasDBModel
    {

        public string Idagencia { get; set; }
        public string Agencia { get; set; }
        public DateTime Fechape { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecpro { get; set; }
        public string Iduser { get; set; }
        public string Hora { get; set; }
        public string Abrev { get; set; }
        public string? LugVot { get; set; }
        public string IdUbigeo { get; set; }
        public decimal MaximoPoliza { get; set; }
        public string? ApiToken { get; set; }
        public string? ApiTokenVirtual { get; set; }
        public string? ApiTokenConvenio { get; set; }
        public string? IdRiesgo { get; set; }
    }
}
