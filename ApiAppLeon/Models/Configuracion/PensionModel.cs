
// Developer: feragu 27/05/2025 - Controlador para control de Pensiones
// DateCreate   : 27/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Configuracion
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Pension 
    /// </summary>
    public class requestPensionModel
    {
        [Required(ErrorMessage = "El campo IdPension es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdPension no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdPension debe tener al menos 2 caracteres")]
        public string IdPension { get; set; }

            
        [Required(ErrorMessage = "El campo PensNombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo PensNombre no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo PensNombre debe tener al menos 50 caracteres")]
        public string PensNombre { get; set; }

            
        public decimal MontoCV { get; set; }

            
        public decimal MontoAP { get; set; }

            
        public decimal MontoSPS { get; set; }

            
        public decimal MontoSNP { get; set; }

            
        public decimal MontoMaxSPS { get; set; }

            
        public decimal MontoCM { get; set; }


        public DateTime PensInicio { get; set; }

            
        [Required(ErrorMessage = "El campo IdUserUpd es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUserUpd no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUserUpd debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }
        [Required(ErrorMessage = "El campo PensActivo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo PensActivo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo PensActivo debe tener al menos 1 caracteres")]
        public string PensActivo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Pension 
    /// </summary>
    [Keyless]
    public class PensionDBModel
    {
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo IdPension es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdPension no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdPension debe tener al menos 2 caracteres")]
        public string IdPension { get; set; }

            
        [Required(ErrorMessage = "El campo PensNombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo PensNombre no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo PensNombre debe tener al menos 50 caracteres")]
        public string PensNombre { get; set; }

            
        public decimal MontoCV { get; set; }

            
        public decimal MontoAP { get; set; }

            
        public decimal MontoSPS { get; set; }

            
        public decimal MontoSNP { get; set; }

            
        public decimal MontoMaxSPS { get; set; }

            
        public decimal MontoCM { get; set; }

        public DateTime PensInicio { get; set; }

            
        //[Required(ErrorMessage = "El campo IdUserUpd es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo IdUserUpd no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUserUpd debe tener al menos 6 caracteres")]
        //public string IdUserUpd { get; set; }
        [Required(ErrorMessage = "El campo PensActivo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo PensActivo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo PensActivo debe tener al menos 1 caracteres")]
        public string PensActivo { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}