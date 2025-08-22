
// Developer    : feragu  
// DateCreate   : 27/03/2025
// Description  : Controlador para Operaciones de Contabilidad
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Contabilidad;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;

namespace Contabilidad.Controllers
{
    [ApiExplorerSettings(GroupName = "Contabilidad")]
    [Route("api/Contabilidad/[controller]")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public OperacionController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/OperacionController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestOperacionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestOperacionModel>>> PostOperacion([FromBody] requestOperacionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre";
                List<OperacionDBModel> lista = await _dbContext.OperacionDB.FromSqlRaw(query,
                    new SqlParameter("@op", "2"),
                    new SqlParameter("@idope", (object?)model.IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel", (object?)model.Nivel ?? DBNull.Value),
                    new SqlParameter("@Operacion", (object?)model.Operacion ?? DBNull.Value),
                    new SqlParameter("@IdTipOpe", (object?)model.IdTipOpe ?? DBNull.Value),
                    new SqlParameter("@Mostrar", (object?)model.Mostrar ?? DBNull.Value),
                    new SqlParameter("@Obliga", (object?)model.Obliga ?? DBNull.Value),
                    new SqlParameter("@ProcesoInt", (object?)model.ProcesoInt ?? DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.IdUser ?? DBNull.Value),
                    new SqlParameter("@Elemento", (object?)model.Elemento ?? DBNull.Value),
                    new SqlParameter("@TipmonedaSol", (object?)model.TipmonedaSol ?? DBNull.Value),
                    new SqlParameter("@TipmonedaDol", (object?)model.TipmonedaDol ?? DBNull.Value),
                    new SqlParameter("@idopepadre", (object?)model.IdOpeP ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                    //).ToListAsync();
                //List<OperacionDBModel> producto = await _dbContext.OperacionDB.FromSqlRaw(storeOperacionController, model).ToListAsync();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<List<OperacionDBModel>>{ Exito = 1, Mensaje = "Success", Data = lista });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AccionOpexTipCtaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("AccionOpe")]
        public async Task<ActionResult<Respuesta<AccionOpexTipCtaModel>>> PostPlanCuentas([FromBody] requestAccionOpexTipCtaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre,@idtipcuenta,@operador";
                List<AccionOpexTipCtaModel> lista = await _dbContext.AccionOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", "9"),
                    new SqlParameter("@idope", (object?)model.IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel",  DBNull.Value),
                    new SqlParameter("@Operacion", DBNull.Value),
                    new SqlParameter("@IdTipOpe", DBNull.Value),
                    new SqlParameter("@Mostrar", DBNull.Value),
                    new SqlParameter("@Obliga", DBNull.Value),
                    new SqlParameter("@ProcesoInt", DBNull.Value),
                    new SqlParameter("@IdUser", DBNull.Value),
                    new SqlParameter("@Elemento", DBNull.Value),
                    new SqlParameter("@TipmonedaSol", DBNull.Value),
                    new SqlParameter("@TipmonedaDol", DBNull.Value),
                    new SqlParameter("@idopepadre", DBNull.Value),
                    new SqlParameter("@idtipcuenta", (object?)model.IdTipCta ?? DBNull.Value),
                    new SqlParameter("@operador", (object?)model.Operador ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                int band = 0;

                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<AccionOpexTipCtaModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else if (band == 2)
                {
                    return Ok(new Respuesta<List<AccionOpexTipCtaModel>>
                    {
                        Exito = 0,
                        Mensaje = "Errores de validacion",
                        Data = lista
                    });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<DocxOpeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("DocumentOpe")]
        public async Task<ActionResult<Respuesta<DocxOpeModel>>> PostPlanCuentas([FromBody] requestDocxOpeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre,@idtipcuenta,@operador,@iddoc,@activo,@autonumerar";
                List<DocxOpeModel> lista = await _dbContext.DocxOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", "12"),
                    new SqlParameter("@idope", (object?)model.IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel", DBNull.Value),
                    new SqlParameter("@Operacion", DBNull.Value),
                    new SqlParameter("@IdTipOpe", DBNull.Value),
                    new SqlParameter("@Mostrar", DBNull.Value),
                    new SqlParameter("@Obliga", (object?)model.Obliga?? DBNull.Value),
                    new SqlParameter("@ProcesoInt", DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.IdUser?? DBNull.Value),
                    new SqlParameter("@Elemento", DBNull.Value),
                    new SqlParameter("@TipmonedaSol", DBNull.Value),
                    new SqlParameter("@TipmonedaDol", DBNull.Value),
                    new SqlParameter("@idopepadre", DBNull.Value),
                    new SqlParameter("@idtipcuenta",  DBNull.Value),
                    new SqlParameter("@operador", DBNull.Value),
                    new SqlParameter("@iddoc", (object?)model.IdDoc ?? DBNull.Value),
                    new SqlParameter("@activo", "1"),
                    new SqlParameter("@autonumerar", (object?)model.Autogen ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                int band = 0;

                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<DocxOpeModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else if (band == 2)
                {
                    return Ok(new Respuesta<List<DocxOpeModel>>
                    {
                        Exito = 0,
                        Mensaje = "Errores de validacion",
                        Data = lista
                    });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpPut("DocumentOpe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<DocxOpeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutDocumentOpe([FromBody] requestDocxOpeModel model)
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre,@idtipcuenta,@operador,@iddoc,@activo,@autonumerar";
                List<DocxOpeModel> lista = await _dbContext.DocxOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", "13"),
                    new SqlParameter("@idope", (object?)model.IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel", DBNull.Value),
                    new SqlParameter("@Operacion", DBNull.Value),
                    new SqlParameter("@IdTipOpe", DBNull.Value),
                    new SqlParameter("@Mostrar", DBNull.Value),
                    new SqlParameter("@Obliga", (object?)model.Obliga ?? DBNull.Value),
                    new SqlParameter("@ProcesoInt", DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.IdUser ?? DBNull.Value),
                    new SqlParameter("@Elemento", DBNull.Value),
                    new SqlParameter("@TipmonedaSol", model.TipMoneda == "0" ? (object?)model.TipMoneda : DBNull.Value),
                    new SqlParameter("@TipmonedaDol", model.TipMoneda == "1" ? (object?)model.TipMoneda : DBNull.Value),
                    new SqlParameter("@idopepadre", DBNull.Value),
                    new SqlParameter("@idtipcuenta", DBNull.Value),
                    new SqlParameter("@operador", DBNull.Value),
                    new SqlParameter("@iddoc", (object?)model.IdDoc ?? DBNull.Value),
                    new SqlParameter("@activo", (object?)model.Activo ?? DBNull.Value),
                    new SqlParameter("@autonumerar",  DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                //).ToListAsync();
                //List<OperacionDBModel> producto = await _dbContext.OperacionDB.FromSqlRaw(storeOperacionController, model).ToListAsync();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<List<DocxOpeModel>> { Exito = 1, Mensaje = "Success", Data = lista });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        // PUT: api/OperacionController/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestOperacionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutOperacion([FromBody] requestOperacionModel model)
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre";
                List<OperacionDBModel> lista = await _dbContext.OperacionDB.FromSqlRaw(query,
                    new SqlParameter("@op", "3"),
                    new SqlParameter("@idope", (object?)model.IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel", (object?)model.Nivel ?? DBNull.Value),
                    new SqlParameter("@Operacion", (object?)model.Operacion ?? DBNull.Value),
                    new SqlParameter("@IdTipOpe", (object?)model.IdTipOpe ?? DBNull.Value),
                    new SqlParameter("@Mostrar", (object?)model.Mostrar ?? DBNull.Value),
                    new SqlParameter("@Obliga", (object?)model.Obliga ?? DBNull.Value),
                    new SqlParameter("@ProcesoInt", (object?)model.ProcesoInt ?? DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.IdUser ?? DBNull.Value),
                    new SqlParameter("@Elemento", (object?)model.Elemento ?? DBNull.Value),
                    new SqlParameter("@TipmonedaSol", (object?)model.TipmonedaSol ?? DBNull.Value),
                    new SqlParameter("@TipmonedaDol", (object?)model.TipmonedaDol ?? DBNull.Value),
                    new SqlParameter("@idopepadre", (object?)model.IdOpeP ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                //).ToListAsync();
                //List<OperacionDBModel> producto = await _dbContext.OperacionDB.FromSqlRaw(storeOperacionController, model).ToListAsync();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<List<OperacionDBModel>> { Exito = 1, Mensaje = "Success", Data = lista });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/OperacionController
        [HttpGet("TipOpe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TabTipOpeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<TabTipOpeModel>>> GetTipOperacionModel()
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op";
                List<TabTipOpeModel> lista = await _dbContext.TabTipOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", 5)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        [HttpGet("Documentos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<DocumentosModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<DocumentosModel>>> GetDocumentosModel()
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op";
                List<DocumentosModel> lista = await _dbContext.DocumentosDB.FromSqlRaw(query,
                    new SqlParameter("@op", 6)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpGet("TabTipCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<TabTipCuentaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<TabTipCuentaModel>>> GetTabTipCuentaModel()
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op";
                List<TabTipCuentaModel> lista = await _dbContext.TabTipCuentaDB.FromSqlRaw(query,
                    new SqlParameter("@op", 7)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpGet("GetOperacion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<OperacionDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<OperacionDBModel>>> GetOperacionModel(int nivel,string IdOpe="")
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel";
                List<OperacionDBModel> lista = await _dbContext.OperacionDB.FromSqlRaw(query,
                    new SqlParameter("@op", 1),
                    new SqlParameter("@idope", (object?)IdOpe ?? DBNull.Value),
                    new SqlParameter("@nivel", (object?)nivel ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        [HttpGet("GetAccionOpe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AccionOpexTipCtaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<AccionOpexTipCtaModel>>> GetAccionOpeModel(string IdOpe = "")
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope";
                List<AccionOpexTipCtaModel> lista = await _dbContext.AccionOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", 8),
                    new SqlParameter("@idope", (object?)IdOpe ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpGet("GetDocumentoOpe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<DocxOpeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<DocxOpeModel>>> GetDocumentoOpeModel(string IdOpe = "")
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope";
                List<DocxOpeModel> lista = await _dbContext.DocxOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", 11),
                    new SqlParameter("@idope", (object?)IdOpe ?? DBNull.Value)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/OperacionController/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestOperacionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteOperacion(int id)
        {
            try
            {                
                string storeOperacionController = "sp_OperacionController";
                var deleteResult = await _dbContext.OperacionDB.FromSqlRaw(storeOperacionController, id).ToListAsync();
                if (deleteResult.Count == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        [HttpDelete("AccionOpe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestOperacionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteAccionOpe(string IdOpe, string IdTipCta)
        {
            try
            {
                string query = "Contabilidad.sp_OperacionController @op,@idope,@nivel,@Operacion,@IdTipOpe,@Mostrar,@Obliga,@ProcesoInt,@IdUser,@Elemento,@TipmonedaSol,@TipmonedaDol,@idopepadre,@idtipcuenta,@operador";
                List<AccionOpexTipCtaModel> lista = await _dbContext.AccionOpeDB.FromSqlRaw(query,
                    new SqlParameter("@op", "10"),
                    new SqlParameter("@idope", IdOpe),
                    new SqlParameter("@nivel", DBNull.Value),
                    new SqlParameter("@Operacion", DBNull.Value),
                    new SqlParameter("@IdTipOpe", DBNull.Value),
                    new SqlParameter("@Mostrar", DBNull.Value),
                    new SqlParameter("@Obliga", DBNull.Value),
                    new SqlParameter("@ProcesoInt", DBNull.Value),
                    new SqlParameter("@IdUser", DBNull.Value),
                    new SqlParameter("@Elemento", DBNull.Value),
                    new SqlParameter("@TipmonedaSol", DBNull.Value),
                    new SqlParameter("@TipmonedaDol", DBNull.Value),
                    new SqlParameter("@idopepadre", DBNull.Value),
                    new SqlParameter("@idtipcuenta", IdTipCta),
                    new SqlParameter("@operador",  DBNull.Value)
                    ).AsNoTracking().ToListAsync();
                int band = 0;
                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<AccionOpexTipCtaModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

    }
}