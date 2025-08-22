
// Developer: VicVil 14/06/2025 - Controlador para Generar Reporte6B
// DateCreate   : 14/06/2025

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Reporte6B 
    /// </summary>
    public class requestReporte6BModel
    {
        [Required(ErrorMessage = "El Campo es Obligatorio")]
        public int iOpcion { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string? cAnio { get; set; }

        [MaxLength(2, ErrorMessage = "El campo Mes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Mes debe tener al menos 2 caracteres")]
        public string? cMes { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string? cUsuario { get; set; }

        [MaxLength(10, ErrorMessage = "El campo Codigo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Codigo debe tener al menos 10 caracteres")]
        public string? CodReporte6B { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Reporte6B 
    /// </summary>
    [Keyless]
    public class Reporte6BDBModel
    {
        public int NroFila { get; set; }
        public int IdReporte6BDetalle { get; set; }
        public int IdReporte6B { get; set; }
        public int IdReporte6BTasa { get; set; }
        public string MNTasaAnual { get; set; }
        public string MNSaldoSoles { get; set; }
        public string MNTasaMinima { get; set; }
        public string MNTasaMaxima { get; set; }
        public string METasaAnual { get; set; }
        public string MESaldoDolares { get; set; }
        public string METasaMinima { get; set; }
        public string METasaMaxima { get; set; }
        public bool Activo { get; set; }
    }
    
}