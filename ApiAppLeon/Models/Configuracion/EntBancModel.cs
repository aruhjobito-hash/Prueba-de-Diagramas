
// Developer: feragu 04/06/2025 - Controlador para control de entidades bancarias
// DateCreate   : 04/06/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Configuracion
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: EntBanc 
    /// </summary>
    public class requestEntBancModel
    {
 
        [Required(ErrorMessage = "El campo IdEntBanc es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdEntBanc no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdEntBanc debe tener al menos 2 caracteres")]
        public string IdEntBanc { get; set; }

            
        [Required(ErrorMessage = "El campo Entidad es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Entidad no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Entidad debe tener al menos 40 caracteres")]
        public string Entidad { get; set; }

        [Required(ErrorMessage = "El campo Entidad es obligatorio")]
        [MaxLength(15, ErrorMessage = "El campo CorEntidad no puede tener más de 15 caracteres")]
        public string CorEntidad { get; set; }


        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        //public DateTime FecPro { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_EntBanc 
    /// </summary>
    [Keyless]
    public class EntBancDBModel
    {
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo IdEntBanc es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdEntBanc no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdEntBanc debe tener al menos 2 caracteres")]
        public string IdEntBanc { get; set; }

            
        [Required(ErrorMessage = "El campo Entidad es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Entidad no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Entidad debe tener al menos 40 caracteres")]
        public string Entidad { get; set; }
        [Required(ErrorMessage = "El campo Entidad es obligatorio")]
        [MaxLength(15, ErrorMessage = "El campo CorEntidad no puede tener más de 15 caracteres")]
        public string CorEntidad { get; set; }

        //[Required(ErrorMessage = "El campo IdUser es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        //public string IdUser { get; set; }


        //public DateTime FecPro { get; set; }


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