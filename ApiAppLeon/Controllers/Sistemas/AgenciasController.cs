
// Developer    : JosAra  
// DateCreate   : 09/05/2025
// Description  : Controlador para información de las agencias
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Sistemas;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Sistemas.Controllers
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public AgenciasController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Agencias
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAgenciasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestAgenciasModel>>> PostAgencias([FromBody] requestAgenciasModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {

                // 1. Define the property names (in order of the stored procedure parameters)
                var parameterNames = new[]
                {
                    "IdAgencia", "Agencia", "Fechape", "Direccion", "Fecpro", "IdUser",
                    "Hora", "Abrev", "LugVot", "IdUbigeo", "MaximoPoliza","ApiToken","ApiTokenVirtual","ApiTokenConvenio","IdRiesgo","opt"
                };

                // 2. Build the parameter array using reflection
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();

                // 3. Build the stored procedure call string
                string storeAgencias = $"[Seguridad].[Agencias] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<AgenciasDBModel> producto = await _dbContext.MantAgencias.FromSqlRaw(storeAgencias, model).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<AgenciasDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // PUT: api/Agencias/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAgenciasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutAgencias(int id, [FromBody] requestAgenciasModel model)
        {
            try
            {
                string storeAgencias = "sp_Agencias";
                var updateResult = await _dbContext.MantAgencias.FromSqlRaw(storeAgencias, model).ToListAsync();
                if (updateResult.Count == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/Agencias
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AgenciasDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<AgenciasDBModel>>> GetAgenciasModel()
        {
            
            try
            {
                var parameterNames = new[]
                {
                   "opt"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, 5)).ToArray();
                string storeAgencias = $"[Sistemas].[Agencias] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                var result = await _dbContext.MantAgencias.FromSqlRaw(storeAgencias,parameters).AsNoTracking().ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/Agencias/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAgenciasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteAgencias(int id)
        {
            try
            {                
                string storeAgencias = "sp_Agencias";
                var deleteResult = await _dbContext.MantAgencias.FromSqlRaw(storeAgencias, id).ToListAsync();
                if (deleteResult.Count == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

    }
}