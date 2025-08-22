
// Developer: feragu 14/05/2025 - Controlador para Estruturas Contables de Contabilidad
// DateCreate   : 14/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Contabilidad
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: EstructuraController 
    /// </summary>
    public class requestEstructuraContableModel
    {
        public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo CtaCargo es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaCargo no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaCargo debe tener al menos 16 caracteres")]
        public string CtaCargo { get; set; }

            
        [Required(ErrorMessage = "El campo CtaAbono es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaAbono no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaAbono debe tener al menos 16 caracteres")]
        public string CtaAbono { get; set; }

            
        [Required(ErrorMessage = "El campo IdOpe es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdOpe no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdOpe debe tener al menos 7 caracteres")]
        public string IdOpe { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }

            
        [Required(ErrorMessage = "El campo TipMoneda es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipMoneda no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipMoneda debe tener al menos 1 caracteres")]
        public string TipMoneda { get; set; }

            
        [Required(ErrorMessage = "El campo Concepto es obligatorio")]
        [MaxLength(180, ErrorMessage = "El campo Concepto no puede tener más de 180 caracteres")]
        [MinLength(180, ErrorMessage = "El campo Concepto debe tener al menos 180 caracteres")]
        public string Concepto { get; set; }

            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        public DateTime Fecpro { get; set; }

            
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        public string Hora { get; set; }

            
        [Required(ErrorMessage = "El campo CtaCargoAnt es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaCargoAnt no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaCargoAnt debe tener al menos 16 caracteres")]
        public string CtaCargoAnt { get; set; }

            
        [Required(ErrorMessage = "El campo CtaAbonoAnt es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaAbonoAnt no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaAbonoAnt debe tener al menos 16 caracteres")]
        public string CtaAbonoAnt { get; set; }

            
        [Required(ErrorMessage = "El campo IdOpeAnt es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdOpeAnt no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdOpeAnt debe tener al menos 7 caracteres")]
        public string IdOpeAnt { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_EstructuraController 
    /// </summary>
    [Keyless]
    public class EstructuraContableDBModel
    {
        
            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }

            
        [Required(ErrorMessage = "El campo CtaCargo es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaCargo no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaCargo debe tener al menos 16 caracteres")]
        public string CtaCargo { get; set; }

            
        [Required(ErrorMessage = "El campo CtaAbono es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaAbono no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaAbono debe tener al menos 16 caracteres")]
        public string CtaAbono { get; set; }

            
        [Required(ErrorMessage = "El campo IdOpe es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdOpe no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdOpe debe tener al menos 7 caracteres")]
        public string IdOpe { get; set; }

            
        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Activo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Activo debe tener al menos 1 caracteres")]
        public string Activo { get; set; }

            
        [Required(ErrorMessage = "El campo TipMoneda es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipMoneda no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipMoneda debe tener al menos 1 caracteres")]
        public string TipMoneda { get; set; }

            
        [Required(ErrorMessage = "El campo Concepto es obligatorio")]
        [MaxLength(180, ErrorMessage = "El campo Concepto no puede tener más de 180 caracteres")]
        [MinLength(180, ErrorMessage = "El campo Concepto debe tener al menos 180 caracteres")]
        public string Concepto { get; set; }

            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        public DateTime Fecpro { get; set; }

            
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        public string Hora { get; set; }

            
        [Required(ErrorMessage = "El campo CtaCargoAnt es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaCargoAnt no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaCargoAnt debe tener al menos 16 caracteres")]
        public string CtaCargoAnt { get; set; }

            
        [Required(ErrorMessage = "El campo CtaAbonoAnt es obligatorio")]
        [MaxLength(16, ErrorMessage = "El campo CtaAbonoAnt no puede tener más de 16 caracteres")]
        [MinLength(16, ErrorMessage = "El campo CtaAbonoAnt debe tener al menos 16 caracteres")]
        public string CtaAbonoAnt { get; set; }

            
        [Required(ErrorMessage = "El campo IdOpeAnt es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo IdOpeAnt no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo IdOpeAnt debe tener al menos 7 caracteres")]
        public string IdOpeAnt { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}