
// Developer    : migzav  
// DateCreate   : 19/03/2025
// Description  : Controlador obtener Productos Negocios
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Negocios;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using ApiAppLeon.Models.Operaciones;
using Microsoft.Data.SqlClient;
using ApiAppLeon.Entidades;
using System.Runtime.Intrinsics.Arm;

namespace Negocios.Controllers
{
    [ApiExplorerSettings(GroupName = "Negocios")]
    [Route("api/Negocios/[controller]")]
    [ApiController]
    public class ProductosNegociosController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public ProductosNegociosController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/ProductosNegocios
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestProductosNegociosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]

        public async Task<ActionResult<Respuesta<requestProductosNegociosModel>>> PostProductosNegocios([FromBody] requestProductosNegociosModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_ProductosNegocios @TipoOperacion, @IdDenoCre, @Denominacion, @FecVig, @IdUser, @MontoMin, @MontoMax, @Abrev, @DiasPlazoPC, @DxP, @sinGara, @PerGracia, @PorcAmplia,@AprobAuto";
                var result = await _dbContext.ResultadoIdDenoCre
                    .FromSqlRaw(query,
                        new SqlParameter("@TipoOperacion", 2),
                        new SqlParameter("@IdDenoCre", model.IdDenoCre ?? (object)DBNull.Value),
                        new SqlParameter("@Denominacion", model.Denominacion),
                        new SqlParameter("@FecVig", model.FecVig),
                        new SqlParameter("@IdUser", model.IdUser),
                        new SqlParameter("@MontoMin", model.MontoMin),
                        new SqlParameter("@MontoMax", model.MontoMax),
                        new SqlParameter("@Abrev", model.Abrev),
                        new SqlParameter("@DiasPlazoPC", model.DiasPlazoPC),
                        new SqlParameter("@DxP", model.DxP),
                        new SqlParameter("@sinGara", model.sinGara),
                        new SqlParameter("@PerGracia", model.PerGracia),
                        new SqlParameter("@PorcAmplia", model.PorcAmplia),
                        new SqlParameter("@AprobAuto", model.AprobAuto)
                    ).ToListAsync();

                var idGenerado = result.FirstOrDefault()?.IdDenoCre;
                model.IdDenoCre = idGenerado;

                return Ok(new Respuesta<requestProductosNegociosModel>
                {
                    Exito = 1,
                    Mensaje = "Producto registrado con éxito",
                    Data = model
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        // PUT: api/ProductosNegocios/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestProductosNegociosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]

        public async Task<ActionResult<Respuesta<requestProductosNegociosModel>>> PutProductosNegocios([FromBody] requestProductosNegociosModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_ProductosNegocios @TipoOperacion ,@IdDenoCre,@Denominacion ,@FecVig,@IdUser ,@MontoMin ,@MontoMax,@Abrev ,@DiasPlazoPC ,@DxP,@sinGara,@PerGracia, @PorcAmplia,@AprobAuto,@Activo ";
                await _dbContext.Database.ExecuteSqlRawAsync(query,
                    new SqlParameter("@TipoOperacion", 3),
                    new SqlParameter("@IdDenoCre", model.IdDenoCre),
                    new SqlParameter("@Denominacion", model.Denominacion),
                    new SqlParameter("@FecVig", model.FecVig),
                    new SqlParameter("@IdUser", model.IdUser),
                    new SqlParameter("@MontoMin", model.MontoMin),
                    new SqlParameter("@MontoMax", model.MontoMax),
                    new SqlParameter("@Abrev", model.Abrev),
                    new SqlParameter("@DiasPlazoPC", model.DiasPlazoPC),
                    new SqlParameter("@DxP", model.DxP),
                    new SqlParameter("@sinGara", model.sinGara),
                    new SqlParameter("@PerGracia", model.PerGracia),
                    new SqlParameter("@PorcAmplia", model.PorcAmplia),
                    new SqlParameter("@AprobAuto", model.AprobAuto),
                    new SqlParameter("@Activo", model.Activo)
                );

                return Ok(new Respuesta<ProductosNegociosDBModel> { Exito = 1, Mensaje = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        // GET: api/ProductosNegocios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestProductosNegociosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<requestProductosNegociosModel>>>> GetProductosNegociosModel([FromHeader] int tipoOperacion = 1)
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

                string query = "EXEC CONFIGURACION.sp_ProductosNegocios @TipoOperacion = 1";

                var result = await _dbContext.ProductosNegociosDB
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

                var response = result.Select(x => new ProductosNegociosDBModel
                {
                    IdDenoCre = x.IdDenoCre,
                    Denominacion = x.Denominacion,
                    FecVig = x.FecVig,
                    IdUser = x.IdUser,
                    MontoMin = x.MontoMin,
                    MontoMax = x.MontoMax,
                    Abrev = x.Abrev,
                    DiasPlazoPC = x.DiasPlazoPC,
                    DxP = x.DxP,
                    sinGara = x.sinGara,
                    PerGracia = x.PerGracia,
                    PorcAmplia=x.PorcAmplia,
                    AprobAuto=x.AprobAuto,
                    Activo=x.Activo



                }).ToList();

                return Ok(new Respuesta<List<ProductosNegociosDBModel>>
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
        // DELETE: api/ProductosNegocios/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestProductosNegociosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]

        public async Task<ActionResult<Respuesta<string>>> DeleteProductosNegocios([FromQuery] string IdDenoCre, [FromQuery] string idUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(IdDenoCre) || string.IsNullOrWhiteSpace(idUser))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdDenoCre e idUser son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 4);  
                var param2 = new SqlParameter("@IdDenoCre", IdDenoCre);
                var param3 = new SqlParameter("@IdUser", idUser);

                string storeProcedure = "EXEC CONFIGURACION.sp_ProductosNegocios @TipoOperacion = {0}, @IdDenoCre = {1}, @IdUser = {2}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se encontró el registro o ya está inactivo."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Registro desactivado correctamente."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Error interno del servidor",
                    Data = new ErrorTxA { codigo = "03", Mensaje = ex.Message }
                });
            }
        }



        [HttpGet("ListaDestinos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ListaDestinosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ListaDestinosDBModel>>>> GetListaDestinos([FromQuery] string? IdDenoCre)
        {
            try
            {

                var result = await _dbContext.ListaDestinosDB
                    .FromSqlRaw("exec CONFIGURACION.sp_ObtenerListaDestinos @TipoOperacion=1, @IdLista=1, @IdDenoCre = {0}", IdDenoCre ?? (object)DBNull.Value)
                    .ToListAsync();

                return Ok(new Respuesta<List<ListaDestinosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Destinos obtenidos correctamente",
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

        [HttpGet("ListaFinalidad")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ListaDestinosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ListaDestinosDBModel>>>> GetListaFinalidad([FromQuery] string? IdDenoCre)
        {
            try
            {

                var result = await _dbContext.ListaDestinosDB
                    .FromSqlRaw("exec CONFIGURACION.sp_ObtenerListaDestinos @TipoOperacion=1, @IdLista=2, @IdDenoCre = {0}", IdDenoCre ?? (object)DBNull.Value)
                    .ToListAsync();

                return Ok(new Respuesta<List<ListaDestinosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Finalidad obtenidos correctamente",
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

        [HttpGet("ListaGarantias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ListaDestinosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ListaDestinosDBModel>>>> GetListaGarantias([FromQuery] string? IdDenoCre)
        {
            try
            {

                var result = await _dbContext.ListaDestinosDB
                    .FromSqlRaw("exec CONFIGURACION.sp_ObtenerListaDestinos @TipoOperacion=1, @IdLista=3, @IdDenoCre = {0}", IdDenoCre ?? (object)DBNull.Value)
                    .ToListAsync();

                return Ok(new Respuesta<List<ListaDestinosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Garantías obtenidos correctamente",
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



        [HttpPost("RegistrarDestinos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RegistrarDestinos([FromBody] RegistrarDestinosRequest request)

        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.IdDenoCre))
                {
                    return BadRequest(new { mensaje = "IdDenoCre es obligatorio" });
                }

                // Elimina los destinos anteriores
                var idListasUnicas = request.ListaDestinos
                    .Select(x => x.IdLista)
                    .Distinct()
                    .ToList();

                foreach (var idLista in idListasUnicas)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "exec CONFIGURACION.sp_ObtenerListaDestinos @TipoOperacion=2, @IdDenoCre={0}, @IdLista={1}",
                        request.IdDenoCre, idLista
                    );
                }

                // Inserta los nuevos seleccionados
                var seleccionados = request.ListaDestinos.Where(x => x.Seleccionado == 1).ToList();

                foreach (var item in seleccionados)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "exec CONFIGURACION.sp_ObtenerListaDestinos @TipoOperacion=3, @IdDenoCre={0}, @IdLista={1}, @IdListaDetalle={2}",
                        request.IdDenoCre, item.IdLista, item.IdListaDetalle
                    );
                }


                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Destinos registrados correctamente ✅",
                    Data = null
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<string>
                {
                    Exito = 0,
                    Mensaje = "Error al registrar destinos ❌",
                    Data = ex.Message
                });

            }
        }


    }

}