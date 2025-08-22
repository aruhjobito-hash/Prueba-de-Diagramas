using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ApiAppLeon.Models.Utilitarios
{
    public class ListarProductosModel
    {

    }
    [Keyless]
    public class ListarProductosBD
    {
        public string? ordenIdDenoCre { get; set; }
        public string? iddenocre { get; set; }
        public string? NombreProducto { get; set; }
        public int? plazomax { get; set; }
        public decimal montomax { get; set; }
        public string? ordenIdDestino { get; set; }
        public string? Destino { get; set; }
        public string? IdDestino { get; set; }
        public string? TipoCreditoSBS { get; set; }
        public string? idTipCreditoSBS { get; set; }
    }
    public class ListarProductoDet
    {
        public string? iddenocre { get; set; }
        public string? NombreProducto { get; set; }
        public int? plazomax { get; set; }
        public decimal montomax { get; set; }
        public List<DestinoProducto> DestinoProductos { get; set; }
    }
    public class DestinoProducto
    {
        public string? Destino { get; set; }
        public string? IdDestino { get; set; }
        public List<TipoCreSBS> tipoCreSBs { get; set; }
    }
    public class TipoCreSBS
    {
        public string? TipoCreditoSBS { get; set; }
        public string? idTipCreditoSBS { get; set; }
    }
    public class paGetTasaIntBD
    {
        //public string? TCredCAC {get;set;}
        public string? DenoCred { get; set; }
        //public string? FrecPago {get;set;}
        //public string? TCuota {get;set;}
        public decimal? Plazo { get; set; }
        //public string? TipMoneda {get;set;}
        public decimal? Monto { get; set; }
        public string? Categoria { get; set; }
        //public string? Recurrente {get;set;}

    }
    public class paGetTasaIntAhoBD
    {
        public string? idtipcta { get; set; }
        public decimal? monto { get; set; }
        public string? tipmoneda { get; set; }

    }
    public class TasaIntAhoBD
    {
        public decimal? TasaVigente { get; set; }
        public decimal? MontoMin { get; set; }
    }

    public class paListarConveniosBD
    {
        public string? iddenocre { get; set; }
        public string? NombreProducto { get; set; }
        public int? plazomax { get; set; }
        public int? plazoMin { get; set; }
        public decimal? montoMin { get; set; }
        public decimal? montomax { get; set; }
        public decimal? tasa { get; set; }

    }
    public class InteresComp
    {
        public decimal? Tasa { get; set; }
        public decimal? TasaPromo { get; set; }
    }
    public class InteresAhoRequest
    {

    }
    public class InteresAho
    {

    }

}
