
// Developer: feragu 10/06/2025 - Controlador para control de Cargos
// DateCreate   : 10/06/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Configuracion
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Cargos 
    /// </summary>
    public class requestCargosNModel
    {

        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

            
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Cargo no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Cargo debe tener al menos 40 caracteres")]
        public string Cargo { get; set; }

            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Cargos 
    /// </summary>
    [Keyless]
    public class CargosNDBModel
    {
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

            
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Cargo no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Cargo debe tener al menos 40 caracteres")]
        public string Cargo { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
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