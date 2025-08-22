
// Developer    : migzav  
// DateCreate   : 18/03/2025
// Description  : Controlador obtener Cuentas
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Operaciones;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using ApiAppLeon.Models.Logistica;

namespace Operaciones.Controllers
{
    [ApiExplorerSettings(GroupName = "Operaciones")]
    [Route("api/Operaciones/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public CuentasController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Cuentas
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]

        public async Task<ActionResult<Respuesta<requestCuentasModel>>> PostCuentas([FromBody] requestCuentasModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_Producto_Detalle @TipoOperacion,@IdProducto,@IdProducto_Detalle,@Descripcion,@IdUser,@TipMoneda,@TasaVigente,@Plazo,@MontoMin,@FechaInicioVigencia";
                await _dbContext.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@TipoOperacion", 2),
                    new SqlParameter("@IdProducto", model.IdProducto),
                    new SqlParameter("@IdProducto_Detalle", model.IdProducto_Detalle),
                    new SqlParameter("@Descripcion",model.Descripcion),
                    new SqlParameter("@IdUser", model.IdUser),
                    new SqlParameter("@TipMoneda", model.TipMoneda),
                    new SqlParameter("@TasaVigente", model.TasaVigente),
                    new SqlParameter("@Plazo", model.Plazo),
                    new SqlParameter("@MontoMin", model.MontoMin),
                    new SqlParameter("@FechaInicioVigencia", model.FechaInicioVigencia)
                );

                return Ok(new Respuesta<CuentasDBModel> { Exito = 1, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        // PUT: api/Cuentas/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
      
        public async Task<ActionResult<Respuesta<requestCuentasModel>>> PutCuentas([FromBody] requestCuentasModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_Producto_Detalle @TipoOperacion,@IdProducto,@IdProducto_Detalle,@Descripcion,@IdUser,@TipMoneda,@TasaVigente,@Plazo,@MontoMin,@FechaInicioVigencia,@Activo";
                await _dbContext.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@TipoOperacion", 3),
                    new SqlParameter("@IdProducto", model.IdProducto),
                    new SqlParameter("@IdProducto_Detalle", model.IdProducto_Detalle),
                    new SqlParameter("@Descripcion", model.Descripcion),
                    new SqlParameter("@IdUser", model.IdUser),
                    new SqlParameter("@TipMoneda", model.TipMoneda),
                    new SqlParameter("@TasaVigente", model.TasaVigente),
                    new SqlParameter("@Plazo", model.Plazo),
                    new SqlParameter("@MontoMin", model.MontoMin),
                    new SqlParameter("@FechaInicioVigencia", model.FechaInicioVigencia),
                    new SqlParameter("@Activo", model.Activo)
                );

                return Ok(new Respuesta<CuentasDBModel> { Exito = 1, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        // GET: api/Cuentas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]

        public async Task<ActionResult<Respuesta<List<requestCuentasModel>>>> GetCuentasModel([FromHeader] int tipoOperacion = 1)
        {
            try
            {
                if (tipoOperacion != 1)
                {
                    return BadRequest(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "Operación no permitida",
                        Data = new ErrorTxA { codigo = "01", Mensaje = "Solo se permite tipoOperacion = 1 en GET" }
                    });
                }

                string query = "EXEC CONFIGURACION.sp_Producto_Detalle @TipoOperacion = 1";

                var result = await _dbContext.CuentasDB
                    .FromSqlRaw(query)
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron cuentas",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No hay cuentas activas en el sistema" }
                    });
                }

                var response = result.Select(x => new CuentasDBModel
                {
                    IdProducto = x.IdProducto,
                    IdProducto_Detalle= x.IdProducto_Detalle,
                    Descripcion = x.Descripcion,
                    TipMoneda = x.TipMoneda,
                    TasaVigente = x.TasaVigente,
                    Plazo = x.Plazo,
                    MontoMin = x.MontoMin,
                    FechaInicioVigencia = x.FechaInicioVigencia,
                    Activo=x.Activo
                }).ToList();

                return Ok(new Respuesta<List<CuentasDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Cuentas obtenidas correctamente",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" }
                });
            }
        }
        // DELETE: api/Cuentas/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]

        public async Task<ActionResult<Respuesta<string>>> DeleteProductoDetalle(string idProducto, string idProductoDetalle, string idUser)
        {
            try
            {
                string storeProcedure = "EXEC CONFIGURACION.sp_Producto_Detalle @TipoOperacion = 4, @IdProducto = {0}, @IdProducto_Detalle = {1}, @IdUser = {2}";

                var result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, idProducto, idProductoDetalle, idUser);

                if (result == 0)
                {
                    return BadRequest(new Respuesta<string> { Exito = 0, Mensaje = "No se encontró el registro o ya está inactivo." });
                }

                return Ok(new Respuesta<string> { Exito = 1, Mensaje = "Registro desactivado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" }
                });
            }
        }

        [HttpGet("ObtenerProductos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ProductosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ProductosDBModel>>>> GetObtenerProductos()
        {
            try
            {
                var result = await _dbContext.Set<ProductosDBModel>()
                .FromSqlRaw("EXEC CONFIGURACION.sp_Producto_Detalle @TipoOperacion = 5")
                .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<ProductosDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron productos.",
                        Data = new List<ProductosDBModel>()
                    });
                }


                return Ok(new Respuesta<List<ProductosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Productos obtenidos correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" }
                });
            }
        }



    }
}