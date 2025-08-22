
// Developer: feragu 05/03/2025 - Controlador para parametros de Contabilidad

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Contabilidad
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: PlaCuentasController 
    /// </summary>
    public class requestPlanCuentasModel
    {
        //public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo CtaContable es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaContable no puede tener más de 16 caracteres")]
        [MinLength(1, ErrorMessage = "El campo CtaContable debe tener al menos 16 caracteres")]
        public string CtaContable { get; set; }


        [Required(ErrorMessage = "El campo Año es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

            
        //[Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        //[MaxLength(150, ErrorMessage = "El campo Descripcion no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo Descripcion debe tener al menos 150 caracteres")]
        public string Descripcion { get; set; }

            
        //[Required(ErrorMessage = "El campo CtaAsiento es obligatorio")]
        //[MaxLength(1, ErrorMessage = "El campo CtaAsiento no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo CtaAsiento debe tener al menos 1 caracteres")]
        public string CtaAsiento { get; set; }

            
        //[Required(ErrorMessage = "El campo NvaCtaContable es obligatorio")]
        //[MaxLength(16, ErrorMessage = "El campo NvaCtaContable no puede tener más de 16 caracteres")]
        //[MinLength(16, ErrorMessage = "El campo NvaCtaContable debe tener al menos 16 caracteres")]
        public string? NvaCtaContable { get; set; }

 
        //[Required(ErrorMessage = "El campo IdUser es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        //public DateTime FecPro { get; set; }

            
        //[Required(ErrorMessage = "El campo Hora es obligatorio")]
        //[MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        //[MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        //public string Hora { get; set; }

    
            
        //[Required(ErrorMessage = "El campo Idarea es obligatorio")]
        //[MaxLength(2, ErrorMessage = "El campo Idarea no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo Idarea debe tener al menos 2 caracteres")]
        //public string? Idarea { get; set; }

    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_PlaCuentasController 
    /// </summary>
    [Keyless]
    public class PlanCuentasDBModel
    {
        //public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo CtaContable es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaContable no puede tener más de 16 caracteres")]
        [MinLength(1, ErrorMessage = "El campo CtaContable debe tener al menos 16 caracteres")]
        public string CtaContable { get; set; }

            
        [Required(ErrorMessage = "El campo Año es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

            
        //[Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        //[MaxLength(150, ErrorMessage = "El campo Descripcion no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo Descripcion debe tener al menos 150 caracteres")]
        public string Descripcion { get; set; }

            
        //[Required(ErrorMessage = "El campo CtaAsiento es obligatorio")]
        //[MaxLength(1, ErrorMessage = "El campo CtaAsiento no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo CtaAsiento debe tener al menos 1 caracteres")]
        public string CtaAsiento { get; set; }

            
        //[Required(ErrorMessage = "El campo NvaCtaContable es obligatorio")]
        //[MaxLength(16, ErrorMessage = "El campo NvaCtaContable no puede tener más de 16 caracteres")]
        //[MinLength(16, ErrorMessage = "El campo NvaCtaContable debe tener al menos 16 caracteres")]
        public string? NvaCtaContable { get; set; }


        public string? area { get; set; }
        public string? idarea { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    [Keyless]
    public class AreaDBModel
    {
        public string? idarea { get; set; }
        public string? Area { get; set; }
        /*Demás tipos de datos para el nuevo modelo*/
    }
    
}