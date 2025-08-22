
// Developer: VicVil 30/07/2025 - Controlador para PEI RESPONSABLES
// DateCreate   : 30/07/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: RESPONSABLES 
    /// </summary>
    public class requestRESPONSABLESModel
    {
        [Required(ErrorMessage = "El campo iOpcion es obligatorio")]
        public int iOpcion { get; set; }
        public string? idUser { get; set; }
        public string? NombreR { get; set; }
        public string? NombreC { get; set; }
        public int? CodResponsable { get; set; }

    }

    [Keyless]
    public class RESPONSABLESDBModel
    {
        public string valor { get; set; }
        public int CodResponsable { get; set; }
        public string? cNombre_responsable { get; set; }
        public string? cNombre_corto { get; set; }
        public bool bEstado { get; set; }
        public string cIduser_creacion { get; set; }
        public DateTime tFec_creacion { get; set; }
        public string? cIduser_mod { get; set; }
        public DateTime? tFec_mod { get; set; }    
    }
    
}