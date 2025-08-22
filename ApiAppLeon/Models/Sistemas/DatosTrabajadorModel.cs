
// Developer: JosAra 08/05/2025 - Controlador para información personal del trabajador
// DateCreate   : 08/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Sistemas
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: DatosTrabajador 
    /// </summary>
    public class requestDatosTrabajadorModel
    {

        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_DatosTrabajador 
    /// </summary>
    [Keyless]
    public class DatosTrabajadorDBModel
    {


        [Required(ErrorMessage = "El campo Soscio es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Soscio no puede tener más de 100 caracteres")]
        //[MinLength(100, ErrorMessage = "El campo Soscio debe tener al menos 100 caracteres")]
        public string Socio { get; set; }


        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Cargo no puede tener más de 100 caracteres")]
        //[MinLength(100, ErrorMessage = "El campo Cargo debe tener al menos 100 caracteres")]
        public string Cargo { get; set; }


        [Required(ErrorMessage = "El campo Area es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Area no puede tener más de 100 caracteres")]
        //[MinLength(100, ErrorMessage = "El campo Area debe tener al menos 100 caracteres")]
        public string Area { get; set; }


        [Required(ErrorMessage = "El campo Agencia es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo Agencia no puede tener más de 30 caracteres")]
        //[MinLength(30, ErrorMessage = "El campo Agencia debe tener al menos 30 caracteres")]
        public string Agencia { get; set; }


        [Required(ErrorMessage = "El campo IdSocio es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdSocio no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdSocio debe tener al menos 7 caracteres")]
        public string IdSocio { get; set; }


        [Required(ErrorMessage = "El campo IdPersona es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdPersona no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdPersona debe tener al menos 7 caracteres")]
        public string IdPersona { get; set; }
        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 7 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 7 caracteres")]
        public string IdCargo { get; set; }
        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 7 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 7 caracteres")]
        public string IdArea { get; set; }
        [Required(ErrorMessage = "El campo IdAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 7 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 7 caracteres")]
        public string IdAgencia { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}