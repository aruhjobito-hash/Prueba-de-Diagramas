using ApiAppLeon.Models.Operaciones;
using ApiAppLeon.Models.Sistemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Controllers.Operaciones
{
    [ApiExplorerSettings(GroupName = "Operaciones")]
    [Route("api/Operaciones/[controller]")]
    [ApiController]
    public class TransferenciaAhorrosCreditoController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public TransferenciaAhorrosCreditoController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public static Conexion.BDSiaf _BDSIAF { get; set; } = new BDSiaf();
        private static List<TransferenciaAhorroCreditoBD> TransferenciaAhorrosCredito_ { get; set; } = new List<TransferenciaAhorroCreditoBD>();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TransferenciaAhorroCreditoBD>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<TransferenciaAhorroCreditoBD>> Post(TransferenciaAhorrosCreditoRequest transferenciaModel)
        {
            await Task.Delay(1000);
            TransferenciaAhorrosCredito_.Clear();
            if (_dbContext.PersonaResponse == null)
            {
                return NotFound();
            }

            //Store para consulta
            string StorePersona =
                " exec pa_EjecutaPagoApp " +
                " @iddoc= '" + transferenciaModel.iddoc + "'," +
                " @nrodoc= '" + transferenciaModel.nrodoc + "'," +
                " @idagencia= '" + transferenciaModel.idagencia + "'," +
                " @tipmoneda= '" + transferenciaModel.tipmoneda + "'," +
                " @montoTotal= " + transferenciaModel.montoTotal + "," +
                " @amort= " + transferenciaModel.amort + "," +
                " @IntComp= " + transferenciaModel.intComp + "," +
                " @intVenc= " + transferenciaModel.intVenc + "," +
                " @intMor= " + transferenciaModel.intMor + "," +
                " @NroCuota= '" + transferenciaModel.Nrocuota + "'," +
                " @Desgra= " + transferenciaModel.desgra + "," +
                " @FechaVen= '" + transferenciaModel.fechaven + "'," +
                " @IdTipCtaaho= '" + transferenciaModel.idTipCtaAho + "'," +
                " @NumCuentaaho= '" + transferenciaModel.numCuentaAho + "'," +
                " @idagenciaCtaAho= '" + transferenciaModel.idagenciaCtaAho + "'," +
                " @tipmonedactaAho= '" + transferenciaModel.tipMonedaCtaAho + "'"

                ;

            //List<PersonaResponse> personaResponse_;
            List<TransferenciaAhorroCreditoBD> TransferenciaAhorroxCredito = new List<TransferenciaAhorroCreditoBD>();
            TransferenciaAhorroxCredito = await _dbContext.TransferenciaAhorrosCreditoBDs.FromSqlRaw(StorePersona).ToListAsync();
            int band = 0;
            foreach (var item in TransferenciaAhorroxCredito)
            {
                if (item.codigo == "00")
                {
                    band = 1;
                }
                else if (item.codigo == "01")
                {
                    band = 2;
                }
                else
                {
                    band = 0;
                }
            }

            if (band == 1)
            {
                return CreatedAtAction(nameof(Post), TransferenciaAhorroxCredito);
            }
            else if (band == 2)
            {
                return BadRequest(TransferenciaAhorroxCredito);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
