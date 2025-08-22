
// Developer    : feragu  
// DateCreate   : 27/05/2025
// Description  : Controlador para control de Pensiones
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
    public class PensionController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PensionController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Pension
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PensionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<PensionDBModel>>> PostPension([FromBody] requestPensionModel model)
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
                    "opt","IdPension","PensNombre","MontoCV","MontoAP","MontoSPS","MontoSNP","MontoMaxSPS","MontoCM","PensInicio","IdUser","PensActivo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 1)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePension = $"Configuracion.SP_Pension {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PensionDBModel> producto = await _dbContext.PensionDB.FromSqlRaw(storePension, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PensionDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // PUT: api/Pension/{id}        
        [HttpPut("Pension")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PensionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutPension([FromBody] requestPensionModel model)
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
                     "opt","IdPension","PensNombre","MontoCV","MontoAP","MontoSPS","MontoSNP","MontoMaxSPS","MontoCM","PensInicio","IdUser","PensActivo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                       name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                           ? new SqlParameter("@Opt", 2)
                                           : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                   ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePension = $"Configuracion.SP_Pension {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PensionDBModel> producto = await _dbContext.PensionDB.FromSqlRaw(storePension, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PensionDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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
        [HttpPut("EstPension")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PensionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutEstPension([FromBody] requestPensionModel model)
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
                     "opt","IdPension","PensNombre","MontoCV","MontoAP","MontoSPS","MontoSNP","MontoMaxSPS","MontoCM","PensInicio","IdUser","PensActivo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                       name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                           ? new SqlParameter("@Opt", 3)
                                           : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                   ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePension = $"Configuracion.SP_Pension {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PensionDBModel> producto = await _dbContext.PensionDB.FromSqlRaw(storePension, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PensionDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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

        // GET: api/Pension
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PensionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<PensionDBModel>>> GetPensionModel()
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
                string storePension = $"Configuracion.SP_Pension {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PensionDBModel> producto = await _dbContext.PensionDB.FromSqlRaw(storePension, parameters).ToListAsync();
                
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

        // DELETE: api/Pension/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PensionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeletePension(string idpension)
        {
           
            try
            {                
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdPension","PensNombre","MontoCV","MontoAP","MontoSPS","MontoSNP","MontoMaxSPS","MontoCM","PensInicio","IdUser","PensActivo"
                };
                var model = new requestPensionModel{
                    IdPension=idpension,
                    PensNombre = string.Empty,
                    MontoCV = 0,
                    MontoAP = 0,
                    MontoSPS = 0,
                    MontoSNP = 0,
                    MontoMaxSPS = 0,
                    MontoCM = 0,
                    PensActivo = "N",
                    PensInicio = DateTime.Now,
                    IdUser = "000000"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 5)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePension = $"Configuracion.SP_Pension {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PensionDBModel> producto = await _dbContext.PensionDB.FromSqlRaw(storePension, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PensionDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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