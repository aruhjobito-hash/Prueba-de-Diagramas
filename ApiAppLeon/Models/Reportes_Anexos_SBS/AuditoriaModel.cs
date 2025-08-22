
// Developer: VicVil 19/05/2025 - Controlador para Generar Auditoria de Reportes
// DateCreate   : 19/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: AuditoriaController 
    /// </summary>
    public class requestAuditoriaModel
    {

        [Required(ErrorMessage = "El campo Operación es obligatorio")]
        public int? Operacion { get; set; }

        [Required(ErrorMessage = "El campo NombreTabla es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo NombreTabla no puede tener más de 50 caracteres")]
        [MinLength(3, ErrorMessage = "El campo NombreTabla debe tener al menos 3 caracteres")]
        public string NombreTabla { get; set; }


        [MaxLength(10, ErrorMessage = "El campo CodAnexo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo CodAnexo debe tener al menos 10 caracteres")]
        public string? Codigo { get; set; }

        [MaxLength(2, ErrorMessage = "El campo Mes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Mes debe tener al menos 2 caracteres")]
        public string? cMes { get; set; }

        [MaxLength(4, ErrorMessage = "El campo cAnio no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo cAnio debe tener al menos 4 caracteres")]
        public string? cAnio { get; set; }

    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_AuditoriaController 
    /// </summary>
    [Keyless]
    public class AuditoriaDBModel
    {
        [Required(ErrorMessage = "El campo CodAnexo es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo Mes es obligatorio")]
        public string Mes { get; set; }
        [Required(ErrorMessage = "El campo Año es obligatorio")]
        public string Anio { get; set; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public string Fecha { get; set; }
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        public string Hora { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string Estado { get; set; }

    }

    [Keyless]
    public class AnexoAuditxCodDBModel
    {
        [Required(ErrorMessage = "El campo CodAnexo es obligatorio")]
        public string? CODIGO { get; set; }
        [Required(ErrorMessage = "El campo Mes es obligatorio")]
        public string? MES { get; set; }
        [Required(ErrorMessage = "El campo Año es obligatorio")]
        public string? ANIO { get; set; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public string? USUARIO { get; set; }
        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public string? FECHA { get; set; }
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        public string? HORA { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string? ESTADO { get; set; }
    }

    [Keyless]
    public class CodAnexoDBModel
    {
        public string Codigo { get; set; }
    }

    [Keyless]
    public class requestCodSucaveDBModel
    {
        public int Codigo { get; set; }
    }

    [Keyless]
    public class SucaveDBModel
    { 
        public int? id { get; set; }
        public string? valor { get; set; }
    }

    [Keyless]
    public class SucaveStringDBModel
    {
        public string? id { get; set; }
        public string? valor { get; set; }
    }



}