
// Developer: JosAra 07/07/2025 - Registro de miembros familiares para trabajadores
// DateCreate   : 07/07/2025

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAppLeon.Models.Recursos_Humanos
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: RegistroMiembrosFam 
    /// </summary>
    public class requestRegistroMiembrosFamModel
    {    
        [Required(ErrorMessage = "El campo IdPersonaTrab es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdPersonaTrab no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdPersonaTrab debe tener al menos 7 caracteres")]
        public string IdPersonaTrab { get; set; }            
        [Required(ErrorMessage = "El campo IdPersonaFam es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdPersonaFam no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdPersonaFam debe tener al menos 7 caracteres")]
        public string IdPersonaFam { get; set; }            
        [Required(ErrorMessage = "El campo IdParentesco es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdParentesco no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdParentesco debe tener al menos 4 caracteres")]
        public string IdParentesco { get; set; }            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }           
        public string Apoderado { get; set; }            
        public DateTime FecPro { get; set; }            
        [Required(ErrorMessage = "El campo hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo hora no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo hora debe tener al menos 8 caracteres")]
        public string hora { get; set; }            
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Estado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Estado debe tener al menos 1 caracteres")]
        public string Estado { get; set; }            
        public DateTime FecModificacion { get; set; }            
        [Required(ErrorMessage = "El campo IdUserModificacion es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUserModificacion no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUserModificacion debe tener al menos 6 caracteres")]
        public string IdUserModificacion { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_RegistroMiembrosFam 
    /// </summary>
    [Keyless]
    public class RegistroMiembrosFamDBModel
    {                    
        public string IdPersonaTrab { get; set; }            
        public string IdPersonaFam { get; set; }
        public string IdParentesco { get; set; }            
        public string IdUser { get; set; }           
        public string Apoderado { get; set; }           
        public DateTime FecPro { get; set; }            
        public string hora { get; set; }            
        public string Estado { get; set; }            
        public DateTime FecModificacion { get; set; }            
        public string IdUserModificacion { get; set; }
    }

    [Table("Personal_MiembrosFam", Schema = "PERSONAL")]
    public class RegistroMiembrosFamTable
    {
        [Key]
        public string IdPersonaTrab { get; set; }
        public string IdPersonaFam { get; set; }
        public string IdParentesco { get; set; }
        public string IdUser { get; set; }
        public string Apoderado { get; set; }
        public DateTime? FecPro { get; set; }
        public string hora { get; set; }
        public string Estado { get; set; }
        public DateTime? FecModificacion { get; set; }
        public string IdUserModificacion { get; set; }
    }

    [Table("ExampleTable")]
    public class ExampleTable
    {
        [Column(TypeName = "varchar(5)")]
        [StringLength(5)]
        public string VarcharColumn { get; set; }
        [Column(TypeName = "money")]
        public decimal MoneyColumn { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        [Precision(16, 2)]
        public decimal DecimalColumn { get; set; }
        public int IntColumn { get; set; }  // INT maps directly to int
        [Column(TypeName = "bit")]
        public bool BitColumn { get; set; }  // BIT maps to bool
        public DateTime DateTimeColumn { get; set; }  // DATETIME maps to DateTime
        [Column(TypeName = "nvarchar(10)")]
        [StringLength(10)]
        public string NvarcharColumn { get; set; }
    }

    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}