
// Developer: JosAra 12/05/2025 - Controlador para información de los Cargos
// DateCreate   : 12/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Sistemas
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Cargos 
    /// </summary>
    public class requestCargosModel
    {
                   
        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

            
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Cargo no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Cargo debe tener al menos 40 caracteres")]
        public string Cargo { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }

            
        //public int opt { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Cargos 
    /// </summary>
    [Keyless]
    public class CargosDBModel
    {
        
         public string bEstado { get; set; }
        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

            
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        //[MaxLength(40, ErrorMessage = "El campo Cargo no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Cargo debe tener al menos 40 caracteres")]
        public string Cargo { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}