
// Developer: VicVil 30/07/2025 - Controlador para PEI Perspectiva
// DateCreate   : 30/07/2025

using System.ComponentModel.DataAnnotations;
using ApiAppLeon.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Perspectiva 
    /// </summary>
    public class requestPerspectivaModel
    {
        [Required(ErrorMessage = "El campo iOpcion es obligatorio")]
        public int iOpcion { get; set; }
        public string? idUser { get; set; }
        public string? cDescripcion { get; set; }
        public int? CodPerspectiva { get; set; }
    }
    
    [Keyless]
    public class PerspectivaDBModel
    {
        public string valor { get; set; }
        public int CodPerspectiva { get; set; }
        public string cDescripcion { get; set; }
        public bool bEstado { get; set; }
        public string cIduser_creacion { get; set; }
        public DateTime tFec_creacion { get; set; }
        public string? cIduser_mod { get; set; }
        public DateTime? tFec_mod { get; set; }
        
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}