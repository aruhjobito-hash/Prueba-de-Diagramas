//  Author              :   JosAra
//  Date                :   10/07/2024
//  Description         :   Modelo de json para Request y Response de Consulta de Deuda
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.KasNet
{
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
#pragma warning disable CS8618 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
    [Keyless]
    public class RegistraPagoRequest
    {
        public string? nroSumin { get; set; }
        public string? numFactura { get; set; }
        public string? tracePago { get; set; }
        public string? traceConsulta { get; set; }
        public string? fechaPago { get; set; }
        public string? horaPago { get; set; }
        public decimal? montoDeudaConvertido { get; set; }
        public decimal? comisionConvertido { get; set; }
        public List<conceptosAdicionalesPago> conceptosAdicionales { get; set; }
        public decimal? montoTotalConceptoConv { get; set; }
        public decimal? montoTotalConvertido { get; set; }
        public string? codEmpresa { get; set; }
        public string? codServicio { get; set; }
        public string? codAgencia { get; set; }
        public string? codCanal { get; set; }
        public string? terminal { get; set; }
    }
    [Keyless]
    public class conceptosAdicionalesPago
    {
        public string? nombreConcepto { get; set; }
        public decimal? montoConceptoConvertido { get; set; }
    }
    [Keyless]
    public class RegistraPagoResponse
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
        public string? nombreCliente { get; set; }
        public string? nroSumin { get; set; }
        public decimal? montoDeudaConvertido { get; set; }
        public decimal? comisionConvertido { get; set; }
        public List<conceptosAdicionalesPago>? conceptosAdicionales { get; set; }
        public decimal? montoTotalConceptoConv { get; set; }
        public decimal? montoTotalConvertido { get; set; }
        public int? numOperacionEmpresa { get; set; }
        public string? glosa { get; set; }
    }
}

