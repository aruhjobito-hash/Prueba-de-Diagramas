using ApiAppLeon.Models.KasNet;
using ApiAppLeon.Models.Sistemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Controllers.KasNet
{
    [ApiExplorerSettings(GroupName = "Kasnet")]
    [Route("/api/kasnet/[controller]")]
    [ApiController]
    public class ExtornoPagoController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public ExtornoPagoController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private static List<RegistraPagoResponse> GetResponse_ { get; set; } = new List<RegistraPagoResponse>();
        private static List<conceptosAdicionalesExtorno> conceptosAdicionalesExtornoRequest_ { get; set; } = new List<conceptosAdicionalesExtorno>();
        private static List<conceptosAdicionalesExtorno> conceptosAdicionalesExtornoResponse_ { get; set; } = new List<conceptosAdicionalesExtorno>();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ExtornaPagoRequest>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ExtornaPagoRequest>> PostRegistraDeudaRequest(ExtornaPagoRequest pagoDeudaRequest)
        {
            GetResponse_.Clear();
            if (_dbContext.ConsultaDeudaRequest == null)
            {
                return NotFound();
            }

            //Store de historial y devolución de deuda
            decimal? Ic, Am, Au, Apo;
            conceptosAdicionalesExtorno conceptoAdicionalPagoRequest = new conceptosAdicionalesExtorno();
            conceptosAdicionalesExtornoRequest_ = pagoDeudaRequest.conceptosAdicionales;
            foreach (conceptosAdicionalesExtorno cap in conceptosAdicionalesExtornoRequest_)
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
                        "exec sp_RegExtornoKasNet" +
                        " @id=" + 1 +
                        " ,@nrosumin=" + pagoDeudaRequest.nroSumin +
                        " ,@traceConsulta=" + pagoDeudaRequest.traceConsulta +
                        //" ,@fechaConsulta=" + consultaDeudaRequest.fechaConsulta +
                        //" ,@horaConsulta=" + consultaDeudaRequest.horaConsulta +
                        " ,@codEmpresa=" + pagoDeudaRequest.codEmpresa +
                        " ,@codServicio=" + pagoDeudaRequest.codServicio +
                        " ,@codAgencia=" + pagoDeudaRequest.codAgencia +
                        " ,@codCanal=" + pagoDeudaRequest.codCanal +
                        " ,@terminal=" + pagoDeudaRequest.terminal;

            List<ExtornaPagoRequest> GetPagos_;
            GetPagos_ = await _dbContext.RegistraExtornoRequest.FromSqlRaw(StoreProc).ToListAsync();
            ExtornaPagoResponse GetResp_ = new ExtornaPagoResponse();
            conceptosAdicionalesExtorno conceptosAdicionales__ = new conceptosAdicionalesExtorno();

            //Devolver el response
            return CreatedAtAction(nameof(PostRegistraDeudaRequest), GetResp_);

            //return CreatedAtAction(nameof(PostConsultaDeudaRequest), new { id = consultaDeudaRequest.traceConsulta }, consultaDeudaRequest);
        }
    }
}
