
// Developer    : migzav  
// DateCreate   : 27/02/2025
// Description  : Controlador obtener valor límite de una operación financiera Lavado de Activos
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Operaciones;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Operaciones.Controllers
{
    [ApiExplorerSettings(GroupName = "Operaciones")]
    [Route("api/Operaciones/[controller]")]
    [ApiController]
    public class ValorLavadoActivosController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public ValorLavadoActivosController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/ValorLavadoActivos
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestValorLavadoActivosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        private async Task<ActionResult<Respuesta<requestValorLavadoActivosModel>>> PostValorLavadoActivos([FromBody] requestValorLavadoActivosModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string storeValorLavadoActivos = "sp_ValorLavadoActivos";
                List<ValorLavadoActivosDBModel> producto = await _dbContext.ValorLavadoActivosDB.FromSqlRaw(storeValorLavadoActivos, model).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<ValorLavadoActivosDBModel>{ Exito = 1, Mensaje = "Success" });
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

        // PUT: api/ValorLavadoActivos/{id}        
        //[HttpPut("ValorLavadoActivos/{X-profile-Token}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestValorLavadoActivosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        //public async Task<IActionResult> PutValorLavadoActivos(int id, [FromBody] requestValorLavadoActivosModel model)
        //{
        //    if (id != model.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        string storeValorLavadoActivos = "sp_ValorLavadoActivos";
        //        var updateResult = await _dbContext.ValorLavadoActivosDB.FromSqlRaw(storeValorLavadoActivos, model).ToListAsync();
        //        if (updateResult.Count == 0)
        //        {
        //            return NotFound();
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
        //    }
        //}
        public async Task<IActionResult> PutValorLavadoActivos([FromBody] requestValorLavadoActivosModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC sp_ValorLavadoActivos @TipoOperacion, @Valor";
                await _dbContext.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@TipoOperacion", 2),
                    new SqlParameter("@Valor", model.Valor)
                );

                return Ok(new Respuesta<ValorLavadoActivosDBModel> { Exito = 1, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                //return StatusCode(500, new { Exito = 0, Mensaje = ex.Message });
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/ValorLavadoActivos
        //[HttpGet("ValorLavadoActivos/{X-profile-Token}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestValorLavadoActivosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        //public async Task<ActionResult<IEnumerable<requestValorLavadoActivosModel>>> GetValorLavadoActivosModel()
        //{
        //    try
        //    {
        //        string storeValorLavadoActivos = "sp_ValorLavadoActivos";
        //        var result = await _dbContext.ValorLavadoActivosDB.FromSqlRaw(storeValorLavadoActivos).ToListAsync();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
        //    }
        //}

        public async Task<ActionResult<IEnumerable<ValorLavadoActivosDBModel>>> GetValorLavadoActivosModel()
        {
            try
            {
                string query = "EXEC sp_ValorLavadoActivos @TipoOperacion";

                List<ValorLavadoActivosDBModel> result = await _dbContext.ValorLavadoActivosDB
                    .FromSqlRaw(query,
                        new SqlParameter("@TipoOperacion", 1)
                    )
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No data found",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No records found for the given date" }
                    });
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }


        // DELETE: api/ValorLavadoActivos/{id}
        [HttpDelete("ValorLavadoActivos/{X-profile-Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestValorLavadoActivosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        private async Task<IActionResult> DeleteValorLavadoActivos(int id)
        {
            try
            {                
                string storeValorLavadoActivos = "sp_ValorLavadoActivos";
                var deleteResult = await _dbContext.ValorLavadoActivosDB.FromSqlRaw(storeValorLavadoActivos, id).ToListAsync();
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