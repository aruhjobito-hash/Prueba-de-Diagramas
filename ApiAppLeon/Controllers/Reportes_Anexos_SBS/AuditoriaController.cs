
// Developer    : VicVil  
// DateCreate   : 19/05/2025
// Description  : Controlador para Generar Auditoria de Reportes
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public AuditoriaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/AuditoriaController
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AuditoriaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAuditoriaModel>>> GetAuditoriaModel([FromQuery] requestAuditoriaModel model)
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
                    "Operacion","NombreTabla"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeAuditoriaController = $"REPORTES_ANEXOS_SBS.SP_AUDITARCONTROLLER {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<AuditoriaDBModel> producto = await _dbContext.AuditoriaDB.FromSqlRaw(storeAuditoriaController, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<AuditoriaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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


        [HttpGet("CodAnexo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AuditoriaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAuditoriaModel>>> GetCodAnexoModel([FromQuery] requestAuditoriaModel model)
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
                    "Operacion","NombreTabla","Codigo","cMes","cAnio"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeAuditoriaController = $"REPORTES_ANEXOS_SBS.SP_AUDITARCONTROLLER {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<CodAnexoDBModel> producto = await _dbContext.CodAnexoDB.FromSqlRaw(storeAuditoriaController, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<CodAnexoDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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


        // GET: api/AuditoriaController
        [HttpGet ("AuditoriaxCod")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AuditoriaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAuditoriaModel>>> GetAuditoriaxCodModel([FromQuery] requestAuditoriaModel model)
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
                    "Operacion","NombreTabla","Codigo"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeAuditoriaController = $"REPORTES_ANEXOS_SBS.SP_AUDITARCONTROLLER {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<AnexoAuditxCodDBModel> producto = await _dbContext.AnexoAuditxCodDB.FromSqlRaw(storeAuditoriaController, parameters).ToListAsync();

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<AnexoAuditxCodDBModel>> { Exito = 1, Mensaje = "Success", Data = producto });
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