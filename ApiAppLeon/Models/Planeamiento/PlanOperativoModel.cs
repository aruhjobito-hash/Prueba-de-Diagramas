
// Developer: VicVil 30/07/2025 - Controlador para PEI Plan Operativo
// DateCreate   : 30/07/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{

    public class requestPlanOperativoModel
    {
        [Required(ErrorMessage = "El campo iOpcion es obligatorio")]
        public int iOpcion { get; set; }
        public string? idUser { get; set; }
        public string? cDescripcion { get; set; }
        public int? iAnio { get; set; }
        public int? CodPlanOperativo { get; set; }
    }

    [Keyless]
    public class PlanOperativoDBModel
    {
        public string valor { get; set; }
        public int CodPlanOperativo { get; set; }
        public string cDescripcion { get; set; }
        public int iAnio { get; set; }
        public bool bEstado { get; set; }
        public string cIduser_creacion { get; set; }
        public DateTime tFec_creacion { get; set; }
        public string? cIduser_mod { get; set; }
        public DateTime? tFec_mod { get; set; }
    }
    
}