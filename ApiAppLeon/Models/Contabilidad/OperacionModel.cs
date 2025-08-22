
// Developer: feragu 27/03/2025 - Controlador para Operaciones de Contabilidad
// DateCreate   : 27/03/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Contabilidad
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: OperacionController 
    /// </summary>
    public class requestOperacionModel
    {

        //[Required(ErrorMessage = "El campo IdOpe es obligatorio")]
        //[MaxLength(7, ErrorMessage = "El campo IdOpe no puede tener más de 7 caracteres")]
        //[MinLength(7, ErrorMessage = "El campo IdOpe debe tener al menos 7 caracteres")]
        public string? IdOpe { get; set; }
        public string Operacion { get; set; }
        public string IdTipOpe { get; set; }
        //public string Activo { get; set; }
        public string? Mostrar { get; set; }
        public string? Obliga { get; set; }   
        [Required(ErrorMessage = "El campo IdUser es obligatorio")]
        public string IdUser { get; set; }
        public string? ProcesoInt { get; set; }
        //[Required(ErrorMessage = "El campo Fecpro es obligatorio")]
        //public DateTime Fecpro { get; set; }    
        //[Required(ErrorMessage = "El campo Hora es obligatorio")]
        //public string Hora { get; set; }
        public string? Elemento { get; set; }
        public int Nivel { get; set; }
        public string ? TipmonedaSol { get; set; }
        public string? TipmonedaDol { get; set; }
        public string? IdOpeP { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_OperacionController 
    /// </summary>
    [Keyless]
    public class OperacionDBModel
    {
        public string bEstado { get; set; }
        public string? IdOpe { get; set; }
        public string? IdOpeHijo { get; set; }
        public string? Operacion { get; set; }
        public string IdTipOpe { get; set; }
        public string Activo { get; set; }
        public string? Mostrar { get; set; }
        public string? Obliga { get; set; }
        public string? ProcesoInt { get; set; }

        public string? Elemento { get; set; }
        public string Nivel { get; set; }
        public string? TipmonedaSol { get; set; }
        public string? TipmonedaDol { get; set; }
    }
    [Keyless]
    public class TabTipOpeModel
    {
        public string IdTipOpe { get; set; }
        public string Descripcion { get; set; }
    }

    [Keyless]
    public class DocumentosModel
    {
        public string IdDoc { get; set; }
        public string? Documento { get; set; }
        public string? Nivel { get; set; }
        public string? Abrev { get; set; }
        public string? Parametro { get;set; }
        public string? CtrlInterno { get; set; }
    }
    [Keyless]
    public class TabTipCuentaModel
    {
        public string IdTipCta { get; set; }
        public string? Descripcion { get; set; }
    }
    [Keyless]
    public class requestAccionOpexTipCtaModel
    {
        public string? IdOpe { get; set; }
        public string? IdTipCta { get; set; }
        public string? Operador { get; set; }
    }
    public class AccionOpexTipCtaModel
    {
        public string bEstado { get; set; }
        public string? IdTipCta { get; set; }
        public string? Descripcion { get; set; }
        public string? Operador { get; set; }
    }
    public class requestDocxOpeModel
    {
        public string? IdOpe { get; set; }
        public string? IdDoc { get; set; }
        public string? Autogen { get; set; }
        public string? TipMoneda { get; set; }
        public string? Obliga { get; set; }
        public string? Activo { get; set; }
        public string? IdUser { get; set; }
    }
    public class DocxOpeModel
    {
        public string bEstado { get; set; }
        public string? IdDoc { get; set; }
        public string? Documento { get; set; }
        public string? Autogen { get; set; }
        public string? Obliga { get; set; }
        public string? Activo { get; set; }
        public string? TipMoneda { get; set; }
    }

    //[Keyless]
    //public class OperacionHijoDBModel
    //{
    //    public string bEstado { get; set; }
    //    public string IdOpe { get; set; }
    //    public string IdOpeHijo { get; set; }
    //    public string Operacion { get; set; }
    //    public string IdTipOpe { get; set; }
    //    public string Activo { get; set; }
    //    public string Mostrar { get; set; }
    //    public string Obliga { get; set; }
    //    public string ProcesoInt { get; set; }
    //    public string Elemento { get; set; }
    //    public string Nivel { get; set; }
    //}
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}