using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.KasNet;
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos

namespace ApiAppLeon.Controllers.KasNet
{
    [ApiExplorerSettings(GroupName = "Kasnet")]
    [Route("/api/kasnet/[controller]")]
    [ApiController]
    public class ConsultaDeudaController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public ConsultaDeudaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private static List<ConsultaDeudaResponse> GetResponse_ { get; set; } = new List<ConsultaDeudaResponse>();
        private static List<lstdebt> lstdebts_ { get; set; } = new List<lstdebt>();
        private static List<conceptosAdicionalesDeuda> conceptosAdicionalesDeuda_ { get; set; } = new List<conceptosAdicionalesDeuda>();
        private static List<ConsultaDeuda> consultaDeuda_ { get; set; } = new List<ConsultaDeuda>();
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ConsultaDeudaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ConsultaDeudaRequest>> PostConsultaDeudaRequest(ConsultaDeudaRequest consultaDeudaRequest)
        {
            GetResponse_.Clear();
            if (_dbContext.ConsultaDeudaRequest == null)
            {
                return NotFound();
            }
            //Store de historial y devolución de deuda
            string StoreProc =
                        "exec sp_getPagosKasNet" +
                        " @nrosumin=" + consultaDeudaRequest.nroSumin +
                        " ,@traceConsulta=" + consultaDeudaRequest.traceConsulta +
                        " ,@fechaConsulta=" + consultaDeudaRequest.fechaConsulta +
                        " ,@horaConsulta=" + consultaDeudaRequest.horaConsulta +
                        " ,@codEmpresa=" + consultaDeudaRequest.codEmpresa +
                        " ,@codServicio=" + consultaDeudaRequest.codServicio +
                        " ,@codAgencia=" + consultaDeudaRequest.codAgencia +
                        " ,@codCanal=" + consultaDeudaRequest.codCanal +
                        " ,@terminal=" + consultaDeudaRequest.terminal
                    ;
            List<ConsultaDeuda> GetPagos_;
            GetPagos_ = await _dbContext.ConsultaDeuda.FromSqlRaw(StoreProc).ToListAsync();
#pragma warning disable IDE0090 // Usar "new(...)"
            ConsultaDeudaResponse GetResp_ = new ConsultaDeudaResponse();
#pragma warning restore IDE0090 // Usar "new(...)"
            lstdebt lstdebt__ = new lstdebt();
            conceptosAdicionalesDeuda conceptosAdicionales__ = new conceptosAdicionalesDeuda();
            int band = 0;
            foreach (var item in GetPagos_) { if (item.Am > 0) { band++; } else { band = 0; } }
            if (band != 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in GetPagos_)
                {
                    if (item.orden == "1")
                    {
                        //Llenar Cabecera del response
                        GetResp_.codigo = "00";
                        GetResp_.mensaje = "Exitoso";
                        GetResp_.nombreCliente = "José Huamán";
                        GetResp_.nroSumin = consultaDeudaRequest.nroSumin;
                        GetResp_.ordenPrelacion = "Indistinto";
                    }
                    // Llenar Detalle del response por cada registro
                    lstdebt__.numfactura = item.numfactura;
                    lstdebt__.tipoMoneda = item.tipoMoneda;
                    lstdebt__.montoTipoCambio = item.montoTipoCambio;
                    lstdebt__.montoDeudaConvertido = item.montoDeudaConvertido;
                    lstdebt__.comision = item.comision;
                    lstdebt__.ComisionConvertido = item.ComisionConvertido;
                    // Llenar Conceptos Adicionales de la deuda por concepto
                    if (item.Am > 0)
                    {
                        conceptosAdicionales__.nombreConcepto = "AMORTIZACION";
                        conceptosAdicionales__.montoConcepto = item.Am;
                        conceptosAdicionales__.montoConceptoConvertido = item.Am;
                        conceptosAdicionalesDeuda_.Add(conceptosAdicionales__);
                    }
                    if (item.Interes > 0)
                    {
                        conceptosAdicionales__.nombreConcepto = "INTERES";
                        conceptosAdicionales__.montoConcepto = item.Interes;
                        conceptosAdicionales__.montoConceptoConvertido = item.Interes;
                        conceptosAdicionalesDeuda_.Add(conceptosAdicionales__);
                    }
                    if (item.Au > 0)
                    {
                        conceptosAdicionales__.nombreConcepto = "DESGRAVAMEN";
                        conceptosAdicionales__.montoConcepto = item.Au;
                        conceptosAdicionales__.montoConceptoConvertido = item.Au;
                        conceptosAdicionalesDeuda_.Add(conceptosAdicionales__);
                    }
                    if (item.Apo > 0)
                    {
                        conceptosAdicionales__.nombreConcepto = "APORTE";
                        conceptosAdicionales__.montoConcepto = item.Apo;
                        conceptosAdicionales__.montoConceptoConvertido = item.Apo;
                        conceptosAdicionalesDeuda_.Add(conceptosAdicionales__);
                    }
                    // Llenar estructura de conceptos de la deuda 
                    lstdebt__.conceptosAdicionales = conceptosAdicionalesDeuda_;
                    // Termina de llenar conceptos adicionales
                    lstdebt__.montoTotalConcepto = item.montoTotalConcepto;
                    lstdebt__.montoTotalConceptoConv = item.montoTotalConceptoConv;
                    lstdebt__.montoTotal = item.montoTotal;
                    lstdebt__.montoTotalConvertido = item.montoTotalConvertido;
                    lstdebt__.formaPago = item.formaPago;
                    lstdebt__.montoMinimo = item.montoMinimo;
                    lstdebt__.montoMinimoConvertido = item.montoMinimoConvertido;
                    lstdebt__.montoMaximo = item.montoMaximo;
                    lstdebt__.montoMaximoConvertido = item.montoMaximoConvertido;
                    lstdebt__.fechaEmision = item.fechaEmision;
                    lstdebt__.fechaVencimiento = item.fechaVencimiento;
                    lstdebt__.tipoIntegracion = item.tipoIntegracion;
                    lstdebt__.glosa = item.glosa;
                    lstdebt__.orden = item.orden;
                    lstdebts_.Add(lstdebt__);
                }
                //Llenar estructura final de la deuda a la cabecera

                GetResp_.lstdebt = lstdebts_;
                //Llenar estructura final del response
                GetResponse_.Add(GetResp_);
                //_dbContext.ConsultaDeudaReponse.Add(consultaDeudaRequest);
                //await _dbContext.SaveChangesAsync();
                //Devolver el response
                return CreatedAtAction(nameof(PostConsultaDeudaRequest), GetResp_);

                //return CreatedAtAction(nameof(PostConsultaDeudaRequest), new { id = consultaDeudaRequest.traceConsulta }, consultaDeudaRequest);
            }
        }
    }
}

