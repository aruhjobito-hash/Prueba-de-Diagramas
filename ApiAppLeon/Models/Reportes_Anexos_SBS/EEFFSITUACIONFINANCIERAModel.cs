
// Developer: VicVil 16/06/2025 - Controlador para Generar EEFF SITUACION FINANCIERA
// DateCreate   : 16/06/2025

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    public class requestEEFFSITUACIONFINANCIERAModel
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
        public string? CodReporteEEFF { get; set; }
    }
    
    [Keyless]
    public class EEFFSITUACIONFINANCIERADBModel
    {
        public long? IdSituacionFinancieraDetalle { get; set; }
        public int? IdSituacionFinanciera { get; set; } 
        public int? IdCuenta { get; set; }
        public decimal? MonedaLocal { get; set; } = 0;
        public decimal? MonedaExtranjera { get; set; } = 0;
        public decimal? Total { get; set; } 
        public bool? Activo { get; set; } 
        public long? Posicion { get; set; } 
    }
    
}