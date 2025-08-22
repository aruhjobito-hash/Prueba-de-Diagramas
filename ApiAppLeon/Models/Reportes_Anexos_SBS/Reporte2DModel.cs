
// Developer: VicVil 13/06/2025 - Controlador para Generar Reporte2D
// DateCreate   : 13/06/2025

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Reporte2D 
    /// </summary>
    public class requestReporte2DModel
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
        public string? CodReporte2D { get; set; }
    }
    
    [Keyless]
    public class Reporte2DDBModel
    {
        public int NroFila { get; set; }
        public int IdReporte2D_Detalle { get; set; }
        public int IdReporte2D { get; set; }
        public int IdReporte2D_Descripcion { get; set; }
        public string APR { get; set; }
        public string? Total { get; set; }
        public bool activo { get; set; }
    }
    
}