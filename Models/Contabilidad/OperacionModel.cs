using System.ComponentModel.DataAnnotations;

namespace LeonXIIICore.Models.Contabilidad
{
    public class requestOperacionModel
    {
        public string? IdOpe { get; set; }
        public string Operacion { get; set; }
        public string IdTipOpe { get; set; }
        public string? Mostrar { get; set; }
        public string? Obliga { get; set; }
        public string IdUser { get; set; }
        public string? ProcesoInt { get; set; }
        public string? Elemento { get; set; }
        public int Nivel { get; set; }
        public string? TipmonedaSol { get; set; }
        public string? TipmonedaDol { get; set; }
        public string? IdOpeP { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_OperacionController 
    /// </summary>

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
        // Nueva propiedad para rastrear si está seleccionado
        public bool IsSelected { get; set; }
        public bool IsSelected2 { get; set; }
    }
    public class TipOpeDBModel
    {
        public string IdTipOpe { get; set; }
        public string Descripcion { get; set; }
    }
    public class DocumentosModel
    {
        public string IdDoc { get; set; }
        public string? Documento { get; set; }
        public string? Nivel { get; set; }
        public string? Abrev { get; set; }
        public string? Parametro { get; set; }
        public string? CtrlInterno { get; set; }
    }
    public class TabTipCuentaModel
    {
        public string IdTipCta { get; set; }
        public string? Descripcion { get; set; }
    }
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
}
