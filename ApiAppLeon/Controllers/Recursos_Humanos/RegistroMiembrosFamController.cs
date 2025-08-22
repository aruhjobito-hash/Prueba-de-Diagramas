
// Developer    : JosAra  
// DateCreate   : 07/07/2025
// Description  : Registro de miembros familiares para trabajadores
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Recursos_Humanos;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Recursos_Humanos.Controllers
{
    [ApiExplorerSettings(GroupName = "Recursos_Humanos")]
    [Route("api/Recursos_Humanos/[controller]")]
    [ApiController]
    public class RegistroMiembrosFamController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public RegistroMiembrosFamController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/RegistroMiembrosFam
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroMiembrosFamDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestRegistroMiembrosFamModel>>> PostRegistroMiembrosFam([FromBody] List<requestRegistroMiembrosFamModel> model)
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
                    "IdPersonaTrab","IdPersonaFam","IdParentesco","IdUser","Apoderado","FecPro","hora","Estado","FecModificacion","IdUserModificacion"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeRegistroMiembrosFam = $"[PERSONAL].[RegistroMiembrosFamTrab] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroMiembrosFamDBModel> producto = await _dbContext.RegistroMiembrosFamDB.FromSqlRaw(storeRegistroMiembrosFam, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroMiembrosFamDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // PUT: api/RegistroMiembrosFam/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroMiembrosFamDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutRegistroMiembrosFam( [FromBody] requestRegistroMiembrosFamModel model)
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
                    "IdPersonaTrab","IdPersonaFam","IdParentesco","IdUser","Apoderado","FecPro","hora","Estado","FecModificacion","IdUserModificacion"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeRegistroMiembrosFam = $"[PERSONAL].[RegistroMiembrosFamTrab] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroMiembrosFamDBModel> producto = await _dbContext.RegistroMiembrosFamDB.FromSqlRaw(storeRegistroMiembrosFam, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroMiembrosFamDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // GET: api/RegistroMiembrosFam
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroMiembrosFamDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestRegistroMiembrosFamModel>>> GetRegistroMiembrosFamModel([FromQuery] string DNI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                //1.- Definimos los parametros que entrarán al store procedure
                var parameters = new[]
                {
                    new SqlParameter("@DNI", string.IsNullOrEmpty(DNI) ? DBNull.Value : (object)DNI)
                };
                //3.- Creamos el store procedure que será llamado
                string storeRegistroMiembrosFam = $"[PERSONAL].[RegistroMiembrosFamTrabListar] @DNI";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroMiembrosFamDBModel> producto = await _dbContext.RegistroMiembrosFamDB.FromSqlRaw(storeRegistroMiembrosFam, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroMiembrosFamDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // DELETE: api/RegistroMiembrosFam/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroMiembrosFamDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        private async Task<IActionResult> DeleteRegistroMiembrosFam(int id, [FromBody] requestRegistroMiembrosFamModel model)
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
                    "IdPersonaTrab","IdPersonaFam","IdParentesco","IdUser","Apoderado","FecPro","hora","Estado","FecModificacion","IdUserModificacion"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeRegistroMiembrosFam = $"[PERSONAL].[RegistroMiembrosFamTrab] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroMiembrosFamDBModel> producto = await _dbContext.RegistroMiembrosFamDB.FromSqlRaw(storeRegistroMiembrosFam, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroMiembrosFamDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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
        //[HttpPost("save")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        //public async Task<ActionResult<Respuesta<string>>> SaveMiembrosFam([FromBody] List<requestRegistroMiembrosFamModel> modelList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new Respuesta<ErrorTxA>
        //        {
        //            Exito = 0,
        //            Mensaje = "Model state validation failed",
        //            Data = new ErrorTxA { codigo = "01", Mensaje = "Invalid Model" }
        //        });
        //    }
        //    try
        //    {
        //        var entities = modelList.Select(m => new RegistroMiembrosFamTable
        //        {
        //            IdPersonaTrab = m.IdPersonaTrab,
        //            IdPersonaFam = m.IdPersonaFam,
        //            IdParentesco = m.IdParentesco,
        //            IdUser = m.IdUser,
        //            Apoderado = m.Apoderado,
        //            FecPro = m.FecPro,
        //            hora = m.hora,
        //            Estado = m.Estado,
        //            FecModificacion = m.FecModificacion,
        //            IdUserModificacion = m.IdUserModificacion
        //        }).ToList();

        //        await _dbContext.RegistroMiembrosFamTable.AddRangeAsync(entities);
        //        await _dbContext.SaveChangesAsync();

        //        return Ok(new Respuesta<List<requestRegistroMiembrosFamModel>>
        //        {
        //            Exito = 1,
        //            Mensaje = "Registros insertados correctamente",
        //            Data = modelList
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new Respuesta<ErrorTxA>
        //        {
        //            Exito = 0,
        //            Mensaje = ex.Message,
        //            Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" }
        //        });
        //    }
        //}
        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<Respuesta<string>>> SaveMiembrosFam([FromBody] List<requestRegistroMiembrosFamModel> modelList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Model state validation failed",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Invalid Model" }
                });
            }

            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var m in modelList)
                {
                    var existing = await _dbContext.RegistroMiembrosFamTable
                        .FirstOrDefaultAsync(x => x.IdPersonaTrab == m.IdPersonaTrab && x.IdPersonaFam == m.IdPersonaFam);

                    if (existing != null)
                    {
                        // Update
                        existing.IdParentesco = m.IdParentesco;
                        existing.IdUser = m.IdUser;
                        existing.Apoderado = m.Apoderado;
                        existing.Estado = m.Estado;
                        existing.FecModificacion = m.FecModificacion;
                        existing.IdUserModificacion = m.IdUserModificacion;

                        _dbContext.Update(existing);
                    }
                    else
                    {
                        // Insert
                        var newEntity = new RegistroMiembrosFamTable
                        {
                            IdPersonaTrab = m.IdPersonaTrab,
                            IdPersonaFam = m.IdPersonaFam,
                            IdParentesco = m.IdParentesco,
                            IdUser = m.IdUser,
                            Apoderado = m.Apoderado,
                            FecPro = m.FecPro,
                            hora = m.hora,
                            Estado = m.Estado,
                            FecModificacion = m.FecModificacion,
                            IdUserModificacion = m.IdUserModificacion
                        };

                        await _dbContext.RegistroMiembrosFamTable.AddAsync(newEntity);
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new Respuesta<string>
                {
                    Exito = 1,
                    Mensaje = "Registros insertados o actualizados correctamente",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

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