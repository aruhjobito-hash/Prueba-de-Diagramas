
// Developer: feragu 26/02/2025 - Controlador para parametros generelares

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Configuracion
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: ListaController 
    /// </summary>
    public class requestListaModel
    {
        //public int? Id { get; set; }
        
            
        public int IdLista { get; set; }

            
        [Required(ErrorMessage = "El campo cDescripcionLista es obligatorio")]
        //[MaxLength(150, ErrorMessage = "El campo cDescripcionLista no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo cDescripcionLista debe tener al menos 150 caracteres")]
        public string cDescripcionLista { get; set; }

            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_ListaController 
    /// </summary>
    [Keyless]
    public class ListaDBModel
    {
        //public int? Id { get; set; }
        
        public string Estado { get; set; }    
        public int IdLista { get; set; }

            
        [Required(ErrorMessage = "El campo cDescripcionLista es obligatorio")]
        //[MaxLength(150, ErrorMessage = "El campo cDescripcionLista no puede tener más de 150 caracteres")]
        //[MinLength(150, ErrorMessage = "El campo cDescripcionLista debe tener al menos 150 caracteres")]
        public string cDescripcionLista { get; set; }

            
        [Required(ErrorMessage = "El campo bEstado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEstado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEstado debe tener al menos 1 caracteres")]
        public string bEstado { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    public class requestListaDetalleModelCuali
    {
        public int IdListaDetalle { get; set; }
        public int IdLista { get; set; }
        public string CodDetalle { get; set; }
        public string cDescripcionDetalle { get; set; }
        public string? bEstado { get; set; }
        public string? cValor { get; set; }
        public string? iduser { get; set; }

    }

    [Keyless]
    public class ListaDetalleModelCuali
    {
        public string Estado { get; set; }
        public int IdListaDetalle { get; set; }
        public int IdLista { get; set; }
        public string CodDetalle { get; set; }
        public string cDescripcionDetalle { get; set; }
        public string? bEstado { get; set; }
        public string? cValor { get; set; }
        public string? iduser { get; set; }

    }

    public class requestListaDetalleModelCuanti
    {
        public int IdListaDetalle { get; set; }
        public int IdLista { get; set; }
        public string CodDetalle { get; set; }
        public string cDescripcionDetalle { get; set; }
        public string? bEstado { get; set; }
        public string? idagencia { get; set; }
        public string? tipmoneda { get; set; }
        public string? cValor1 { get; set; }
        public string? cValor2 { get; set; }
        public string? cValor3 { get; set; }
        public string? FecIni { get; set; }
        public string? FecFin { get; set; }
        public string? iduser { get; set; }

    }

    [Keyless]
    public class ListaDetalleModelCuanti
    {
        public string Estado { get; set; }
        public int IdListaDetalle { get; set; }
        public int IdLista { get; set; }
        public string CodDetalle { get; set; }
        public string cDescripcionDetalle { get; set; }
        public string? bEstado { get; set; }
        public string? idagencia { get; set; }
        public string? tipmoneda { get; set; }
        public string? cValor1 { get; set; }
        public string? cValor2 { get; set; }
        public string? cValor3 { get; set; }
        public DateTime? FecIni { get; set; }
        public DateTime? FecFin { get; set; }
        public string? iduser { get; set; }

    }
}