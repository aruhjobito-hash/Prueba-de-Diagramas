
// Developer    : migzav  
// DateCreate   : 15/04/2025
// Description  : Controlador Mantenedor Articulos
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Logistica;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using ApiAppLeon.Models.Operaciones;
using ApiAppLeon.Models.Negocios;
using Microsoft.Data.SqlClient;

namespace Logistica.Controllers
{
    [ApiExplorerSettings(GroupName = "Logistica")]
    [Route("api/Logistica/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public ArticulosController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost("InsertarArticulo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> InsertarGrupo([FromBody] ArticuloCreateModel articulo)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(articulo.Nombre) || string.IsNullOrWhiteSpace(articulo.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros Descripcion y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 2);
                var param2 = new SqlParameter("@Nombre", articulo.Nombre);
                var param3 = new SqlParameter("@IdGrupo", articulo.IdGrupo);
                var param4 = new SqlParameter("@IdUniMed", articulo.IdUniMed);
                var param5 = new SqlParameter("@AfectoIGV", articulo.AfectoIGV);
                var param6 = new SqlParameter("@AfectoKardex", articulo.AfectoKardex);
                var param7 = new SqlParameter("@Usuario", articulo.Usuario);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantArticulos @TipoOperacion = {0},@Nombre = {1}, @IdGrupo = {2}, @IdUniMed ={3},@AfectoIGV = {4},@AfectoKardex = {5},@Usuario = {6}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo registrar artículo."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Artículo registrado correctamente."
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

        [HttpPut("EditarArticulo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> EditarArticulo([FromBody] ArticuloEditModel articulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.IdArticulo) || string.IsNullOrWhiteSpace(articulo.Usuario) || string.IsNullOrWhiteSpace(articulo.Activo))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdArticulo, Usuario y Activo son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 3);
                var param2 = new SqlParameter("@IdArticulo", articulo.IdArticulo);
                var param3 = new SqlParameter("@IdGrupo", articulo.IdGrupo);
                var param4 = new SqlParameter("@IdUniMed", articulo.IdUniMed);
                var param5 = new SqlParameter("@AfectoIGV", articulo.AfectoIGV);
                var param6 = new SqlParameter("@AfectoKardex", articulo.AfectoKardex);
                var param7 = new SqlParameter("@Usuario", articulo.Usuario);
                var param8 = new SqlParameter("@Activo", articulo.Activo);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantArticulos @TipoOperacion = {0}, @IdArticulo = {1},@IdGrupo = {2}, @IdUniMed ={3},@AfectoIGV = {4},@AfectoKardex = {5},@Usuario = {6},@Activo = {7}";


                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7, param8);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo actualizar artículo. Verifica que el IdArticulo exista."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Artículo actualizado correctamente."
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

        [HttpGet("ObtenerArticulos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ArticulosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ArticulosDBModel>>>> ObtenerArticulos()
        {
            try
            {

                var result = await _dbContext.Set<ArticulosDBModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantArticulos @TipoOperacion = 1")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<ArticulosDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron artículos.",
                        Data = new List<ArticulosDBModel>()
                    });
                }


                return Ok(new Respuesta<List<ArticulosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Artículos obtenidos correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }

        [HttpGet("ComboObtenerArticulos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<ArticulosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<ArticulosDBModel>>>> ComboObtenerArticulos()
        {
            try
            {

                var result = await _dbContext.Set<ArticulosDBModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantArticulos @TipoOperacion = 5")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<ArticulosDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron artículos.",
                        Data = new List<ArticulosDBModel>()
                    });
                }


                return Ok(new Respuesta<List<ArticulosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Artículos obtenidos correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPut("DesactivarArticulo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> DesactivarArticulo([FromBody] ArticuloUpdateModel articulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.IdArticulo) || string.IsNullOrWhiteSpace(articulo.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdArticulo y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 4);
                var param2 = new SqlParameter("@IdArticulo", articulo.IdArticulo);
                var param3 = new SqlParameter("@Usuario", articulo.Usuario);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantArticulos @TipoOperacion = {0}, @IdArticulo = {1}, @Usuario = {2}";

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

        //UNIDAD DE MEDIDA
        [HttpGet("ObtenerComboUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<UnidadMedidaModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<UnidadMedidaModel>>>> ObtenerComboUnidadMedida()
        {
            try
            {

                var result = await _dbContext.Set<UnidadMedidaModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantUnidadMedida @TipoOperacion = 1, @Combo=1")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<UnidadMedidaModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron unidades de medida.",
                        Data = new List<UnidadMedidaModel>()
                    });
                }


                return Ok(new Respuesta<List<UnidadMedidaModel>>
                {
                    Exito = 1,
                    Mensaje = "Unidades de medida obtenidas correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpGet("ObtenerUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<UnidadMedidaModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<UnidadMedidaModel>>>> ObtenerUnidadMedida()
        {
            try
            {

                var result = await _dbContext.Set<UnidadMedidaModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantUnidadMedida @TipoOperacion = 1")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<UnidadMedidaModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron unidades de medida.",
                        Data = new List<UnidadMedidaModel>()
                    });
                }


                return Ok(new Respuesta<List<UnidadMedidaModel>>
                {
                    Exito = 1,
                    Mensaje = "Unidades de medida obtenidas correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPut("DesactivarUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> DesactivarUnidadMedida([FromBody] UnidadMedidaUpdateModel unidad)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(unidad.IdUniMed) || string.IsNullOrWhiteSpace(unidad.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdUniMed y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 2);
                var param2 = new SqlParameter("@IdUniMed", unidad.IdUniMed);
                var param3 = new SqlParameter("@Usuario", unidad.Usuario);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantUnidadMedida @TipoOperacion = {0}, @IdUniMed = {1}, @Usuario = {2}";

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

        [HttpPost("InsertarUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> InsertarUnidadMedida([FromBody] UnidadMedidaCreateModel unidad)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(unidad.Descripcion) || string.IsNullOrWhiteSpace(unidad.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros Descripcion y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 3); 
                var param2 = new SqlParameter("@Descripcion", unidad.Descripcion);  
                var param3 = new SqlParameter("@Usuario", unidad.Usuario);  

                string storeProcedure = "EXEC CONFIGURACION.sp_MantUnidadMedida @TipoOperacion = {0}, @Descripcion = {1}, @Usuario = {2}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo insertar la unidad de medida."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Unidad de medida insertada correctamente."
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

        [HttpPut("EditarUnidadMedida")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> EditarUnidadMedida([FromBody] UnidadMedidaEditModel unidad)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(unidad.IdUniMed) || string.IsNullOrWhiteSpace(unidad.Usuario) || string.IsNullOrWhiteSpace(unidad.Activo))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdUniMed, Usuario y Activo son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 4);
                var param2 = new SqlParameter("@IdUniMed", unidad.IdUniMed);
                var param3 = new SqlParameter("@Usuario", unidad.Usuario);
                var param4 = new SqlParameter("@Activo", unidad.Activo);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantUnidadMedida @TipoOperacion = {0}, @IdUniMed = {1}, @Usuario = {2}, @Activo = {3}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo actualizar la unidad de medida. Verifica que el IdUniMed exista."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Unidad de medida actualizada correctamente."
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


        //GRUPOS

        [HttpGet("ObtenerComboGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<GrupoModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<GrupoModel>>>> ObtenerComboGrupo()
        {
            try
            {

                var result = await _dbContext.Set<GrupoModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = 1,@Combo = 1")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<GrupoModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron grupos.",
                        Data = new List<GrupoModel>()
                    });
                }


                return Ok(new Respuesta<List<GrupoModel>>
                {
                    Exito = 1,
                    Mensaje = "Grupos obtenidos correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }

        [HttpGet("ObtenerGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<GrupoModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<GrupoModel>>>> ObtenerGrupo()
        {
            try
            {

                var result = await _dbContext.Set<GrupoModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = 1")
                    .ToListAsync();


                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<GrupoModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron grupos.",
                        Data = new List<GrupoModel>()
                    });
                }


                return Ok(new Respuesta<List<GrupoModel>>
                {
                    Exito = 1,
                    Mensaje = "Grupos obtenidos correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPut("DesactivarGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> DesactivarGrupo([FromBody] GrupoUpdateModel grupo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(grupo.IdGrupo) || string.IsNullOrWhiteSpace(grupo.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdGrupo y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 2);
                var param2 = new SqlParameter("@IdGrupo", grupo.IdGrupo);
                var param3 = new SqlParameter("@Usuario", grupo.Usuario);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = {0}, @IdGrupo = {1}, @Usuario = {2}";

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

        [HttpPost("InsertarGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> InsertarGrupo([FromBody] GrupoCreateModel grupo)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(grupo.Descripcion) || string.IsNullOrWhiteSpace(grupo.Usuario))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros Descripcion y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 3);
                var param2 = new SqlParameter("@Descripcion", grupo.Descripcion);
                var param3 = new SqlParameter("@CtaContable", grupo.CtaContable);
                var param4 = new SqlParameter("@TasaDepreciacion", grupo.TasaDepreciacion);
                var param5 = new SqlParameter("@Usuario", grupo.Usuario);
                var param6 = new SqlParameter("@ActivoFijo", grupo.ActivoFijo);
                var param7 = new SqlParameter("@Kardex", grupo.Kardex);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = {0}, @Descripcion = {1}, @CtaContable ={2}, @TasaDepreciacion={3}, @Usuario = {4}, @ActivoFijo={5}, @Kardex={6}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo registrar el grupo."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Grupo registrado correctamente."
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

        [HttpPut("EditarGrupo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> EditarGrupo([FromBody] GrupoEditModel grupo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(grupo.IdGrupo) || string.IsNullOrWhiteSpace(grupo.Usuario) || string.IsNullOrWhiteSpace(grupo.Activo))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdGrupo, Usuario y Activo son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 4);
                var param2 = new SqlParameter("@IdGrupo", grupo.IdGrupo);
                var param3 = new SqlParameter("@TasaDepreciacion", grupo.TasaDepreciacion);
                var param4 = new SqlParameter("@Usuario", grupo.Usuario);
                var param5 = new SqlParameter("@Activo", grupo.Activo);
                var param6 = new SqlParameter("@ActivoFijo", grupo.ActivoFijo);
                var param7 = new SqlParameter("@Kardex", grupo.Kardex);

                string storeProcedure = "EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = {0}, @IdGrupo = {1}, @TasaDepreciacion={2}, @Usuario = {3}, @Activo = {4},@ActivoFijo={5}, @Kardex={6}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4,param5, param6, param7);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo actualizar grupo. Verifica que el IdGrupo exista."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Grupo actualizado correctamente."
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

        [HttpGet("ObtenerPlanCuentas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PlanCuentasModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<PlanCuentasModel>>>> ObtenerPlanCuentas(string? filtroCuenta)
        {
            try
            {
                var parametro1 = new SqlParameter("@TipoOperacion", 5);
                var parametro2 = new SqlParameter("@Descripcion", filtroCuenta ?? (object)DBNull.Value);

                var result = await _dbContext.Set<PlanCuentasModel>()
                    .FromSqlRaw("EXEC CONFIGURACION.sp_MantGrupo @TipoOperacion = @TipoOperacion, @Descripcion = @Descripcion", parametro1, parametro2)
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<PlanCuentasModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron cuentas contables.",
                        Data = new List<PlanCuentasModel>()
                    });
                }

                return Ok(new Respuesta<List<PlanCuentasModel>>
                {
                    Exito = 1,
                    Mensaje = "Cuentas Contables obtenidas correctamente",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


    }
}