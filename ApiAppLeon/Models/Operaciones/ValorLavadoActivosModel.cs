
// Developer: migzav 27/02/2025 - Controlador obtener valor límite de una operación financiera Lavado de Activos

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Operaciones
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: ValorLavadoActivos 
    /// </summary>
    public class requestValorLavadoActivosModel
    {
        //public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo Valor es obligatorio")]
        //[MaxLength(10, ErrorMessage = "El campo Valor no puede tener más de 10 caracteres")]
        //[MinLength(10, ErrorMessage = "El campo Valor debe tener al menos 10 caracteres")]
        public string Valor { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_ValorLavadoActivos 
    /// </summary>
    [Keyless]
    public class ValorLavadoActivosDBModel
    {
        //public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo Valor es obligatorio")]
        //[MaxLength(10, ErrorMessage = "El campo Valor no puede tener más de 10 caracteres")]
        //[MinLength(10, ErrorMessage = "El campo Valor debe tener al menos 10 caracteres")]
        public string Valor { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}