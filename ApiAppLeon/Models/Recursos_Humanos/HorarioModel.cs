
// Developer: VicVil 05/03/2025 - Controlador para Registrar los Horarios de Empleados
// DateCreate   : 05/03/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Recursos_Humanos
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: HorarioController 
    /// </summary>
    public class requestHorarioModel
    {
        public int? Id { get; set; }
        
            
        public int? IdHorario { get; set; }

            
        //[Required(ErrorMessage = "El campo CodHorario es obligatorio")]
        //[MaxLength(6, ErrorMessage = "El campo CodHorario no puede tener más de 6 caracteres")]
        //[MinLength(6, ErrorMessage = "El campo CodHorario debe tener al menos 6 caracteres")]
        public string? CodHorario { get; set; }

            
        [Required(ErrorMessage = "El campo cNombreHorario es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo cNombreHorario no puede tener más de 50 caracteres")]
        [MinLength(5, ErrorMessage = "El campo cNombreHorario debe tener al menos 5 caracteres")]
        public string cNombreHorario { get; set; }

            
        [Required(ErrorMessage = "El campo cDescripcionHorario es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo cDescripcionHorario no puede tener más de 100 caracteres")]
        [MinLength(10, ErrorMessage = "El campo cDescripcionHorario debe tener al menos 10 caracteres")]
        public string cDescripcionHorario { get; set; }

            
        public string bActivo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_HorarioController 
    /// </summary>
    [Keyless]
    public class HorarioDBModel
    {
        
            
        [Required(ErrorMessage = "El campo Codigo Horario es obligatorio")]
        [MaxLength(7, ErrorMessage = "El campo Codigo Horario no puede tener más de 7 caracteres")]
        [MinLength(7, ErrorMessage = "El campo Codigo Horario debe tener al menos 7 caracteres")]
        public string CodHorario { get; set; }

        [Required(ErrorMessage = "El campo Nombre Horario es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Nombre Horario no puede tener más de 50 caracteres")]
        [MinLength(5, ErrorMessage = "El campo Nombre Horario debe tener al menos 5 caracteres")]
        public string cNombreHorario { get; set; }

            
        [Required(ErrorMessage = "El campo Descripcion Horario es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Descripcion Horario no puede tener más de 100 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Descripcion Horario debe tener al menos 10 caracteres")]
        public string cDescripcionHorario { get; set; }

        [Required(ErrorMessage = "El campo Descripcion Horario es obligatorio")]
        public string bActivo { get;  set; }

    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}