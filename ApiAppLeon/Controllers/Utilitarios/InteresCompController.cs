using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class InteresCompController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public InteresCompController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<paGetTasaIntBD> intComp_ { get; set; } = new List<paGetTasaIntBD>();

        //public static Conexion.BDSiaf _BDSIAF { get; set; } = new BDSiaf();
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<InteresComp>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<InteresComp>> IntCompPost(paGetTasaIntBD paramIntComp)
        {
            try
            {
                if (_dbContext.PersonaResponse == null)
                {
                    return NotFound();
                }

                string StorePersona = " exec paGetTasaIntCompNv1 " +
                                        " @TCredCAC ='1'" +
                                        " ,@DenoCred ='" + paramIntComp.DenoCred + "'" +
                                        " ,@FrecPago ='1601'" +
                                        " ,@TCuota ='2'" +
                                        " ,@Plazo =" + paramIntComp.Plazo +
                                        " ,@TipMoneda ='0'" +
                                        " ,@Monto =" + paramIntComp.Monto +
                                        " ,@Categoria ='" + paramIntComp.Categoria + "'" +
                                        " ,@Recurrente ='X'"
                                        ;

                List<InteresComp> IntComp;
                InteresComp IntResp = new InteresComp();
                IntComp = await _dbContext.PaGetTasaIntBD.FromSqlRaw(StorePersona).ToListAsync();

                foreach (var i in IntComp)
                {
                    IntResp.Tasa = i.Tasa;
                    if (i.TasaPromo == null)
                    {
                        IntResp.TasaPromo = i.TasaPromo;
                    }
                    else
                    {
                        IntResp.TasaPromo = i.TasaPromo;
                    }
                }

                return CreatedAtAction(nameof(IntCompPost), IntResp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Data + ex.Message);
            }
        }
    }
}
