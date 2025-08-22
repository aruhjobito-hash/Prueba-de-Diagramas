using ApiAppLeon.Models.KasNet;
using ApiAppLeon.Models.Sistemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Controllers.KasNet
{
    [ApiExplorerSettings(GroupName = "Kasnet")]
    [Route("/api/kasnet/[controller]")]
    [ApiController]
    public class RegistraPagoController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public RegistraPagoController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private static List<RegistraPagoResponse> GetResponse_ { get; set; } = new List<RegistraPagoResponse>();
        private static List<conceptosAdicionalesPago> conceptosAdicionalesPagoRequest_ { get; set; } = new List<conceptosAdicionalesPago>();
        private static List<conceptosAdicionalesPago> conceptosAdicionalesPagoResponse_ { get; set; } = new List<conceptosAdicionalesPago>();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistraPagoRequest>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<RegistraPagoRequest>> PostRegistraDeudaRequest(RegistraPagoRequest pagoDeudaRequest)
        {
            GetResponse_.Clear();
            if (_dbContext.ConsultaDeudaRequest == null)
            {
                return NotFound();
            }

            //Store de historial y devolución de deuda
            decimal? Ic, Am, Au, Apo;
            conceptosAdicionalesPago conceptoAdicionalPagoRequest = new conceptosAdicionalesPago();
            conceptosAdicionalesPagoRequest_ = pagoDeudaRequest.conceptosAdicionales;
            foreach (conceptosAdicionalesPago cap in conceptosAdicionalesPagoRequest_)
            {
                if (cap.nombreConcepto == "INTERES")
                {
                    Ic = cap.montoConceptoConvertido;
                }
                if (cap.nombreConcepto == "APORTE")
                {
                    Apo = cap.montoConceptoConvertido;
                }
                if (cap.nombreConcepto == "DESGRAVAMEN")
                {
                    Au = cap.montoConceptoConvertido;
                }
                if (cap.nombreConcepto == "AMORTIZACION")
                {
                    Am = cap.montoConceptoConvertido;
                }
            }

            string StoreProc =
                        "exec sp_RegPagosKasNet" +
                        " ,@nrosumin=" + pagoDeudaRequest.nroSumin +
                        " ,@traceConsulta=" + pagoDeudaRequest.traceConsulta +
                        " ,@codEmpresa=" + pagoDeudaRequest.codEmpresa +
                        " ,@codServicio=" + pagoDeudaRequest.codServicio +
                        " ,@codAgencia=" + pagoDeudaRequest.codAgencia +
                        " ,@codCanal=" + pagoDeudaRequest.codCanal +
                        " ,@terminal=" + pagoDeudaRequest.terminal;

            List<RegistraPagoRequest> GetPagos_;
            GetPagos_ = await _dbContext.RegistraPagoRequest.FromSqlRaw(StoreProc).ToListAsync();
            RegistraPagoResponse GetResp_ = new RegistraPagoResponse();
            conceptosAdicionalesPago conceptosAdicionales__ = new conceptosAdicionalesPago();

            //Llenar Cabecera del response
            GetResp_.codigo = "00";
            GetResp_.mensaje = "Exitoso";
            GetResp_.nombreCliente = "José Huamán";
            GetResp_.nroSumin = pagoDeudaRequest.nroSumin;

            //Llenar Conceptos Adicionales de la deuda            
            conceptosAdicionales__.nombreConcepto = "INTERES";
            conceptosAdicionales__.montoConceptoConvertido = 20;
            conceptosAdicionalesPagoResponse_.Add(conceptosAdicionales__);
            conceptosAdicionales__.nombreConcepto = "APORTE";
            conceptosAdicionales__.montoConceptoConvertido = 10;
            conceptosAdicionalesPagoResponse_.Add(conceptosAdicionales__);
            conceptosAdicionales__.nombreConcepto = "AMORTIZACION";
            conceptosAdicionales__.montoConceptoConvertido = 320;
            conceptosAdicionalesPagoResponse_.Add(conceptosAdicionales__);
            conceptosAdicionales__.nombreConcepto = "DESGRAVAMEN";
            conceptosAdicionales__.montoConceptoConvertido = 320;
            //Llenar estructura de conceptos de la deuda 
            conceptosAdicionalesPagoResponse_.Add(conceptosAdicionales__);
            GetResp_.conceptosAdicionales = conceptosAdicionalesPagoResponse_;

            //Llenar estructura final del response

            //_dbContext.ConsultaDeudaReponse.Add(consultaDeudaRequest);
            //await _dbContext.SaveChangesAsync();
            //Devolver el response
            return CreatedAtAction(nameof(PostRegistraDeudaRequest), GetResp_);

            //return CreatedAtAction(nameof(PostConsultaDeudaRequest), new { id = consultaDeudaRequest.traceConsulta }, consultaDeudaRequest);
        }
    }
}
