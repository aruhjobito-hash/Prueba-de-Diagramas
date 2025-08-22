
// Developer: josara 02/05/2025 - Controlador para mantenedor de Perfiles
// DateCreate   : 02/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Sistemas
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Perfil 
    /// </summary>
    public class requestPerfilModel
    {

        public int IdPerfil { get; set; }
        [Required(ErrorMessage = "El campo ActivePerfil es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo ActivePerfil no puede tener más de 30 caracteres")]
        //[MinLength(30, ErrorMessage = "El campo ActivePerfil debe tener al menos 30 caracteres")]
        public string ActivePerfil { get; set; }
        public DateTime fecpro { get; set; }
        [Required(ErrorMessage = "El campo hora es obligatorio")]
        [MaxLength(5, ErrorMessage = "El campo hora no puede tener más de 5 caracteres")]
        [MinLength(5, ErrorMessage = "El campo hora debe tener al menos 5 caracteres")]
        public string hora { get; set; }
        [Required(ErrorMessage = "El campo iduser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo iduser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo iduser debe tener al menos 6 caracteres")]
        public string iduser { get; set; }
        [Required(ErrorMessage = "El campo iduserR es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo iduserR no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo iduserR debe tener al menos 6 caracteres")]
        public string iduserR { get; set; }
        [Required(ErrorMessage = "El campo idAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idAgencia debe tener al menos 2 caracteres")]
        public string idAgencia { get; set; }
        [Required(ErrorMessage = "El campo idcargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idcargo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idcargo debe tener al menos 2 caracteres")]        
        public string idcargo { get; set; }
        [Required(ErrorMessage = "El campo idarea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idarea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idarea debe tener al menos 2 caracteres")]
        public string idarea { get; set; }
        [Required(ErrorMessage = "El campo TokenFijo es obligatorio")]
        [MaxLength(200, ErrorMessage = "El campo TokenFijo no puede tener más de 200 caracteres")]
        //[MinLength(200, ErrorMessage = "El campo TokenFijo debe tener al menos 200 caracteres")]
        public string TokenFijo { get; set; }
        public int opt { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Perfil 
    /// </summary>
    [Keyless]
    public class PerfilDBModel
    {
        public int IdPerfil { get; set; }
        [Required(ErrorMessage = "El campo ActivePerfil es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo ActivePerfil no puede tener más de 30 caracteres")]
        [MinLength(30, ErrorMessage = "El campo ActivePerfil debe tener al menos 30 caracteres")]
        public string ActivePerfil { get; set; }
        public DateTime fecpro { get; set; }
        [Required(ErrorMessage = "El campo hora es obligatorio")]
        [MaxLength(5, ErrorMessage = "El campo hora no puede tener más de 5 caracteres")]
        [MinLength(5, ErrorMessage = "El campo hora debe tener al menos 5 caracteres")]
        public string hora { get; set; }
        [Required(ErrorMessage = "El campo iduser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo iduser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo iduser debe tener al menos 6 caracteres")]
        public string iduser { get; set; }
        [Required(ErrorMessage = "El campo iduserR es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo iduserR no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo iduserR debe tener al menos 6 caracteres")]
        public string iduserR { get; set; }
        [Required(ErrorMessage = "El campo idAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idAgencia debe tener al menos 2 caracteres")]
        public string idAgencia { get; set; }
        [Required(ErrorMessage = "El campo idcargo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idcargo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idcargo debe tener al menos 2 caracteres")]
        public string idcargo { get; set; }
        [Required(ErrorMessage = "El campo idarea es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo idarea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo idarea debe tener al menos 2 caracteres")]
        public string idarea { get; set; }
        [Required(ErrorMessage = "El campo TokenFijo es obligatorio")]
        [MaxLength(200, ErrorMessage = "El campo TokenFijo no puede tener más de 200 caracteres")]
        [MinLength(200, ErrorMessage = "El campo TokenFijo debe tener al menos 200 caracteres")]
        public string? TokenFijo { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Estado no puede tener más de 200 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Estado debe tener al menos 200 caracteres")]
        public string? Estado { get; set; }
        //public int opt { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}