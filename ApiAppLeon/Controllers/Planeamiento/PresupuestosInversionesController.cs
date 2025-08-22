
// Developer    : migzav  
// DateCreate   : 28/04/2025
// Description  : Controlador Mantenedor Presupuestos Inversiones
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Planeamiento;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using ApiAppLeon.Models.Logistica;
using ApiAppLeon.Models.Negocios;
using Microsoft.Data.SqlClient;
using ApiAppLeon.Models.Operaciones;

namespace Planeamiento.Controllers
{
    [ApiExplorerSettings(GroupName = "Planeamiento")]
    [Route("api/Planeamiento/[controller]")]
    [ApiController]
    public class PresupuestosInversionesController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PresupuestosInversionesController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("RegistrarArticuloInversiones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> RegistrarArticuloInversiones([FromBody] PresupuestosInversionesEditModel articulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.IdArticulo) || string.IsNullOrWhiteSpace(articulo.IdUser) || string.IsNullOrWhiteSpace(articulo.Activo))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdArticulo, Usuario y Activo son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 1);
                var param2 = new SqlParameter("@IdArticulo", articulo.IdArticulo);
                var param3 = new SqlParameter("@IdAgencia", articulo.IdAgencia);
                var param4 = new SqlParameter("@IdArea", articulo.IdArea);
                var param5 = new SqlParameter("@Año", articulo.Año);
                var param6 = new SqlParameter("@IdUnd", articulo.IdUnd);
                var param7 = new SqlParameter("@Cant", articulo.Cant);
                var param8 = new SqlParameter("@NR", articulo.NR);
                var param9 = new SqlParameter("@Enero", articulo.Enero);
                var param10 = new SqlParameter("@Febrero", articulo.Febrero);
                var param11 = new SqlParameter("@Marzo", articulo.Marzo);
                var param12 = new SqlParameter("@Abril", articulo.Abril);
                var param13 = new SqlParameter("@Mayo", articulo.Mayo);
                var param14 = new SqlParameter("@Junio", articulo.Junio);
                var param15 = new SqlParameter("@Julio", articulo.Julio);
                var param16 = new SqlParameter("@Agosto", articulo.Agosto);
                var param17 = new SqlParameter("@Septiembre", articulo.Septiembre);
                var param18 = new SqlParameter("@Octubre", articulo.Octubre);
                var param19 = new SqlParameter("@Noviembre", articulo.Noviembre);
                var param20 = new SqlParameter("@Diciembre", articulo.Diciembre);
                var param21 = new SqlParameter("@IdUser", articulo.IdUser);
                var param22 = new SqlParameter("@Activo", articulo.Activo);
                var param23 = new SqlParameter("@TipMoneda", articulo.TipMoneda);

                string storeProcedure = "EXEC PRESUPUESTO_ANUAL.sp_RegistrarPresupuestoInversiones @TipoOperacion = {0}, @IdArticulo = {1},@IdAgencia={2}, @IdArea = {3}, @Año ={4},@IdUnd = {5},@Cant = {6},@NR = {7},@Enero = {8},@Febrero = {9},@Marzo = {10},@Abril = {11},@Mayo = {12},@Junio = {13},@Julio = {14},@Agosto = {15},@Septiembre = {16},@Octubre = {17},@Noviembre = {18},@Diciembre = {19},@IdUser = {20},@Activo = {21},@TipMoneda = {22}";


                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18, param19, param20, param21, param22, param23);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo actualizar artículo de inversiones. Verifica que el IdArticulo exista."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Artículo de inversiones actualizado correctamente."
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
      
        [HttpPut("EditarArticuloInversiones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> EditarArticuloInversiones([FromBody] PresupuestosInversionesEditModel articulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.IdArticulo) || string.IsNullOrWhiteSpace(articulo.IdUser) || string.IsNullOrWhiteSpace(articulo.Activo))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdArticulo, Usuario y Activo son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 2);
                var param2 = new SqlParameter("@IdArticulo", articulo.IdArticulo);
                var param3 = new SqlParameter("@IdAgencia", articulo.IdAgencia);
                var param4 = new SqlParameter("@IdArea", articulo.IdArea);
                var param5 = new SqlParameter("@Año", articulo.Año);
                var param6 = new SqlParameter("@IdUnd", articulo.IdUnd);
                var param7 = new SqlParameter("@Cant", articulo.Cant);
                var param8 = new SqlParameter("@NR", articulo.NR);
                var param9 = new SqlParameter("@Enero", articulo.Enero);
                var param10 = new SqlParameter("@Febrero", articulo.Febrero);
                var param11 = new SqlParameter("@Marzo", articulo.Marzo);
                var param12 = new SqlParameter("@Abril", articulo.Abril);
                var param13 = new SqlParameter("@Mayo", articulo.Mayo);
                var param14 = new SqlParameter("@Junio", articulo.Junio);
                var param15 = new SqlParameter("@Julio", articulo.Julio);
                var param16 = new SqlParameter("@Agosto", articulo.Agosto);
                var param17 = new SqlParameter("@Septiembre", articulo.Septiembre);
                var param18 = new SqlParameter("@Octubre", articulo.Octubre);
                var param19 = new SqlParameter("@Noviembre", articulo.Noviembre);
                var param20 = new SqlParameter("@Diciembre", articulo.Diciembre);
                var param21 = new SqlParameter("@IdUser", articulo.IdUser);
                var param22 = new SqlParameter("@Activo", articulo.Activo);
                var param23 = new SqlParameter("@TipMoneda", articulo.TipMoneda);

                string storeProcedure = "EXEC PRESUPUESTO_ANUAL.sp_RegistrarPresupuestoInversiones @TipoOperacion = {0}, @IdArticulo = {1},@IdAgencia={2}, @IdArea = {3}, @Año ={4},@IdUnd = {5},@Cant = {6},@NR = {7},@Enero = {8},@Febrero = {9},@Marzo = {10},@Abril = {11},@Mayo = {12},@Junio = {13},@Julio = {14},@Agosto = {15},@Septiembre = {16},@Octubre = {17},@Noviembre = {18},@Diciembre = {19},@IdUser = {20},@Activo = {21},@TipMoneda = {22}";


                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18, param19, param20, param21, param22, param23);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo actualizar artículo de inversiones. Verifica que el IdArticulo exista."
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Artículo de inversiones actualizado correctamente."
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


        [HttpPost("ObtenerPresupuestoInversiones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PresupuestosInversionesDBModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<PresupuestosInversionesDBModel>>>> ObtenerPresupuestoInversiones( [FromBody] requestPresupuestosInversionesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "EXEC PRESUPUESTO_ANUAL.sp_BuscarPresupuestoInversiones @Año, @IdAgencia, @IdArea, @TipMoneda";
                var result = await _dbContext.PresupuestosInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@TipMoneda", model.TipMoneda)

                    ).ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return Ok(new Respuesta<List<PresupuestosInversionesDBModel>> 
                    {
                        Exito = 0,
                        Mensaje = "No data found",
                        Data = result
                    });
                }

                return Ok(new Respuesta<List<PresupuestosInversionesDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Presupuesto inversiones obtenido correctamente",
                    Data = result
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }


        }

