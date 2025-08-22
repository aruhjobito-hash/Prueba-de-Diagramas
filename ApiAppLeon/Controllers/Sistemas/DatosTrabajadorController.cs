
// Developer    : JosAra  
// DateCreate   : 08/05/2025
// Description  : Controlador para información personal del trabajador
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace ApiAppLeon.Controllers.Sistemas
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class DatosTrabajadorController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public DatosTrabajadorController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/DatosTrabajador
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestDatosTrabajadorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestDatosTrabajadorModel>>> PostDatosTrabajador([FromBody] requestDatosTrabajadorModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                var parameterNames = new[]
                {
                   "IdUser"
                };

                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeDatosTrabajador = $"[Seguridad].[sp_DatosTrabajador] {string.Join(", ", parameterNames.Select(n => "@" + n))}";                
                List<DatosTrabajadorDBModel> producto = await _dbContext.DatosTrabajadorDB.FromSqlRaw(storeDatosTrabajador, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(producto);
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

        // PUT: api/DatosTrabajador/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestDatosTrabajadorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutDatosTrabajador(int id, [FromBody] requestDatosTrabajadorModel model)
        {
            try
            {
                string storeDatosTrabajador = "sp_DatosTrabajador";
                var updateResult = await _dbContext.DatosTrabajadorDB.FromSqlRaw(storeDatosTrabajador, model).ToListAsync();
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

        // GET: api/DatosTrabajador
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestDatosTrabajadorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestDatosTrabajadorModel>>> GetDatosTrabajadorModel()
        {
            try
            {
                string storeDatosTrabajador = "sp_DatosTrabajador";
                var result = await _dbContext.DatosTrabajadorDB.FromSqlRaw(storeDatosTrabajador).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/DatosTrabajador/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestDatosTrabajadorModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteDatosTrabajador(int id)
        {
            try
            {
                string storeDatosTrabajador = "sp_DatosTrabajador";
                var deleteResult = await _dbContext.DatosTrabajadorDB.FromSqlRaw(storeDatosTrabajador, id).ToListAsync();
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