
// Developer    : JosAra  
// DateCreate   : 12/08/2025
// Description  : Endpoints para listar y obtener planillas
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
    public class PLanillaBoletaController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PLanillaBoletaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/PLanillaBoleta
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PlanillaBoletaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestPlanillaBoletaModel>>> PostPlanillaBoleta([FromBody] requestPlanillaBoletaModel model)
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
                    "Id"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePLanillaBoleta = $"[Planillas].[ImprimirPLanillas] {string.Join(" ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<ImprimeBoletaDBModel> producto = await _dbContext.PlanillaListarPlanilla.FromSqlRaw(storePLanillaBoleta, parameters).ToListAsync();
                List<ImprimeBolDBModel> listaFinal = new ();
                if (producto != null && producto.Count > 0)
                {
                    ImprimeBolDBModel imprimeBolDBModel = new ImprimeBolDBModel();
                    imprimeBolDBModel.ListaConceptos = new List<ListaConceptos>();
                    ListaConceptos Conceptos = new ListaConceptos();
                    List<ListaConceptos> listaConceptos = new ();
                    foreach (var item in producto)
                    {                        
                        if (item.Id == 1)
                        {
                            imprimeBolDBModel =new ImprimeBolDBModel(); // Reiniciar el modelo para cada nuevo registro
                            imprimeBolDBModel.Nombres = item.Nombres;
                            imprimeBolDBModel.Cargo = item.Cargo;
                            imprimeBolDBModel.Situacion = item.Situacion;
                            imprimeBolDBModel.cPensNombre = item.cPensNombre;
                            imprimeBolDBModel.DocumentoIdentidad = item.DocumentoIdentidad;
                            imprimeBolDBModel.fechaIngreso = item.fechaIngreso;
                            imprimeBolDBModel.Categoria = item.Categoria;
                            imprimeBolDBModel.ctrabPensCUSSP = item.ctrabPensCUSSP;
                            imprimeBolDBModel.DiasLaborados = item.DiasLaborados;
                            imprimeBolDBModel.TotalIngresos = item.TotalIngresos;
                            imprimeBolDBModel.TotalDscto = item.TotalDscto;
                            imprimeBolDBModel.TotalAPortes = item.TotalAPortes;
                            imprimeBolDBModel.NetoPagar = item.NetoPagar;
                            imprimeBolDBModel.Correo=item.Correo;
                            if (imprimeBolDBModel!=null)
                            {
                              imprimeBolDBModel.ListaConceptos = listaConceptos;
                              listaFinal.Add(imprimeBolDBModel); // Agregar el modelo a la lista final
                              listaConceptos.Clear(); // Limpiar la lista de conceptos para el siguiente registro                             
                            }                            
                        }
                        Conceptos=new ListaConceptos();// Reiniciar el modelo para cada nuevo concepto
                        Conceptos.cConNombre=item.cConNombre;
                        Conceptos.nplamenconmonto=item.nplamenconmonto;
                        Conceptos.tipo=item.tipo;
                        listaConceptos.Add(Conceptos);
                    }
                    return Ok(new Respuesta<List<ImprimeBolDBModel>>{ Exito = 1, Mensaje = "Success",Data = listaFinal });
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

        // PUT: api/PLanillaBoleta/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PlanillaBoletaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        private async Task<IActionResult> PutPlanillaBoleta(int id, [FromBody] requestPlanillaBoletaModel model)
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
                    "iplamenid","tipo","cplamenperiodo","ANIO","MES"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePLanillaBoleta = $"PlanillaBoleta {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<PlanillaBoletaDBModel> producto = await _dbContext.PlanillaBoletaDB.FromSqlRaw(storePLanillaBoleta, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PlanillaBoletaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // GET: api/PLanillaBoleta
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PlanillaBoletaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestPlanillaBoletaModel>>> GetPlanillaBoletaModel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                
                //3.- Creamos el store procedure que será llamado
                string storePLanillaBoleta = $"select*from [PLanillas].VerPlanillas";
                //4.- Almacenamos la información obtenida del store procedure
                List<PlanillaBoletaDBModel> producto = await _dbContext.PlanillaBoletaDB.FromSqlRaw(storePLanillaBoleta).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<PlanillaBoletaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // DELETE: api/PLanillaBoleta/{id}
        [HttpPost ("[controller]/Detalle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<PlanillaBoletaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DatosPlanillaBoleta([FromBody] requestPlanillaBoletaModel model)
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
                    "Id"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storePLanillaBoleta = $"[PLanillas].[DatosPlanilla]  {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<DetPlanillaBoletaDBModel> producto = await _dbContext.DetPlanillaBoletaDB.FromSqlRaw(storePLanillaBoleta, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<DetPlanillaBoletaDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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