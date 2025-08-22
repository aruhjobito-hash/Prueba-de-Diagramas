using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Sistema
{
    public class requestDatosTrabajadorModel
    {

        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }
    }
    public class DatosTrabajadorDBModel
    {
        [Required(ErrorMessage = "El campo Soscio es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Soscio no puede tener más de 100 caracteres")]
        public string Socio { get; set; }
        [Required(ErrorMessage = "El campo Cargo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Cargo no puede tener más de 100 caracteres")]
        public string Cargo { get; set; }
        [Required(ErrorMessage = "El campo Area es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Area no puede tener más de 100 caracteres")]
        public string Area { get; set; }
        [Required(ErrorMessage = "El campo Agencia es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo Agencia no puede tener más de 30 caracteres")]
        public string Agencia { get; set; }
        [Required(ErrorMessage = "El campo IdSocio es obligatorio")]
        public string IdSocio { get; set; }
        [Required(ErrorMessage = "El campo IdPersona es obligatorio")]
        public string IdPersona { get; set; }
        [Required(ErrorMessage = "El campo IdCargo es obligatorio")]
        public string IdCargo { get; set; }
        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        public string IdArea { get; set; }
        [Required(ErrorMessage = "El campo IdArea es obligatorio")]
        public string IdAgencia { get; set; }
    }
}
