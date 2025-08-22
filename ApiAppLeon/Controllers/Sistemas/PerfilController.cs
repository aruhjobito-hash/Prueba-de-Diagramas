
// Developer    : josara  
// DateCreate   : 02/05/2025
// Description  : Controlador para mantenedor de Perfiles
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Sistemas;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace ApiAppLeon.Controllers.Sistemas
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PerfilController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Perfil
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPerfilModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestPerfilModel>>> PostPerfil([FromBody] requestPerfilModel model)
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
                    "IdPerfil", "ActivePerfil", "fecpro", "hora", "iduser", "iduserR",
                    "idAgencia", "idcargo", "idarea", "TokenFijo", "opt"
                };

                // 2. Build the parameter array using reflection
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();

                // 3. Build the stored procedure call string
                string storePerfil = $"[Seguridad].[PerfilProcedure] {string.Join(", ", parameterNames.Select(n => "@" + n))}";

                // 4. Execute the stored procedure with the parameters
                List<PerfilDBModel> producto = await _dbContext.PerfilDB
                    .FromSqlRaw(storePerfil, parameters)
                    .ToListAsync();


                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PerfilDBModel>> { Exito = 1, Mensaje = "Success" ,Data = producto });
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

        // PUT: api/Perfil/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPerfilModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutPerfil(int id, [FromBody] requestPerfilModel model)
        {
            try
            {
                string storePerfil = "sp_Perfil";
                var updateResult = await _dbContext.PerfilDB.FromSqlRaw(storePerfil, model).ToListAsync();
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

        // GET: api/Perfil
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPerfilModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestPerfilModel>>> GetPerfilModel()
        {
            try
            {
                string storePerfil = "sp_Perfil";
                var result = await _dbContext.PerfilDB.FromSqlRaw(storePerfil).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/Perfil/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPerfilModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            try
            {
                string storePerfil = "sp_Perfil";
                var deleteResult = await _dbContext.PerfilDB.FromSqlRaw(storePerfil, id).ToListAsync();
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