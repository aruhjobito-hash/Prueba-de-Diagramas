using ApiAppLeon.Models;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using static ApiAppLeon.Models.DBClassModel;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class ConsultaPersonaController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public ConsultaPersonaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<ConsultaPersona> Persona_ { get; set; } = new List<ConsultaPersona>();
        public static List<ConsultaPersonaResponse> personaResponse_ { get; set; } = new List<ConsultaPersonaResponse>();
        public static List<Agencias> Agencia_ { get; set; } = new List<Agencias>();

        //public static Conexion.BDSiaf _BDSIAF { get; set; } = new BDSiaf();
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<Agencias>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        private async Task<ActionResult<Agencias>> Agencias()
        {
            if (_dbContext.PersonaResponse == null)
            {
                return NotFound();
            }
            //Store para consulta
            string StorePersona = "select idagencia from agencias";

            List<Agencias> Agencia_;

            Agencia_ = await _dbContext.ConsultaAgencia.FromSqlRaw(StorePersona).ToListAsync();

            return CreatedAtAction(nameof(Agencias), Agencia_);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ConsultaPersonaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ConsultaPersonaRes>> PostPersona([FromBody] ConsultaPersona personaModel)
        {
            //Persona_.Clear();
            try
            {
                if (_dbContext.PersonaResponse == null)
                {
                    return NotFound();
                }
                //Store para consulta
                string StorePersona = "exec consulta_Persona_DNI " +
                                        " @DNI='" + personaModel.DNI + "'";
                List<ConsultaPersonaRes> person = new List<ConsultaPersonaRes>();
                person = await _dbContext.PersonaResponse.FromSqlRaw(StorePersona).ToListAsync();
                int band = 0;
                foreach (var item in person)
                {
                    if (item.IdPersona != "")
                    {
                        band++;
                    }
                    else
                    {
                        band = 0;
                    }
                }
                if (band != 0)
                {
                    return CreatedAtAction(nameof(PostPersona), person);
                }
                else
                {
                    return NotFound(person);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Data + ex.Message);
            }
        }
    }
}
