
// Developer: migzav 18/03/2025 - Controlador obtener Cuentas

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Operaciones
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Cuentas 
    /// </summary>
    public class requestCuentasModel
    {
        public int? Id { get; set; }
        
            
        public string IdProducto { get; set; }

            
        public string IdProducto_Detalle { get; set; }


        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(255, ErrorMessage = "El campo Descripcion no puede tener más de 255 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Descripcion debe tener al menos 255 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }


        [Required(ErrorMessage = "El campo TipMoneda es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipMoneda no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipMoneda debe tener al menos 1 caracteres")]
        public string TipMoneda { get; set; }

            
        public decimal TasaVigente { get; set; }

            
        public int Plazo { get; set; }

            
        public decimal MontoMin { get; set; }

            
        public string FechaInicioVigencia { get; set; }

        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Cuentas 
    /// </summary>
    [Keyless]
    public class CuentasDBModel
    {

        public string IdProducto { get; set; }

        public string IdProducto_Detalle { get; set; }
        public string Descripcion { get; set; }

        public string TipMoneda { get; set; }
        public decimal TasaVigente { get; set; }

            
        public int Plazo { get; set; }

        public decimal MontoMin { get; set; }

        public string FechaInicioVigencia { get; set; }

        public string Activo { get; set; }
    }


    public class ProductosDBModel
    {

        public string IdProducto { get; set; }

        public string Producto { get; set; }

    }


    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}