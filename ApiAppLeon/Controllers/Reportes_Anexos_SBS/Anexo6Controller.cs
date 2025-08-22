
// Developer    : VicVil  
// DateCreate   : 06/05/2025
// Description  : Controlador para generar el anexo6
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class Anexo6Controller : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Anexo6Controller(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Anexo6
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo6Model>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestAnexo6Model>>> PostAnexo6([FromBody] requestAnexo6Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string storeAnexo6 = "REPORTES_ANEXOS_SBS.SP_ANEXO6CONTROLLER @OPERACION";
                List<Anexo6DBModel> producto = await _dbContext.Anexo6DB.FromSqlRaw(storeAnexo6, model).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<Anexo6DBModel>{ Exito = 1, Mensaje = "Success" });
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

        


        [HttpGet("DescargarSucave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo6Model>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAnexo6Model>>> GetAnexo13SucaveModel([FromQuery] string CodAnexo6)
        {
            try
            {
                string storeAnexo6Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO6CONTROLLER @OPERACION = '3',@CodAnexo = {0}";
                var result = await _dbContext.Anexo6SucaveDB.FromSqlRaw(storeAnexo6Controller, CodAnexo6).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        
        [HttpGet("DatosExcel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo13Model>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAnexo6Model>>> GetAnexo6Model([FromQuery] string? CodAnexo6, [FromQuery] string? cMes, [FromQuery] string? cAnio, [FromQuery] string? cUsuario)
        {
            try
            {
                string storeAnexo6Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO6CONTROLLER @OPERACION = '4',@CodAnexo = {0}, @cMes = {1}, @cAnio = {2}, @cUsuario = {3} ";
                var result = await _dbContext.Anexo6DB.FromSqlRaw(storeAnexo6Controller, CodAnexo6, cMes, cAnio, cUsuario).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }





    }
}