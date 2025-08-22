
// Developer: feragu 09/06/2025 - Controlador para control de niveles de trabajadores
// DateCreate   : 09/06/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Configuracion
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: NivelCategPers 
    /// </summary>
    public class requestNivelPersModel
    {

        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdNivelPers debe tener al menos 2 caracteres")]
        public string IdNivelPers { get; set; }

            
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Descripcion no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Descripcion debe tener al menos 40 caracteres")]
        public string Descripcion { get; set; }

            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        //public DateTime FecPro { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_NivelCategPers 
    /// </summary>
    [Keyless]
    public class NivelPersDBModel
    {
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdNivelPers debe tener al menos 2 caracteres")]
        public string IdNivelPers { get; set; }

            
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Descripcion no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Descripcion debe tener al menos 40 caracteres")]
        public string Descripcion { get; set; }

            
        //[Required(ErrorMessage = "El campo IdUser es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        //public string IdUser { get; set; }

            
        //public DateTime FecPro { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }

    [Keyless]
    public class requestCategPersModel
    {
        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        public string IdCategoriaPers { get; set; }

        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdNivelPers debe tener al menos 2 caracteres")]
        public string IdNivelPers { get; set; }


        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Descripcion no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Descripcion debe tener al menos 40 caracteres")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }


        //public DateTime FecPro { get; set; }


        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_NivelCategPers 
    /// </summary>
    [Keyless]
    public class CategPersDBModel
    {


        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        public string IdCategoriaPers { get; set; }

        [Required(ErrorMessage = "El campo IdNivelPers es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdNivelPers no puede tener más de 2 caracteres")]
        //[MinLength(2, ErrorMessage = "El campo IdNivelPers debe tener al menos 2 caracteres")]
        public string IdNivelPers { get; set; }


        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo Descripcion no puede tener más de 40 caracteres")]
        //[MinLength(40, ErrorMessage = "El campo Descripcion debe tener al menos 40 caracteres")]
        public string Descripcion { get; set; }


        //[Required(ErrorMessage = "El campo IdUser es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        //public string IdUser { get; set; }


        //public DateTime FecPro { get; set; }


        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }
    }
}