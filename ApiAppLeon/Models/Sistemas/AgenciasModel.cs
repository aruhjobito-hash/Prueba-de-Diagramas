
// Developer: JosAra 09/05/2025 - Controlador para información de las agencias
// DateCreate   : 09/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Sistemas
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Agencias 
    /// </summary>
    public class requestAgenciasModel
    {
        
        [Required(ErrorMessage = "El campo Idagencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo Idagencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Idagencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }
            
        [Required(ErrorMessage = "El campo Agencia es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Agencia no puede tener más de 50 caracteres")]
        [MinLength(50, ErrorMessage = "El campo Agencia debe tener al menos 50 caracteres")]
        public string Agencia { get; set; }

        public DateTime Fechape { get; set; }

        [Required(ErrorMessage = "El campo Direccion es obligatorio")]
        [MaxLength(60, ErrorMessage = "El campo Direccion no puede tener más de 60 caracteres")]
        [MinLength(60, ErrorMessage = "El campo Direccion debe tener al menos 60 caracteres")]
        public string Direccion { get; set; }
            
        public DateTime Fecpro { get; set; }

        [Required(ErrorMessage = "El campo Iduser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo Iduser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Iduser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        public string Hora { get; set; }

        [Required(ErrorMessage = "El campo Abrev es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo Abrev no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Abrev debe tener al menos 4 caracteres")]
        public string Abrev { get; set; }

        [Required(ErrorMessage = "El campo LugVot es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo LugVot no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo LugVot debe tener al menos 4 caracteres")]
        public string LugVot { get; set; }

        [Required(ErrorMessage = "El campo IdUbigeo es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo IdUbigeo no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo IdUbigeo debe tener al menos 8 caracteres")]
        public string IdUbigeo { get; set; }

        public decimal MaximoPoliza { get; set; }

        [Required(ErrorMessage = "El campo ApiToken es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiToken no puede tener más de 150 caracteres")]
        [MinLength(150, ErrorMessage = "El campo ApiToken debe tener al menos 150 caracteres")]
        public string ApiToken { get; set; }

        [Required(ErrorMessage = "El campo ApiTokenVirtual es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiTokenVirtual no puede tener más de 150 caracteres")]
        [MinLength(150, ErrorMessage = "El campo ApiTokenVirtual debe tener al menos 150 caracteres")]
        public string ApiTokenVirtual { get; set; }

        [Required(ErrorMessage = "El campo ApiTokenConvenio es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiTokenConvenio no puede tener más de 150 caracteres")]
        [MinLength(150, ErrorMessage = "El campo ApiTokenConvenio debe tener al menos 150 caracteres")]
        public string ApiTokenConvenio { get; set; }

        [Required(ErrorMessage = "El campo IdRiesgo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo IdRiesgo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo IdRiesgo debe tener al menos 1 caracteres")]
        public string IdRiesgo { get; set; }
        public int opt { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Agencias 
    /// </summary>
    [Keyless]
    public class AgenciasDBModel
    {
        
            
        [Required(ErrorMessage = "El campo Idagencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo Idagencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Idagencia debe tener al menos 2 caracteres")]
        public string Idagencia { get; set; }

            
        [Required(ErrorMessage = "El campo Agencia es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Agencia no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo Agencia debe tener al menos 50 caracteres")]
        public string Agencia { get; set; }

            
        public DateTime Fechape { get; set; }

            
        [Required(ErrorMessage = "El campo Direccion es obligatorio")]
        [MaxLength(60, ErrorMessage = "El campo Direccion no puede tener más de 60 caracteres")]
        //[MinLength(60, ErrorMessage = "El campo Direccion debe tener al menos 60 caracteres")]
        public string Direccion { get; set; }

            
        public DateTime Fecpro { get; set; }

            
        [Required(ErrorMessage = "El campo Iduser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo Iduser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Iduser debe tener al menos 6 caracteres")]
        public string Iduser { get; set; }

            
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        //[MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        public string Hora { get; set; }

            
        [Required(ErrorMessage = "El campo Abrev es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo Abrev no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Abrev debe tener al menos 4 caracteres")]
        public string Abrev { get; set; }

            
        //[Required(ErrorMessage = "El campo LugVot es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo LugVot no puede tener más de 4 caracteres")]
        //[MinLength(4, ErrorMessage = "El campo LugVot debe tener al menos 4 caracteres")]
        public string? LugVot { get; set; }

            
        [Required(ErrorMessage = "El campo IdUbigeo es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo IdUbigeo no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo IdUbigeo debe tener al menos 8 caracteres")]
        public string IdUbigeo { get; set; }

            
        public decimal MaximoPoliza { get; set; }

            
        //[Required(ErrorMessage = "El campo ApiToken es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiToken no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo ApiToken debe tener al menos 150 caracteres")]
        public string? ApiToken { get; set; }

            
        //[Required(ErrorMessage = "El campo ApiTokenVirtual es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiTokenVirtual no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo ApiTokenVirtual debe tener al menos 150 caracteres")]
        public string? ApiTokenVirtual { get; set; }

            
        //[Required(ErrorMessage = "El campo ApiTokenConvenio es obligatorio")]
        [MaxLength(150, ErrorMessage = "El campo ApiTokenConvenio no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo ApiTokenConvenio debe tener al menos 150 caracteres")]
        public string? ApiTokenConvenio { get; set; }

            
        //[Required(ErrorMessage = "El campo IdRiesgo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo IdRiesgo no puede tener más de 1 caracteres")]
        //[MinLength(1, ErrorMessage = "El campo IdRiesgo debe tener al menos 1 caracteres")]
        public string? IdRiesgo { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}