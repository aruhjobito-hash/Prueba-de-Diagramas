
// Developer    : VicVil  
// DateCreate   : 13/03/2025
// Description  : Controlador para Registrar los Feriados del Año
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Recursos_Humanos;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;

namespace Recursos_Humanos.Controllers
{
    [ApiExplorerSettings(GroupName = "Recursos_Humanos")]
    [Route("api/Recursos_Humanos/[controller]")]
    [ApiController]
    public class FeriadoControllerController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public FeriadoControllerController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/FeriadoController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestFeriadoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestFeriadoModel>>> PostFeriado([FromBody] requestFeriadoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string storeFeriadoController = "sp_FeriadoController";
                List<FeriadoDBModel> producto = await _dbContext.FeriadoDB.FromSqlRaw(storeFeriadoController, model).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<FeriadoDBModel>{ Exito = 1, Mensaje = "Success" });
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

        
        // POST: api/FeriadoController/DuplicarFeriados
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("DuplicarFeriados")]
        public async Task<ActionResult<Respuesta<string>>> DuplicarFeriados([FromBody] int anioNuevo)
        {
            if (anioNuevo <= 0)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Año inválido", Data = new ErrorTxA { codigo = "01", Mensaje = "El año debe ser mayor a 0" } });
            }

            try
            {
                string storeDuplicarFeriados = "sp_DuplicarFeriados";
                int resultado = await _dbContext.Database.ExecuteSqlRawAsync($"EXEC {storeDuplicarFeriados} @AnioNuevo={anioNuevo}");

                if (resultado > 0)
                {
                    return Ok(new Respuesta<string> { Exito = 1, Mensaje = "Feriados duplicados exitosamente" });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No se duplicaron feriados", Data = new ErrorTxA { codigo = "02", Mensaje = "No se encontraron feriados para duplicar" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" } });
            }
        }



        // PUT: api/FeriadoController/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestFeriadoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutFeriado(string CodFeriado, [FromBody] requestFeriadoModel model)
        {
            try
            {
                string storeFeriadoController = "CONFIGURACION.sp_FeriadoController @TipoOperacion = '3', @CodFeriado = '" + CodFeriado + "', @cNombreFeriado = '" + model.cNombre + "', @tFechaFeriado = '" + model.tFecha + "', @cTipo = '"+ model.cTipo + "',@bEsRecurrente = '"+model.bEsRecurrente+"',@cRegion = '"+model.cRegion+"',@iAño = '"+ model.iAño +"', @bActivo = '"+model.bActivo+"' ";
                var updateResult = await _dbContext.FeriadoDB.FromSqlRaw(storeFeriadoController).ToListAsync();
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

        // GET: api/FeriadoController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestFeriadoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestFeriadoModel>>> GetFeriadoModel([FromQuery] int? bActivo, [FromQuery] string? CodFeriado , [FromQuery] string? cNombreFeriado, [FromQuery] DateTime ? tFechaFeriado, [FromQuery] string? cTipo, [FromQuery] int? bEsRecurrente, [FromQuery] string? cRegion, [FromQuery] int? iAño)
        {
            try
            {
                string storeFeriadoController = "CONFIGURACION.sp_FeriadoController @TipoOperacion = '1',@CodFeriado = {0}, @cNombreFeriado = {1}, @tFechaFeriado = {2}, @cTipo = {3} , @bEsRecurrente = {4} , @cRegion = {5} ,@iAño = {6},@bActivo = {7}";
                var result = await _dbContext.FeriadoDB.FromSqlRaw(storeFeriadoController, CodFeriado, cNombreFeriado  , (object?)tFechaFeriado ?? DBNull.Value , cTipo , (object ?)bEsRecurrente ?? DBNull.Value ,(object ?)iAño ??DBNull.Value, (object ?)cRegion ??DBNull.Value ,(object?)bActivo ?? DBNull.Value).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/FeriadoController/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestFeriadoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteFeriado(string CodFeriado)
        {
            try
            {                
                string storeFeriadoController = "CONFIGURACION.sp_FeriadoController @TipoOperacion = 4,@CodFeriado = '" + CodFeriado + "'";
                var deleteResult = await _dbContext.FeriadoDB.FromSqlRaw(storeFeriadoController).ToListAsync();
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