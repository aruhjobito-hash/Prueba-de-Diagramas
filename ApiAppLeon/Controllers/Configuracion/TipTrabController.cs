
// Developer    : feragu  
// DateCreate   : 04/06/2025
// Description  : Controlador para control de Tipos de trabajadores
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
    public class TipTrabController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public TipTrabController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/TipTrab
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipTrabDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<TipTrabDBModel>>> PostTipTrab([FromBody] requestTipTrabModel model)
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
                    "opt","IdTipTrab","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                         name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                             ? new SqlParameter("@Opt", 1)
                                             : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                     ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipTrab = $"Configuracion.SP_TipTrab {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipTrabDBModel> producto = await _dbContext.TipTrabDB.FromSqlRaw(storeTipTrab, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipTrabDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // PUT: api/TipTrab/{id}        
        [HttpPut("TipTrab")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipTrabDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutTipTrab([FromBody] requestTipTrabModel model)
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
                    "opt","IdTipTrab","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 2)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipTrab = $"Configuracion.SP_TipTrab {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipTrabDBModel> producto = await _dbContext.TipTrabDB.FromSqlRaw(storeTipTrab, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipTrabDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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
        [HttpPut("EstTipTrab")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipTrabDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutEstTipTrab([FromBody] requestTipTrabModel model)
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
                    "opt","IdTipTrab","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 3)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeTipTrab = $"Configuracion.SP_TipTrab {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipTrabDBModel> producto = await _dbContext.TipTrabDB.FromSqlRaw(storeTipTrab, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipTrabDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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

        // GET: api/TipTrab
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipTrabDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<TipTrabDBModel>>> GetTipTrabModel( )
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
                string storeTipTrab = $"Configuracion.SP_TipTrab {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipTrabDBModel> producto = await _dbContext.TipTrabDB.FromSqlRaw(storeTipTrab, parameters).ToListAsync();
                
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

        // DELETE: api/TipTrab/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TipTrabDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteTipTrab(string IdTipTrab)
        {
            
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdTipTrab","Descripcion","IdUser","Activo"
                };
                var model = new requestTipTrabModel
                {
                    IdTipTrab = IdTipTrab,
                    Descripcion = string.Empty, // Asignar un valor por defecto o vacío
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
                string storeTipTrab = $"Configuracion.SP_TipTrab {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<TipTrabDBModel> producto = await _dbContext.TipTrabDB.FromSqlRaw(storeTipTrab, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<TipTrabDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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