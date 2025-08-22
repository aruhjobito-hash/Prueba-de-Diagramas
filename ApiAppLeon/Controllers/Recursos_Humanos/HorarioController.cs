
// Developer    : VicVil  
// DateCreate   : 05/03/2025
// Description  : Controlador para Registrar los Horarios de Empleados
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Recursos_Humanos;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.AspNetCore.Authorization;

namespace Recursos_Humanos.Controllers
{
    [ApiExplorerSettings(GroupName = "Recursos_Humanos")]
    [Route("api/Recursos_Humanos/[controller]")]
    [ApiController]
    public class HorarioControllerController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public HorarioControllerController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/HorarioController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestHorarioModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestHorarioModel>>> PostHorario([FromBody] requestHorarioModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                List<HorarioDBModel> producto = new List<HorarioDBModel>();
                string storeHorarioController = "CONFIGURACION.sp_HorarioController @TipoOperacion = 2,@CodHorario= Null,@cNombreHorario = '" + model.cNombreHorario + "',@cDescripcionHorario = '" + model.cDescripcionHorario + "',@bActivo = '"+ model.bActivo +"'";
                producto = await _dbContext.HorarioDB.FromSqlRaw(storeHorarioController).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<HorarioDBModel>{ Exito = 1, Mensaje = "Success" });
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

        
        // PUT: api/HorarioController/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestHorarioModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutHorario(string CodHorario, [FromBody] requestHorarioModel model)
        {
            try
            {
                string storeHorarioController = "CONFIGURACION.sp_HorarioController @TipoOperacion = 3,@CodHorario= '" + CodHorario + "',@cNombreHorario = '" + model.cNombreHorario + "',@cDescripcionHorario = '" + model.cDescripcionHorario + "',@bActivo = '" + model.bActivo + "'";
                var updateResult = await _dbContext.HorarioDB.FromSqlRaw(storeHorarioController).ToListAsync();
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

        // GET: api/HorarioController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestHorarioModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestHorarioModel>>> GetHorarioModel([FromQuery] int? bActivo, [FromQuery] string? cNombreHorario, [FromQuery] string? cDescripcionHorario)
        {
            try
            {
                string storeHorarioController = "CONFIGURACION.sp_HorarioController @TipoOperacion = '1',@CodHorario = Null, @cNombreHorario = {0}, @cDescripcionHorario = {1}, @bActivo = {2}";
                var result = await _dbContext.HorarioDB.FromSqlRaw(storeHorarioController, cNombreHorario, cDescripcionHorario, (object?)bActivo ?? DBNull.Value).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/HorarioController/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestHorarioModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteHorario(string CodHorario)
        {
            try
            {
                string storeHorarioController = "CONFIGURACION.sp_HorarioController @TipoOperacion = 4,@CodHorario= '" + CodHorario + "',@cNombreHorario = Null,@cDescripcionHorario = Null,@bActivo = Null ";
                var deleteResult = await _dbContext.HorarioDB.FromSqlRaw(storeHorarioController).ToListAsync();
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