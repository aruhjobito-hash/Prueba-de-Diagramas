
// Developer    : feragu  
// DateCreate   : 26/02/2025
// Description  : Controlador para parametros generelares
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using ApiAppLeon.Models.Configuracion;

namespace ApiAppLeon.Controllers.Configuracion
{
    [ApiExplorerSettings(GroupName = "Configuracion")]
    [Route("api/Configuracion/[controller]")]
    [ApiController]
    public class ListaControllerController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public ListaControllerController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //********************************************************GETs***********************************
        // GET: api/ListaController
        //[HttpGet("ListaController/{X-profile-Token}")]
        [HttpGet("ListaCuali")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestListaModel>>> GetListaModelCuali()
        {
            try
            {
                string storeListaController = "CONFIGURACION.sp_Lista_ListaDetalleCuali @op='1'";
                List<ListaDBModel> lista = new List<ListaDBModel>();
                lista = await _dbContext.ListaDB.FromSqlRaw(storeListaController).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/ListaController
        //[HttpGet("ListaController/{X-profile-Token}")]
        [HttpGet("ListaDetalleCuali")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuali>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestListaDetalleModelCuali>>> GetListaDetalleModelCuali(int id)
        {
            try
            {
                string storeListaController = "CONFIGURACION.sp_Lista_ListaDetalleCuali @op='2' , " +
                                               "@idlista='" + id + "'";
                List<ListaDetalleModelCuali> lista = new List<ListaDetalleModelCuali>();
                lista = await _dbContext.ListaDetalleCualiDB.FromSqlRaw(storeListaController).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        //****************************************************FIN GETs********************************************



        // POST: api/ListaController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("CreListaCuali")]
        public async Task<ActionResult<Respuesta<requestListaModel>>> PostLista([FromBody] requestListaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string storeListaController = "CONFIGURACION.sp_Lista_ListaDetalleCuali @op='3' , " +
                                               "@descripcion='" + model.cDescripcionLista + "', " +
                                                "@bestadoLista='" + model.bEstado + "'";
                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(storeListaController).ToListAsync();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<ListaDBModel>{ Exito = 1, Mensaje = "Success" });
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

        // POST: api/ListaController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuali>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("CreListaDetallePost")]
        public async Task<ActionResult<Respuesta<requestListaDetalleModelCuali>>> PostListaDetalle([FromBody] requestListaDetalleModelCuali model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                


                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuali @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor, @bEstadoListaDetalle,@IdUser";

                List<ListaDetalleModelCuali> lista = await _dbContext.ListaDetalleCualiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "4"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value),
                    new SqlParameter("@idListaDetalle", DBNull.Value),
                    new SqlParameter("@CodDetalle", (object?)model.CodDetalle ?? DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", (object?)model.cDescripcionDetalle ?? DBNull.Value),
                    new SqlParameter("@valor", (object?)model.cValor ?? DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@IdUser", (object?)model.iduser ?? DBNull.Value)


                ).ToListAsync();


                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<ListaDetalleModelCuali> { Exito = 1, Mensaje = "Success" });
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

        // PUT: api/ListaController/{id}        
        //[HttpPut("ListaController/{X-profile-Token}")]
        [HttpPut("UpdLista")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutLista(int id, [FromBody] requestListaModel model)
        {
            if (id != model.IdLista)
            {
                return BadRequest();
            }

            try
            {
                string query = "CONFIGURACION.sp_Lista_ListaDetalleCuali @op,@descripcion,@bestadoLista, @idlista ";

                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(query,
                    new SqlParameter("@op", "5"),
                    new SqlParameter("@descripcion", (object?)model.cDescripcionLista ?? DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value)

                ).ToListAsync();

                //var updateResult = await _dbContext.ListaDB.FromSqlRaw(storeListaController, model).ToListAsync();
                if (lista.Count == 0)
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

        [HttpPut("UpdListaDetalleCuali")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuali>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutListaDetalle(int id, [FromBody] requestListaDetalleModelCuali model)
        {
            if (id != model.IdListaDetalle)
            {
                return BadRequest();
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuali @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor, @bEstadoListaDetalle,@IdUser";

                List<ListaDetalleModelCuali> lista = await _dbContext.ListaDetalleCualiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "6"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value),
                    new SqlParameter("@idListaDetalle", (object?)model.IdListaDetalle ?? DBNull.Value),
                    new SqlParameter("@CodDetalle", (object?)model.CodDetalle ?? DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", (object?)model.cDescripcionDetalle ?? DBNull.Value),
                    new SqlParameter("@valor", (object?)model.cValor ?? DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", (object?)model.bEstado ?? DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.iduser ?? DBNull.Value)
                   

                ).ToListAsync();

                //var updateResult = await _dbContext.ListaDB.FromSqlRaw(storeListaController, model).ToListAsync();
                if (lista.Count == 0)
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


        // DELETE: api/ListaController/{id}
        //[HttpDelete("ListaController/{X-profile-Token}")]
        [HttpDelete("DelLista")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteLista(int id)
        {
            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuali @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor, @bEstadoListaDetalle,@IdUser";

                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(query,
                    new SqlParameter("@op", "7"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", id),
                    new SqlParameter("@idListaDetalle", DBNull.Value),
                    new SqlParameter("@CodDetalle", DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", DBNull.Value),
                    new SqlParameter("@valor", DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@IdUser",  DBNull.Value)


                ).ToListAsync();
                if (lista.Count == 0)
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


        [HttpDelete("DelListaDetalleCuali")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuali>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteListaDetalle(int id)
        {
            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuali @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor, @bEstadoListaDetalle,@IdUser";

                List<ListaDetalleModelCuali> lista = await _dbContext.ListaDetalleCualiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "8"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", DBNull.Value),
                    new SqlParameter("@idListaDetalle",id),
                    new SqlParameter("@CodDetalle", DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", DBNull.Value),
                    new SqlParameter("@valor", DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@IdUser", DBNull.Value)


                ).ToListAsync();
                if (lista.Count == 0)
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

        ///*********************************************************************************************************************************************************
        [HttpGet("ListaCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestListaModel>>> GetListaModelCuanti()
        {
            try
            {
                string storeListaController = "CONFIGURACION.sp_Lista_ListaDetalleCuanti @op='1'";
                List<ListaDBModel> lista = new List<ListaDBModel>();
                lista = await _dbContext.ListaDB.FromSqlRaw(storeListaController).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/ListaController
        //[HttpGet("ListaController/{X-profile-Token}")]
        [HttpGet("ListaDetalleCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuanti>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestListaDetalleModelCuanti>>> GetListaDetalleModelCuanti(int id)
        {
            try
            {
                string storeListaController = "exec CONFIGURACION.sp_Lista_ListaDetalleCuanti @op='2' , " +
                                               "@idlista='" + id + "'";
                List<ListaDetalleModelCuanti> lista = new List<ListaDetalleModelCuanti>();
                lista = await _dbContext.ListaDetalleCuantiDB.FromSqlRaw(storeListaController).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        //****************************************************FIN GETs********************************************



        // POST: api/ListaController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("CreListaCuanti")]
        public async Task<ActionResult<Respuesta<requestListaModel>>> PostListaCuanti([FromBody] requestListaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                string storeListaController = "CONFIGURACION.sp_Lista_ListaDetalleCuanti @op='3' , " +
                                               "@descripcion='" + model.cDescripcionLista + "', " +
                                                "@bestadoLista='" + model.bEstado + "'";
                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(storeListaController).ToListAsync();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<ListaDBModel> { Exito = 1, Mensaje = "Success" });
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

        // POST: api/ListaController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuanti>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost("CreListaDetalleCuantiPost")]
        public async Task<ActionResult<Respuesta<requestListaDetalleModelCuanti>>> PostListaDetalleCuanti([FromBody] requestListaDetalleModelCuanti model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {



                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuanti @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor1,@valor2,@valor3, @bEstadoListaDetalle,@idagencia,@tipmoneda,@fecIni,@fecFin,@IdUser";

                List<ListaDetalleModelCuanti> lista = await _dbContext.ListaDetalleCuantiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "4"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value),
                    new SqlParameter("@idListaDetalle", DBNull.Value),
                    new SqlParameter("@CodDetalle", (object?)model.CodDetalle ?? DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", (object?)model.cDescripcionDetalle ?? DBNull.Value),
                    new SqlParameter("@valor1", (object?)model.cValor1 ?? DBNull.Value),
                    new SqlParameter("@valor2", (object?)model.cValor2 ?? DBNull.Value),
                    new SqlParameter("@valor3", (object?)model.cValor3 ?? DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@idagencia", (object?)model.idagencia ?? DBNull.Value),
                    new SqlParameter("@tipmoneda", (object?)model.tipmoneda ?? DBNull.Value),
                    new SqlParameter("@fecIni", (object?)model.FecIni ?? DBNull.Value),
                    new SqlParameter("@fecFin", (object?)model.FecFin ?? DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.iduser ?? DBNull.Value)


                ).ToListAsync();


                if (lista != null && lista.Count > 0)
                {
                    return Ok(new Respuesta<ListaDetalleModelCuanti> { Exito = 1, Mensaje = "Success" });
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

        // PUT: api/ListaController/{id}        
        //[HttpPut("ListaController/{X-profile-Token}")]
        [HttpPut("UpdListaCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutListaCuanti(int id, [FromBody] requestListaModel model)
        {
            if (id != model.IdLista)
            {
                return BadRequest();
            }

            try
            {
                string query = "CONFIGURACION.sp_Lista_ListaDetalleCuanti @op,@descripcion,@bestadoLista, @idlista ";

                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(query,
                    new SqlParameter("@op", "5"),
                    new SqlParameter("@descripcion", (object?)model.cDescripcionLista ?? DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value)

                ).ToListAsync();

                //var updateResult = await _dbContext.ListaDB.FromSqlRaw(storeListaController, model).ToListAsync();
                if (lista.Count == 0)
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

        [HttpPut("UpdListaDetalleCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuanti>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutListaDetalleCuanti(int id, [FromBody] requestListaDetalleModelCuanti model)
        {
            if (id != model.IdListaDetalle)
            {
                return BadRequest();
            }

            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuanti @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor1,@valor2,@valor3, @bEstadoListaDetalle,@idagencia,@tipmoneda,@fecIni,@fecFin,@IdUser";

                List<ListaDetalleModelCuanti> lista = await _dbContext.ListaDetalleCuantiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "6"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", (object?)model.IdLista ?? DBNull.Value),
                    new SqlParameter("@idListaDetalle", (object?)model.IdListaDetalle ?? DBNull.Value),
                    new SqlParameter("@CodDetalle", (object?)model.CodDetalle ?? DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", (object?)model.cDescripcionDetalle ?? DBNull.Value),
                    new SqlParameter("@valor1", (object?)model.cValor1 ?? DBNull.Value),
                    new SqlParameter("@valor2", (object?)model.cValor2 ?? DBNull.Value),
                    new SqlParameter("@valor3", (object?)model.cValor3 ?? DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@idagencia", (object?)model.idagencia ?? DBNull.Value),
                    new SqlParameter("@tipmoneda", (object?)model.tipmoneda ?? DBNull.Value),
                    new SqlParameter("@fecIni", (object?)model.FecIni ?? DBNull.Value),
                    new SqlParameter("@fecFin", (object?)model.FecFin ?? DBNull.Value),
                    new SqlParameter("@IdUser", (object?)model.iduser ?? DBNull.Value)


                ).ToListAsync();

                //var updateResult = await _dbContext.ListaDB.FromSqlRaw(storeListaController, model).ToListAsync();
                if (lista.Count == 0)
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


        // DELETE: api/ListaController/{id}
        //[HttpDelete("ListaController/{X-profile-Token}")]
        [HttpDelete("DelListaCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteListaCuanti(int id)
        {
            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuanti @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor, @bEstadoListaDetalle,@IdUser";

                List<ListaDBModel> lista = await _dbContext.ListaDB.FromSqlRaw(query,
                    new SqlParameter("@op", "7"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", id),
                    new SqlParameter("@idListaDetalle", DBNull.Value),
                    new SqlParameter("@CodDetalle", DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", DBNull.Value),
                    new SqlParameter("@valor", DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", "1"),
                    new SqlParameter("@IdUser", DBNull.Value)


                ).ToListAsync();
                if (lista.Count == 0)
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


        [HttpDelete("DelListaDetalleCuanti")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestListaDetalleModelCuanti>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteListaDetalleCuanti(int id)
        {
            try
            {
                string query = "EXEC CONFIGURACION.sp_Lista_ListaDetalleCuanti @op, @descripcion, @bestadoLista, @idlista, " +
               "@idListaDetalle, @CodDetalle, @cDescripcionDetalle, @valor1,@valor2,@valor3, @bEstadoListaDetalle,@idagencia,@tipmoneda,@fecIni,@fecFin,@IdUser";

                List<ListaDetalleModelCuanti> lista = await _dbContext.ListaDetalleCuantiDB.FromSqlRaw(query,
                    new SqlParameter("@op", "8"),
                    new SqlParameter("@descripcion", DBNull.Value),
                    new SqlParameter("@bestadoLista", DBNull.Value),
                    new SqlParameter("@idlista", DBNull.Value),
                    new SqlParameter("@idListaDetalle", id),
                    new SqlParameter("@CodDetalle",  DBNull.Value),
                    new SqlParameter("@cDescripcionDetalle", DBNull.Value),
                    new SqlParameter("@valor1", DBNull.Value),
                    new SqlParameter("@valor2", DBNull.Value),
                    new SqlParameter("@valor3", DBNull.Value),
                    new SqlParameter("@bEstadoListaDetalle", DBNull.Value),
                    new SqlParameter("@idagencia", DBNull.Value),
                    new SqlParameter("@tipmoneda", DBNull.Value),
                    new SqlParameter("@fecIni", DBNull.Value),
                    new SqlParameter("@fecFin", DBNull.Value),
                    new SqlParameter("@IdUser", DBNull.Value)


                ).ToListAsync();
                if (lista.Count == 0)
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
    }
}