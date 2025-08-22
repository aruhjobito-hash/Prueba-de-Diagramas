
// Developer: migzav 05/05/2025 - Controlador Mantenedor Presupuesto de Ingresos y Egresos
// DateCreate   : 05/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: PresupuestosIngresosEgresos 
    /// </summary>
    public class requestPresupuestosIngresosEgresosModel
    {
        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Tipo no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Tipo debe tener al menos 4 caracteres")]
        public string Tipo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AfectoKardex solo puede tener el valor '0' o '1'.")]
        public string IdRec { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_PresupuestosIngresosEgresos 
    /// </summary>
    [Keyless]
    public class PresupuestosIngresosEgresosDBModel
    {


        public string CtaContable { get; set; }

        public string CtaNombre { get; set; }

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

        public decimal TotalPresupuesto { get; set; }
    }

    public class CreatePresupuestosIngresosEgresosDBModel
    {
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Tipo no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Tipo debe tener al menos 4 caracteres")]
        public string Tipo { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [MaxLength(16, ErrorMessage = "El campo CtaContable no puede tener más de 16 caracteres")]
        public string CtaContable { get; set; }

        [MaxLength(150, ErrorMessage = "El campo CtaContable no puede tener más de 150 caracteres")]
        public string CtaNombre { get; set; }

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

    }

    [Keyless]
    public class UpdatePresupuestosIngresosEgresosDBModel
    {
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string Año { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Tipo no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Tipo debe tener al menos 4 caracteres")]
        public string Tipo { get; set; }

        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

    }

    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}