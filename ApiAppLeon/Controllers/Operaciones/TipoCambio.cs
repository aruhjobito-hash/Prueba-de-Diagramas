
// Developer    : migzav  
// DateCreate   : 26/02/2025
// Description  : Controlador para tipo de cambio
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Operaciones;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using System;

namespace Operaciones.Controllers
{
    [ApiExplorerSettings(GroupName = "Operaciones")]
    [Route("api/Operaciones/[controller]")]
    [ApiController]
    public class TipoCambioController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public TipoCambioController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/TipoCambio
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestTipoCambioModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestTipoCambioModel>>> PostTipoCambio([FromBody] requestTipoCambioModel model)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(new { Exito = 0, Mensaje = "Datos inválidos" });
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC sp_TipoCambio @TipoOperacion, @Fecha, @Compra, @Venta, @Fijo,@IdUser, @CompraSunat, @VentaSunat,  @IdCargo, @IdArea";
                await _dbContext.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@TipoOperacion", 2),
                    new SqlParameter("@Fecha", model.Fecha),
                    new SqlParameter("@Compra", (object)model.Compra ?? DBNull.Value),
                    new SqlParameter("@Venta", (object)model.Venta ?? DBNull.Value),
                    new SqlParameter("@Fijo", (object)model.Fijo ?? DBNull.Value),
                    new SqlParameter("@IdUser", model.IdUser ?? (object)DBNull.Value),
                    new SqlParameter("@CompraSunat", (object)model.CompraSunat ?? DBNull.Value),
                    new SqlParameter("@VentaSunat", (object)model.VentaSunat ?? DBNull.Value),
                    new SqlParameter("@IdCargo", model.IdCargo),
                    new SqlParameter("@IdArea", model.IdArea)
                );


                return Ok(new Respuesta<TipoCambioDBModel> { Exito = 1, Mensaje = "Success" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/TipoCambio
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipoCambioDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]

        public async Task<ActionResult<IEnumerable<TipoCambioDBModel>>> GetTipoCambioModel([FromQuery] string fecha)

        {

            try
            {
                string query = "EXEC sp_TipoCambio @TipoOperacion, @Fecha";

                List<TipoCambioDBModel> result = await _dbContext.TipoCambioDB
                    .FromSqlRaw(query,
                        new SqlParameter("@TipoOperacion", 1),
                        new SqlParameter("@Fecha", fecha)
                    )
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return Ok(new Respuesta<List<TipoCambioDBModel>> //ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No data found",
                        Data = result//new ErrorTxA { codigo = "02", Mensaje = "No records found for the given date" }
                    });
                }

                return Ok(new Respuesta<List<TipoCambioDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Datos encontrados",
                    Data = result
                });


            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

      

    }
}