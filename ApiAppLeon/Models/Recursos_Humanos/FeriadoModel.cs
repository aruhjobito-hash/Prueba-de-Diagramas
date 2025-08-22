
// Developer: VicVil 13/03/2025 - Controlador para Registrar los Feriados del Año
// DateCreate   : 13/03/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Recursos_Humanos
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: FeriadoController 
    /// </summary>
    public class requestFeriadoModel
    {
        public int? Id { get; set; }
        
            
        public int? IdFeriado { get; set; }

                
        //[Required(ErrorMessage = "El campo CodFeriado es obligatorio")]
        //[MaxLength(7, ErrorMessage = "El campo CodFeriado no puede tener más de 7 caracteres")]
        //[MinLength(7, ErrorMessage = "El campo CodFeriado debe tener al menos 7 caracteres")]
        public string CodFeriado { get; set; }

            
        //[Required(ErrorMessage = "El campo cNombre es obligatorio")]
        //[MaxLength(100, ErrorMessage = "El campo cNombre no puede tener más de 100 caracteres")]
        //[MinLength(100, ErrorMessage = "El campo cNombre debe tener al menos 100 caracteres")]
        public string cNombre { get; set; }

            
        public DateTime tFecha { get; set; }

            
        //[Required(ErrorMessage = "El campo cTipo es obligatorio")]
        //[MaxLength(50, ErrorMessage = "El campo cTipo no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo cTipo debe tener al menos 50 caracteres")]
        public string cTipo { get; set; }

            
        public string bEsRecurrente { get; set; }

            
        //[Required(ErrorMessage = "El campo cRegion es obligatorio")]
        //[MaxLength(100, ErrorMessage = "El campo cRegion no puede tener más de 100 caracteres")]
        //[MinLength(100, ErrorMessage = "El campo cRegion debe tener al menos 100 caracteres")]
        public string cRegion { get; set; }

            
        public int iAño { get; set; }

            
        public string bActivo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_FeriadoController 
    /// </summary>
    [Keyless]
    public class FeriadoDBModel
    {
        public string CodFeriado { get; set; }

        public string cNombre { get; set; }

        public DateTime tFecha { get; set; }
        
        public string cTipo { get; set; }

        public string bEsRecurrente { get; set; }

        public string cRegion { get; set; }

        public int iaño { get; set; }

        public string bActivo { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}