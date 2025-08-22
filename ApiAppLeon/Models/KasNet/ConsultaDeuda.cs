//  Author              :   JosAra
//  Date                :   10/07/2024
//  Description         :   Modelo de json para Request y Response de Consulta de Deuda
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiAppLeon.Models.KasNet
{
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
    [Keyless]
    public class ConsultaDeudaRequest
    {
        //[Required]

        //public Int32? id { get; set; } 
        public string? nroSumin { get; set; }
        public string? traceConsulta { get; set; }
        public string? fechaConsulta { get; set; }
        public string? horaConsulta { get; set; }
        public string? codEmpresa { get; set; }
        public string? codServicio { get; set; }
        public string? codAgencia { get; set; }
        public string? codCanal { get; set; }
        public string? terminal { get; set; }
    }
    [Keyless]
    public class ConsultaDeudaResponse
    {
        //public Int32? id { get; set; } 
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
        public string? nombreCliente { get; set; }
        public string? nroSumin { get; set; }
        public string? ordenPrelacion { get; set; }
        public List<lstdebt>? lstdebt { get; set; }

    }
    [Keyless]
    public class lstdebt
    {
        //public Int32? id { get; set; }
        public string? numfactura { get; set; }
        public string? tipoMoneda { get; set; }
        public decimal? montoTipoCambio { get; set; }
        public decimal? montoDeudaConvertido { get; set; }
        public decimal? comision { get; set; }
        public decimal? ComisionConvertido { get; set; }
        public List<conceptosAdicionalesDeuda>? conceptosAdicionales { get; set; }
        public decimal? montoTotalConcepto { get; set; }
        public decimal? montoTotalConceptoConv { get; set; }
        public decimal? montoTotal { get; set; }
        public decimal? montoTotalConvertido { get; set; }
        public string? formaPago { get; set; }
        public decimal? montoMinimo { get; set; }
        public decimal? montoMinimoConvertido { get; set; }
        public decimal? montoMaximo { get; set; }
        public decimal? montoMaximoConvertido { get; set; }
        public string? fechaEmision { get; set; }
        public string? fechaVencimiento { get; set; }
        public string? tipoIntegracion { get; set; }
        public string? glosa { get; set; }
        public string? orden { get; set; }
    }
    /// <summary>
    /// <c>Esta clase contiene la variable y declaración .Keyless necesarios en DBContext.cs para almacenar los objetos enviados a la base de datos y los obtenidos de la misma
    /// Variable: establece la virtualización del modelo que almacenará los datos provenientes de la base de datos 
    /// Funcion: De no tener un id (int) la consulta devuelta deberá incluirse en la función lineas abajo para indicar que la consulta será almacenada sin id en el modelo (objeto)</c>
    /// </summary>
    [Keyless]
    public class conceptosAdicionalesDeuda //Maximo 5 Conceptos adicionales
    {
        //public Int32? id { get; set; }
        public string? nombreConcepto { get; set; }
        public decimal? montoConcepto { get; set; }
        public decimal? montoConceptoConvertido { get; set; }
    }
    [Keyless]
    public class ConsultaDeuda
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
        public string? nombreCliente { get; set; }
        public string? nroSumin { get; set; }
        public string? ordenPrelacion { get; set; }
        public string? numfactura { get; set; }
        public string? tipoMoneda { get; set; }
        public decimal? montoTipoCambio { get; set; }
        public decimal? montoDeuda { get; set; }
        public decimal? montoDeudaConvertido { get; set; }
        public decimal? comision { get; set; }
        public decimal? ComisionConvertido { get; set; }
        public decimal? Interes { get; set; }
        public decimal? Ic { get; set; }
        public decimal? Im { get; set; }
        public decimal? Iv { get; set; }
        public decimal? Am { get; set; }
        public decimal? Au { get; set; }
        public decimal? Apo { get; set; }
        public decimal? montoTotalConcepto { get; set; }
        public decimal? montoTotalConceptoConv { get; set; }
        public decimal? montoTotal { get; set; }
        public decimal? montoTotalConvertido { get; set; }
        public string? formaPago { get; set; }
        public decimal? montoMinimo { get; set; }
        public decimal? montoMinimoConvertido { get; set; }
        public decimal? montoMaximo { get; set; }
        public decimal? montoMaximoConvertido { get; set; }
        public string? fechaEmision { get; set; }
        public string? fechaVencimiento { get; set; }
        public string? tipoIntegracion { get; set; }
        public string? glosa { get; set; }
        public string? orden { get; set; }

    }
}
