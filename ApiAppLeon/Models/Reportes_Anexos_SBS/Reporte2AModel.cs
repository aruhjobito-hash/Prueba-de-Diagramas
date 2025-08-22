
// Developer: VicVil 09/06/2025 - Controlador para Generar Reporte 2A
// DateCreate   : 09/06/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    public class requestReporte2AModel
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
        public string? CodReporte2A { get; set; }
    }

    [Keyless]
    public class Reporte2ADBModel
    {
     
        public decimal? f1 { get; set; }
        public decimal? f2 { get; set; }
        public decimal? f3 { get; set; }
        public decimal? f4 { get; set; }
        public decimal? f5 { get; set; }
        public decimal? f6 { get; set; }
        public decimal? f7 { get; set; }
        public decimal? f8 { get; set; }

        
    }
    
}