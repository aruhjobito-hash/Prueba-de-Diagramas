
// Developer    : feragu  
// DateCreate   : 04/06/2025
// Description  : Controlador para control de Tipos de moneda
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Configuracion;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Configuracion.Controllers
{
    [ApiExplorerSettings(GroupName = "Configuracion")]
    [Route("api/Configuracion/[controller]")]
    [ApiController]
    public class TipMonedaController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public TipMonedaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/TipMoneda
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipMonedaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<TipMonedaDBModel>>> PostTipMoneda([FromBody] requestTipMonedaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdTipMoneda","Descripcion","Simbolo","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                         name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                             ? new SqlParameter("@Opt", 1)
                                             : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                     ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipMoneda = $"Configuracion.SP_TipMoneda {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipMonedaDBModel> producto = await _dbContext.TipMonedaDB.FromSqlRaw(storeTipMoneda, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipMonedaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // PUT: api/TipMoneda/{id}        
        [HttpPut("TipMoneda")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipMonedaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutTipMoneda( [FromBody] requestTipMonedaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdTipMoneda","Descripcion","Simbolo","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 2)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipMoneda = $"Configuracion.SP_TipMoneda {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipMonedaDBModel> producto = await _dbContext.TipMonedaDB.FromSqlRaw(storeTipMoneda, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipMonedaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpPut("EstTipMoneda")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipMonedaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutEstTipMoneda( [FromBody] requestTipMonedaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdTipMoneda","Descripcion","Simbolo","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 3)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipMoneda = $"Configuracion.SP_TipMoneda {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipMonedaDBModel> producto = await _dbContext.TipMonedaDB.FromSqlRaw(storeTipMoneda, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipMonedaDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/TipMoneda
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipMonedaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<TipMonedaDBModel>>> GetTipMonedaModel()
        {
            
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, 4)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipMoneda = $"Configuracion.SP_TipMoneda {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipMonedaDBModel> producto = await _dbContext.TipMonedaDB.FromSqlRaw(storeTipMoneda, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/TipMoneda/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipMonedaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteTipMoneda(string IdTipMoneda)
        {
            
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdTipMoneda","Descripcion","Simbolo","IdUser","Activo"
                };
                var model = new requestTipMonedaModel
                {
                    IdTipMoneda = IdTipMoneda,
                    Descripcion = string.Empty, // Asignar un valor por defecto o vacío
                    Simbolo = string.Empty, // Asignar un valor por defecto o vacío
                    IdUser = string.Empty, // Asignar un valor por defecto o vacío
                    Activo = "0" // Asignar un valor por defecto o vacío
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 5)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipMoneda = $"Configuracion.SP_TipMoneda {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipMonedaDBModel> producto = await _dbContext.TipMonedaDB.FromSqlRaw(storeTipMoneda, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipMonedaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

    }
}