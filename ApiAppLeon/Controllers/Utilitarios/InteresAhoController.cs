using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class InteresAhoController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public InteresAhoController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<paGetTasaIntBD> intComp_ { get; set; } = new List<paGetTasaIntBD>();

        //public static Conexion.BDSiaf _BDSIAF { get; set; } = new BDSiaf();
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TasaIntAhoBD>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<TasaIntAhoBD>> IntCompPost(paGetTasaIntAhoBD paramCta)
        {
            if (paramCta == null)
            {
                return BadRequest();
            }

            //Store para consulta 
            string StorePersona = " exec pa_get_tasa_ahorro " +
                                    " @idtipcta ='" + paramCta.idtipcta + "'" +
                                    " ,@monto =" + paramCta.monto +
                                    " ,@TipMoneda ='" + paramCta.tipmoneda + "'"
                                    ;

            List<TasaIntAhoBD> IntComp;
            TasaIntAhoBD IntResp = new TasaIntAhoBD();
            IntComp = await _dbContext.TasaIntAhoBD.FromSqlRaw(StorePersona).ToListAsync();

            return Ok(IntComp);
        }
    }
}
