
// Developer    : migzav  
// DateCreate   : 05/05/2025
// Description  : Controlador Mantenedor Presupuestos de Ingresos y Egresos
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Planeamiento;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Planeamiento.Controllers
{
    [ApiExplorerSettings(GroupName = "Planeamiento")]
    [Route("api/Planeamiento/[controller]")]
    [ApiController]
    public class PresupuestosIngresosEgresosController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PresupuestosIngresosEgresosController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/PresupuestosIngresosEgresos
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CreatePresupuestosIngresosEgresosDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("RegistrarPresupuesto")]

        public async Task<ActionResult<Respuesta<string>>> RegistrarPresupuesto([FromBody] CreatePresupuestosIngresosEgresosDBModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Año) || string.IsNullOrWhiteSpace(model.IdUser))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros Año, Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 1);
                var param2 = new SqlParameter("@IdAgencia", model.IdAgencia);
                var param3 = new SqlParameter("@IdArea", model.IdArea);
                var param4 = new SqlParameter("@Tipo", model.Tipo);
                var param5 = new SqlParameter("@Año", model.Año);
                var param6 = new SqlParameter("@CtaContable", model.CtaContable);
                var param7 = new SqlParameter("@CtaNombre", model.CtaNombre);
                var param8 = new SqlParameter("@Enero", model.Enero);
                var param9 = new SqlParameter("@Febrero", model.Febrero);
                var param10 = new SqlParameter("@Marzo", model.Marzo);
                var param11 = new SqlParameter("@Abril", model.Abril);
                var param12 = new SqlParameter("@Mayo", model.Mayo);
                var param13 = new SqlParameter("@Junio", model.Junio);
                var param14 = new SqlParameter("@Julio", model.Julio);
                var param15 = new SqlParameter("@Agosto", model.Agosto);
                var param16 = new SqlParameter("@Septiembre", model.Septiembre);
                var param17 = new SqlParameter("@Octubre", model.Octubre);
                var param18 = new SqlParameter("@Noviembre", model.Noviembre);
                var param19 = new SqlParameter("@Diciembre", model.Diciembre);
                var param20 = new SqlParameter("@IdUser", model.IdUser);




                string storeProcedure = "EXEC PRESUPUESTO_ANUAL.sp_RegistrarPresupuestoEgresosIngresos @TipoOperacion = {0}, @IdAgencia= {1}, @IdArea= {2}, @Tipo= {3}, @Año= {4}, @CtaContable= {5}, @CtaNombre= {6}, @Enero= {7}, @Febrero= {8}, @Marzo= {9}, @Abril= {10}, @Mayo= {11}, @Junio= {12}, @Julio= {13}, @Agosto= {14}, @Septiembre= {15}, @Octubre= {16}, @Noviembre= {17}, @Diciembre= {18}, @IdUser= {19}";


                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18, param19, param20);

                if (result == 0)
                {
                    return NotFound(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "No se pudo registrar presupuesto"
                    });
                }

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Presupuesto registrado correctamente."
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

        [HttpPut("DeletePresupuesto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> DeletePresupuesto([FromBody] UpdatePresupuestosIngresosEgresosDBModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Año) || string.IsNullOrWhiteSpace(model.IdUser))
                {
                    return BadRequest(new Respuesta<string>
                    {
                        Exito = 0,
                        Mensaje = "Los parámetros Año y Usuario son obligatorios."
                    });
                }

                var param1 = new SqlParameter("@TipoOperacion", 2);
                var param2 = new SqlParameter("@IdAgencia", model.IdAgencia);
                var param3 = new SqlParameter("@IdArea", model.IdArea);
                var param4 = new SqlParameter("@Año", model.Año);
                var param5 = new SqlParameter("@Tipo", model.Tipo);
                var param6 = new SqlParameter("@IdUser", model.IdUser);


                string storeProcedure = "EXEC PRESUPUESTO_ANUAL.sp_RegistrarPresupuestoEgresosIngresos @TipoOperacion = {0},@IdAgencia = {1}, @IdArea = {2}, @Año = {3}, @Tipo = {4}, @IdUser = {5}";

                int result = await _dbContext.Database.ExecuteSqlRawAsync(storeProcedure, param1, param2, param3, param4, param5, param6);

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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<List<PresupuestosIngresosEgresosDBModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<List<PresupuestosIngresosEgresosDBModel>>>> GetPresupuestosModel([FromQuery] requestPresupuestosIngresosEgresosModel filtro)
        {
            try
            {
                var result = await _dbContext.PresupuestosIngresosEgresosDB
                    .FromSqlRaw("exec PRESUPUESTO_ANUAL.sp_BuscarPresupuestoPlantilla @Año={0}, @idagencia={1}, @tipo={2}, @idarea={3}, @idrec={4}",
                        filtro.Año, filtro.IdAgencia, filtro.Tipo, filtro.IdArea, filtro.IdRec)
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return Ok(new Respuesta<List<PresupuestosIngresosEgresosDBModel>>
                    {
                        Exito = 0,
                        Mensaje = "No data found",
                        Data = result
                    });
                }

                return Ok(new Respuesta<List<PresupuestosIngresosEgresosDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Consulta exitosa",
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