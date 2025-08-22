
// Developer: migzav 19/03/2025 - Controlador obtener Productos Negocios

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Negocios
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: ProductosNegocios 
    /// </summary>
    public class requestProductosNegociosModel
    {
        public int? Id { get; set; }
        
            
        [Required(ErrorMessage = "El campo IdDenoCre es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdDenoCre no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdDenoCre debe tener al menos 4 caracteres")]
        public string IdDenoCre { get; set; }

            
        [Required(ErrorMessage = "El campo Denominacion es obligatorio")]
        [MaxLength(60, ErrorMessage = "El campo Denominacion no puede tener más de 60 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Denominacion debe tener al menos 60 caracteres")]
        public string Denominacion { get; set; }

            
        public string FecVig { get; set; }

            
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo IdUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo IdUser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }

            
        public decimal MontoMin { get; set; }

            
        public decimal MontoMax { get; set; }

            
        [Required(ErrorMessage = "El campo Abrev es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo Abrev no puede tener más de 20 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Abrev debe tener al menos 20 caracteres")]
        public string Abrev { get; set; }

            
        public int DiasPlazoPC { get; set; }

            
        [Required(ErrorMessage = "El campo DxP es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo DxP no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo DxP debe tener al menos 1 caracteres")]
        public string DxP { get; set; }

            
        [Required(ErrorMessage = "El campo sinGara es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo sinGara no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo sinGara debe tener al menos 1 caracteres")]
        public string sinGara { get; set; }

            
        public int PerGracia { get; set; }

        public decimal PorcAmplia { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo AprobAuto solo puede tener el valor '0' o '1'.")]
        public string AprobAuto { get; set; }

        [StringLength(1)]
        [RegularExpression("^[01]{1}$", ErrorMessage = "El campo Activo solo puede tener el valor '0' o '1'.")]
        public string Activo { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_ProductosNegocios 
    /// </summary>
    [Keyless]
    public class ProductosNegociosDBModel
    {
        
        public string IdDenoCre { get; set; }

        public string Denominacion { get; set; }
   
        public string FecVig { get; set; }

        public string IdUser { get; set; }
    
        public decimal MontoMin { get; set; }

        public decimal MontoMax { get; set; }

        public string Abrev { get; set; }

        public int DiasPlazoPC { get; set; }

        public string DxP { get; set; }

        public string sinGara { get; set; }
  
        public int PerGracia { get; set; }

        public decimal PorcAmplia { get; set; }

        public string AprobAuto { get; set; }

        public string Activo { get; set; }
    }

    public class ResultadoIdDenoCre
    {
        public string IdDenoCre { get; set; }
    }



    public class ListaDestinosDBModel
    {

        public int IdListaDetalle { get; set; }

        public int IdLista { get; set; }

        public string CodDetalle { get; set; }

        public string cDescripcionDetalle { get; set; }

        public int Seleccionado { get; set; }


    }

    public class RegistrarDestinosRequest
    {
        public string IdDenoCre { get; set; } = string.Empty;
        public List<ListaDestinos> ListaDestinos { get; set; }
    }

    public class ListaDestinos
    {
        public int IdLista { get; set; }
        public int IdListaDetalle { get; set; }

        public int Seleccionado { get; set; }
    }


    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}