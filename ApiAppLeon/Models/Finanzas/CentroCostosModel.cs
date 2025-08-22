
// Developer: feragu 22/05/2025 - Controlador para control de Centro de Costos
// DateCreate   : 22/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Finanzas
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: CentroCostos 
    /// </summary>
    public class requestCentroCostosModel
    {

        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

            
        [Required(ErrorMessage = "El campo IdAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

            
        //[Required(ErrorMessage = "El campo Valor es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo Valor no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo Valor debe tener al menos 2 caracteres")]
        public string Valor { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_CentroCostos 
    /// </summary>
    [Keyless]
    public class CentroCostosDBModel
    {
        
            
        //[Required(ErrorMessage = "El campo bEstado es obligatorio")]
        //[MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

            
        [Required(ErrorMessage = "El campo IdAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

            
        [Required(ErrorMessage = "El campo Valor es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo Valor no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo Valor debe tener al menos 2 caracteres")]
        public string Valor { get; set; }

            
        //[Required(ErrorMessage = "El campo Activo es obligatorio")]
        //[MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
        public string Area { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}