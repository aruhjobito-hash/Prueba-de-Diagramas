
// Developer: migzav 26/02/2025 - Controlador para tipo de cambio

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Operaciones
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: TipoCambio 
    /// </summary>
    public class requestTipoCambioModel
    {
        public int? Id { get; set; }

        [MaxLength(10, ErrorMessage = "El campo Fecha no puede tener más de 6 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Fecha debe tener al menos 6 caracteres")]
        public string Fecha { get; set; }


        public decimal Compra { get; set; }

            
        public decimal Venta { get; set; }

            
        public decimal Fijo { get; set; }

        public decimal CompraSunat { get; set; } // migzav 21/03/2025

        public decimal VentaSunat { get; set; } // migzav 21/03/2025


        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_TipoCambio 
    /// </summary>
    [Keyless]
    public class TipoCambioDBModel
    {
        //public int? Id { get; set; }
        
            
        public decimal Compra { get; set; }

            
        public decimal Venta { get; set; }

            
        public decimal Fijo { get; set; }

        public decimal CompraSunat { get; set; } // migzav 21/03/2025

        public decimal VentaSunat { get; set; } // migzav 21/03/2025
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}