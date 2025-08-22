
// Developer    : feragu  
// DateCreate   : 09/06/2025
// Description  : Controlador para control de niveles de trabajadores
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
    public class NivelCategPersController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public NivelCategPersController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/NivelCategPers
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<NivelPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("NivelPers")]
        public async Task<ActionResult<Respuesta<NivelPersDBModel>>> PostNivelPers([FromBody] requestNivelPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                         name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                             ? new SqlParameter("@Opt", 1)
                                             : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                     ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<NivelPersDBModel> producto = await _dbContext.NivelPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<NivelPersDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // PUT: api/NivelCategPers/{id}        
        [HttpPut("NivelPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<NivelPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutNivelPers([FromBody] requestNivelPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 2)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<NivelPersDBModel> producto = await _dbContext.NivelPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<NivelPersDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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
        [HttpPut("EstNivelPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<NivelPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutEstNivelPers([FromBody] requestNivelPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 3)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<NivelPersDBModel> producto = await _dbContext.NivelPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<NivelPersDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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

        // GET: api/NivelCategPers
        [HttpGet("NivelPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<NivelPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<NivelPersDBModel>>> GetNivelPersModel()
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
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<NivelPersDBModel> producto = await _dbContext.NivelPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok( producto );
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

        // DELETE: api/NivelCategPers/{id}
        [HttpDelete("NivelPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<NivelPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteNivelPers(string IdNivelPers)
        {
            
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                var model = new requestNivelPersModel
                {
                    IdNivelPers = IdNivelPers,
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
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<NivelPersDBModel> producto = await _dbContext.NivelPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<NivelPersDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        //-------------------------------------------------------------------------------------------------
        // POST: api/NivelCategPers
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CategPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("CategPers")]
        public async Task<ActionResult<Respuesta<CategPersDBModel>>> PostCategPers([FromBody] requestCategPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                         name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                             ? new SqlParameter("@Opt", 6)
                                             : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                     ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CategPersDBModel> producto = await _dbContext.CategPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<CategPersDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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

        // PUT: api/NivelCategPers/{id}        
        [HttpPut("CategPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CategPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutCategPers([FromBody] requestCategPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 7)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CategPersDBModel> producto = await _dbContext.CategPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<CategPersDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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
        [HttpPut("EstCategPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CategPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutEstCategPers([FromBody] requestCategPersModel model)
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
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 8)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CategPersDBModel> producto = await _dbContext.CategPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<CategPersDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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

        // GET: api/NivelCategPers
        [HttpGet("CategPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CategPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<CategPersDBModel>>> GetCategPersModel(string IdNivelPers)
        {

            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                var model = new requestCategPersModel
                {
                    IdCategoriaPers = string.Empty,
                    IdNivelPers = IdNivelPers,
                    Descripcion = string.Empty, // Asignar un valor por defecto o vacío
                    IdUser = string.Empty, // Asignar un valor por defecto o vacío
                    Activo = "0" // Asignar un valor por defecto o vacío
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                //var parameters = parameterNames.Select(name => new SqlParameter("@" + name, 9)).ToArray();
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 9)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CategPersDBModel> producto = await _dbContext.CategPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

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

        // DELETE: api/NivelCategPers/{id}
        [HttpDelete("CategPers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<CategPersDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteCategPers(string IdCategoriaPers)
        {

            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "opt","IdNivelPers","IdCategoriaPers","Descripcion","IdUser","Activo"
                };
                var model = new requestCategPersModel
                {
                    IdCategoriaPers=IdCategoriaPers,
                    IdNivelPers = "00",
                    Descripcion = string.Empty, // Asignar un valor por defecto o vacío
                    IdUser = string.Empty, // Asignar un valor por defecto o vacío
                    Activo = "0" // Asignar un valor por defecto o vacío
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name =>
                                        name.Equals("Opt", StringComparison.OrdinalIgnoreCase)
                                            ? new SqlParameter("@Opt", 10)
                                            : new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)
                                    ).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeNivelCategPers = $"Configuracion.SP_NivelCategoria {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CategPersDBModel> producto = await _dbContext.CategPersDB.FromSqlRaw(storeNivelCategPers, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<CategPersDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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