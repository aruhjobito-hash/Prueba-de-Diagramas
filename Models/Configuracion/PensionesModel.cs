using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Configuracion
{
    public class requestPensionModel
    {

        public string IdPension { get; set; }
        public string PensNombre { get; set; }
        public decimal MontoCV { get; set; }
        public decimal MontoAP { get; set; }
        public decimal MontoSPS { get; set; }
        public decimal MontoSNP { get; set; }
        public decimal MontoMaxSPS { get; set; }
        public decimal MontoCM { get; set; }
        public DateTime PensInicio { get; set; }
        public string IdUser { get; set; }
        public string PensActivo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Pension 
    /// </summary>
    [Keyless]
    public class PensionDBModel
    {
        public string bEstado { get; set; }
        public string IdPension { get; set; }
        public string PensNombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoCV { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoAP { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoSPS { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoSNP { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoMaxSPS { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal MontoCM { get; set; }
        public DateTime PensInicio { get; set; }
        public string PensActivo { get; set; }
    }
}
