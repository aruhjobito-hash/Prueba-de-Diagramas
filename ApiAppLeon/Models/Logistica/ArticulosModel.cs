
// Developer: migzav 15/04/2025 - Controlador Mantenedor Articulos
// DateCreate   : 15/04/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Logistica
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Articulos 
    /// </summary>
    [Keyless]
    public class ArticulosDBModel
    {
        
        public string Codigo { get; set; }

        public string IdUniMed { get; set; }

        public string UnidadMedida { get; set; }

        public string Articulo { get; set; }

        public string AfectoIGV { get; set; }

        public string IdGrupo { get; set; }

        public string Grupo { get; set; }

        public string CtaContable { get; set; }

        public string AfectoKardex { get; set; }

        public string Usuario { get; set; }

        public string FechaRegistro { get; set; }

        public string Activo { get; set; }
    }

    public class ArticuloCreateModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "El campo Nombre no puede tener más de 200 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdGrupo no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdGrupo debe tener al menos 4 caracteres")]
        public string IdGrupo { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdUniMed no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdUniMed debe tener al menos 4 caracteres")]
        public string IdUniMed { get; set; }
        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AfectoIGV solo puede tener el valor '0' o '1'.")]
        public string AfectoIGV { get; set; }
        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AfectoKardex solo puede tener el valor '0' o '1'.")]
        public string AfectoKardex { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string Usuario { get; set; }
    }

    public class ArticuloEditModel
    {

        [Required]
        [MaxLength(10, ErrorMessage = "El campo IdArticulo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo IdArticulo debe tener al menos 10 caracteres")]
        public string IdArticulo { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdGrupo no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdGrupo debe tener al menos 4 caracteres")]
        public string IdGrupo { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdUniMed no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdUniMed debe tener al menos 4 caracteres")]
        public string IdUniMed { get; set; }
        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AfectoIGV solo puede tener el valor '0' o '1'.")]
        public string AfectoIGV { get; set; }
        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AfectoKardex solo puede tener el valor '0' o '1'.")]
        public string AfectoKardex { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Activo solo puede tener el valor '0' o '1'.")]
        public string Activo { get; set; }
    }

    public class ArticuloUpdateModel
    {
        [Required]
        [MaxLength(10, ErrorMessage = "El campo IdArticulo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo IdArticulo debe tener al menos 10 caracteres")]
        public string IdArticulo { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }
    }


    //UNIDAD DE MEDIDA
    [Keyless]
    public class UnidadMedidaModel
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]

        public string Usuario { get; set; }
        public string FechaRegistro { get; set; }
        public string Activo { get; set; }
    }

    public class UnidadMedidaCreateModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El campo Descripcion no puede tener más de 50 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string Usuario { get; set; }
    }


    public class UnidadMedidaUpdateModel
    {
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdUniMed no puede tener más de 4 caracteres.")]
        [MinLength(4, ErrorMessage = "El campo IdUniMed debe tener al menos 4 caracteres.")]
        public string IdUniMed { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }
    }

    public class UnidadMedidaEditModel
    {
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdUniMed no puede tener más de 4 caracteres.")]
        [MinLength(4, ErrorMessage = "El campo IdUniMed debe tener al menos 4 caracteres.")]
        public string IdUniMed { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Activo solo puede tener el valor '0' o '1'.")]
        public string Activo { get; set; } 
    }


    //GRUPO

    [Keyless]
    public class GrupoModel
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public string CtaContable { get; set; }
        public decimal TasaDepreciacion { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]

        public string Usuario { get; set; }
        public string FechaRegistro { get; set; }
        public string Activo { get; set; }
        public string ActivoFijo { get; set; }

        public string Kardex { get; set; }
    }

    public class GrupoCreateModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "El campo Descripcion no puede tener más de 50 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(16, ErrorMessage = "El campo CtaContable no puede tener más de 16 caracteres")]
        public string CtaContable { get; set; }

        public decimal TasaDepreciacion { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string Usuario { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo ActivoFijo solo puede tener el valor '0' o '1'.")]
        public string ActivoFijo { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Kardex solo puede tener el valor '0' o '1'.")]
        public string Kardex { get; set; }
    }


    public class GrupoUpdateModel
    {
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdGrupo no puede tener más de 4 caracteres.")]
        [MinLength(4, ErrorMessage = "El campo IdGrupo debe tener al menos 4 caracteres.")]
        public string IdGrupo { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }
    }

    public class GrupoEditModel
    {
        [Required]
        [MaxLength(4, ErrorMessage = "El campo IdGrupo no puede tener más de 4 caracteres.")]
        [MinLength(4, ErrorMessage = "El campo IdGrupo debe tener al menos 4 caracteres.")]
        public string IdGrupo { get; set; }

        public decimal TasaDepreciacion { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres.")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres.")]
        public string Usuario { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Activo solo puede tener el valor '0' o '1'.")]
        public string Activo { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo ActivoFijo solo puede tener el valor '0' o '1'.")]
        public string ActivoFijo { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Kardex solo puede tener el valor '0' o '1'.")]
        public string Kardex { get; set; }
    }

    [Keyless]
    public class PlanCuentasModel
    {

        public string CtaContable { get; set; }
        public string Descripcion { get; set; }


    }














    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}