        [HttpPut("DesactivarArticuloInversiones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> DesactivarArticuloInversiones([FromBody] PresupuestosUpdateModel articulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.IdArticulo) || string.IsNullOrWhiteSpace(articulo.IdUser))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros IdArticulo y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 3);
                var param2 = new SqlParameter("@IdArticulo", articulo.IdArticulo);
                var param3 = new SqlParameter("@IdAgencia", articulo.IdAgencia);
                var param4 = new SqlParameter("@IdArea", articulo.IdArea);
                var param5 = new SqlParameter("@Año", articulo.Año);
                var param6 = new SqlParameter("@IdUser", articulo.IdUser);
                var param7 = new SqlParameter("@TipMoneda", articulo.TipMoneda);

                string storeProcedure = "EXEC PRESUPUESTO_ANUAL.sp_RegistrarPresupuestoInversiones @TipoOperacion = {0},@IdArticulo = {1}, @IdAgencia = {2}, @IdArea = {3}, @Año = {4}, @IdUser = {5}, @TipMoneda={6}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7);

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


        [HttpPost("ObtenerComboAgencias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<AgenciasModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<AgenciasModel>>>> ObtenerComboAgencias([FromBody] RequestFiltroCombos request)
        {
            try
            {
                var result = await _dbContext.Set<AgenciasModel>()
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.sp_CombosPresupuestos @Func = {0}, @IdCargo = {1}, @IdArea = {2}, @IdAgencia = {3}",
                                1, request.IdCargo, request.IdArea, request.IdAgencia)
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<AgenciasModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron agencias.",
                        Data = new List<AgenciasModel>()
                    });
                }

                return Ok(new Respuesta<List<AgenciasModel>>
                {
                    Exito = 1,
                    Mensaje = "Agencias obtenidas correctamente",
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


        [HttpPost("ObtenerComboAreas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<AreasModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<AreasModel>>>> ObtenerComboAreas([FromBody] RequestFiltroCombos request)
        {
            try
            {
                var result = await _dbContext.Set<AreasModel>()
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.sp_CombosPresupuestos @Func = {0}, @IdCargo = {1}, @IdArea = {2}, @IdAgencia = {3}",
                                2, request.IdCargo, request.IdArea, request.IdAgencia)
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<AreasModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontraron Áreas.",
                        Data = new List<AreasModel>()
                    });
                }

                return Ok(new Respuesta<List<AreasModel>>
                {
                    Exito = 1,
                    Mensaje = "Áreas obtenidas correctamente",
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


        [HttpPost("ObtenerFechaCierre")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PresupuestosFechaCierreModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<PresupuestosFechaCierreModel>>>> ObtenerFechaCierre([FromBody] PresupuestosFechaCierreModel fechacierre)
        {
            try
            {
                var result = await _dbContext.Set<PresupuestosFechaCierreModel>()
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.sp_CombosPresupuestos @Func = {0}, @Año = {1}, @IdUser = {2}, @Fecha = {3}",
                                3, fechacierre.Año, fechacierre.IdUser, fechacierre.Fecha)
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Ok(new Respuesta<List<PresupuestosFechaCierreModel>>
                    {
                        Exito = 1,
                        Mensaje = "No se encontró Fecha Cierre.",
                        Data = new List<PresupuestosFechaCierreModel>()
                    });
                }

                return Ok(new Respuesta<List<PresupuestosFechaCierreModel>>
                {
                    Exito = 1,
                    Mensaje = "Fecha Cierre obtenida correctamente",
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

        [HttpPost("RegistrarFechaCierre")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PresupuestosFechaCierreModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> RegistrarFechaCierre([FromBody] PresupuestosFechaCierreModel fechacierre)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC PRESUPUESTO_ANUAL.sp_CombosPresupuestos @Func = {0}, @Año = {1}, @IdUser = {2}, @Fecha = {3}",
                    4, fechacierre.Año, fechacierre.IdUser, fechacierre.Fecha
                );

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Fecha Cierre registrada correctamente",
                    Data = null
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