
// Developer: VicVil 25/07/2025 - Controlador para PEI
// DateCreate   : 25/07/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: PEI 
    /// </summary>
    public class requestPEIModel
    {
        public int? Id { get; set; }
        
            
        public int IdFeriado { get; set; }

            
        [Required(ErrorMessage = "El campo CodFeriado es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo CodFeriado no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo CodFeriado debe tener al menos 6 caracteres")]
        public string CodFeriado { get; set; }

            
        [Required(ErrorMessage = "El campo cNombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo cNombre no puede tener más de 100 caracteres")]
        [MinLength(100, ErrorMessage = "El campo cNombre debe tener al menos 100 caracteres")]
        public string cNombre { get; set; }

            
        public string tFecha { get; set; }

            
        [Required(ErrorMessage = "El campo cTipo es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo cTipo no puede tener más de 50 caracteres")]
        [MinLength(50, ErrorMessage = "El campo cTipo debe tener al menos 50 caracteres")]
        public string cTipo { get; set; }

            
        public string bEsRecurrente { get; set; }

            
        [Required(ErrorMessage = "El campo cRegion es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo cRegion no puede tener más de 100 caracteres")]
        [MinLength(100, ErrorMessage = "El campo cRegion debe tener al menos 100 caracteres")]
        public string cRegion { get; set; }

            
        public int iAño { get; set; }

            
        public string bActivo { get; set; }
    }


    [Keyless]
    public class PEIDBModel
    {
        public string cDescripcion { get; set; }        
    }

    // 

    [Keyless]
    public class RequestPEIListarDBModel
    {
        [Required(ErrorMessage = "El Campo de iOpcion es necesario")]
        public int iOpcion { get; set; }

        public int iAnio { get; set; }        
    }



}