
// Developer: migzav 28/04/2025 - Controlador Mantenedor Presupuestos Inversiones
// DateCreate   : 28/04/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: PresupuestosInversiones 
    /// </summary>
    public class requestPresupuestosInversionesModel
    {
            
        [Required(ErrorMessage = "El campo Año es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [Required(ErrorMessage = "El campo IdAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

            
        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [Required(ErrorMessage = "El campo TipMoneda es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipMoneda no puede tener más de 1 caracter")]
        [MinLength(1, ErrorMessage = "El campo TipMoneda debe tener al menos 1 caracter")]
        public string TipMoneda { get; set; }

    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_PresupuestosInversiones 
    /// </summary>
    [Keyless]
    public class PresupuestosInversionesDBModel
    {

        public string IdArticulo { get; set; }

        public int Cant { get; set; }

        public string NR { get; set; }

        public string IdUnd { get; set; }
        public string Und { get; set; }

        public string Concepto { get; set; }

        public decimal? Enero { get; set; }

        public decimal? Febrero { get; set; }

        public decimal? Marzo { get; set; }

        public decimal? Abril { get; set; }

        public decimal? Mayo { get; set; }

        public decimal? Junio { get; set; }

        public decimal? Julio { get; set; }

        public decimal? Agosto { get; set; }

        public decimal? Septiembre { get; set; }

        public decimal? Octubre { get; set; }

        public decimal? Noviembre { get; set; }

        public decimal? Diciembre { get; set; }

        public decimal? TotalPresupuestado { get; set; }
    }


    public class PresupuestosInversionesEditModel
    {
        [MaxLength(10, ErrorMessage = "El campo IdArticulo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo IdArticulo debe tener al menos 10 caracteres")]
        public string IdArticulo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [MaxLength(4, ErrorMessage = "El campo IdUnd no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdUnd debe tener al menos 4 caracteres")]
        public string IdUnd { get; set; }

        public int Cant { get; set; }

        [MaxLength(1, ErrorMessage = "El campo NR no puede tener más de 1 caracter")]
        [MinLength(1, ErrorMessage = "El campo NR debe tener al menos 1 caracter")]
        public string NR { get; set; }

        public decimal Enero { get; set; }

        public decimal Febrero { get; set; }

        public decimal Marzo { get; set; }

        public decimal Abril { get; set; }

        public decimal Mayo { get; set; }

        public decimal Junio { get; set; }

        public decimal Julio { get; set; }

        public decimal Agosto { get; set; }

        public decimal Septiembre { get; set; }

        public decimal Octubre { get; set; }

        public decimal Noviembre { get; set; }

        public decimal Diciembre { get; set; }

        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Activo solo puede tener el valor '0' o '1'.")]
        public string Activo { get; set; }
        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo TipMoneda solo puede tener el valor '0' o '1'.")]
        public string TipMoneda { get; set; }
    }


    public class RequestFiltroCombos
    {
        [MaxLength(2, ErrorMessage = "El campo IdUser no puede tener más de 2 caracteres")]
        public string? IdCargo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        public string? IdArea { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        public string? IdAgencia { get; set; }
    }

    [Keyless]
    public class AgenciasModel
    {
        public string IdAgencia { get; set; }
        public string Agencia { get; set; }

    }

    [Keyless]
    public class AreasModel
    {
        public string IdArea { get; set; }
        public string Area { get; set; }

    }

    [Keyless]
    public class PresupuestosUpdateModel
    {
        [MaxLength(10, ErrorMessage = "El campo IdArticulo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo IdArticulo debe tener al menos 10 caracteres")]
        public string IdArticulo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo TipMoneda solo puede tener el valor '0' o '1'.")]
        public string TipMoneda { get; set; }
    }

    [Keyless]
    public class PresupuestosFechaCierreModel
    {
        public string Año { get; set; }

        public string IdUser { get; set; }

        public string Fecha { get; set; }

    }

    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}