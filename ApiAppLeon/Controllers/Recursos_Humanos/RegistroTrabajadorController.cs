
// Developer    : JosAra  
// DateCreate   : 08/07/2025
// Description  : Endpoints para registro de trabajador
using ApiAppLeon;
using ApiAppLeon.Controllers.Utilitarios;
using ApiAppLeon.Models.Recursos_Humanos;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
namespace Recursos_Humanos.Controllers
{
    [ApiExplorerSettings(GroupName = "Recursos_Humanos")]
    [Route("api/Recursos_Humanos/[controller]")]
    [ApiController]
    public class RegistroTrabajadorController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public RegistroTrabajadorController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly APIModel _apiModel = new APIModel(); 

        // POST: api/RegistroTrabajador
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroTrabajadorDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestRegistroTrabajadorModel>>> PostRegistroTrabajador([FromBody] requestRegistroTrabajadorModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {

                //1.- Definimos los parametros que entrarán al store procedure
                var parameterNames = new[]
                {
                    "IdPersona","Direccion","cReferencia","IdUbigeoDir","Telefono2","Telefono","IngMensual","TipPersona","IdTipViv","Email","IdMedioDif","Requisitoriado","FechaNac","IdUbigeoNac","IdUrbe","idUser","Fecpro","Hora","FechaReg","ApePat","ApeMat","Nombres","Sexo","IdTipDocId","NroDocId","IdOcupacion","IdEstCivil","IdGradInst","IdProfesion","ApeCas","Nacionalidad","CarnetSeguro","IdPension","IdCtaPension","FechaInscripcion","iCalculo","iDeclararPDT","iAfectoQuinta","cInformacionDomicilio","cImagen","fechaIngreso","cIdTipTrabajador","CentroCosto","IdAgencia","IdArea","IdCargo","cIdRegLaboral","cIdTipContrato","dFechaTerminoContrato","cSItuacion","RUC_EPS","dFechaCese","mIngresoBasico","mAsigFamiliar","tipMoneda","ctaBancariaHaberes","cIdBancoHaberes","ctaBancariaCTS","cIdBancoCTS","cidSitEsp","cPeriodo","cTipoPago","bTrabRegAlt","bTrabJorMax","bTrabHorNoc","cCentroRiesgo","SCTRSalud","SCTRPension","mOtrosIngresos","bSindicalizado","bDiscapacitado","bDomiciliado","bEsSaludVida","bAfiliaSeguroPension","bRentaExonerada","BackOffice","FormaMarcado","Opt"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeRegistroTrabajador = $"[PERSONAL].[Registra_Personal] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroTrabajadorDBModel> producto = await _dbContext.RegistroTrabajadorDB.FromSqlRaw(storeRegistroTrabajador, parameters).ToListAsync();
                //5.- Verificamos si se obtuvo información y retornamos la respuesta adecuada

                List<requestRegistroMiembrosFamModel> ListaFamiliar = new(); //Inicializamos la lista para almacenar los familiares en la tabla RegistroMiembrosFamTable
                List<requestRegistroMiembrosFamModel> ListaConsultaPersona = new();
                foreach (var item in model.ListaFamiliares)
                {
                    var client = new HttpClient(); 
                    var clientFam = new HttpClient();
                    requestRegistroMiembrosFamModel personaFamTrab = new requestRegistroMiembrosFamModel();
                    personaFamTrab.IdPersonaTrab = model.IdPersona;
                    ConsultaPersona dni = new(); // Obtenemos el DNI del familiar
                    dni.DNI=item.NroDocId; // Asignamos el DNI del familiar
                    clientFam.BaseAddress = new Uri(_apiModel.APIURL);
                    var existeFamiliarResponse = await clientFam.PostAsJsonAsync($"/api/Utilitarios/ConsultaPersona", dni);
                    //Verificamos si existe una persona con el DNI del familiar
                    if (existeFamiliarResponse.IsSuccessStatusCode)
                    {                       
                        try
                        {                           
                            var consultaPersona = await existeFamiliarResponse.Content.ReadFromJsonAsync<List<ConsultaPersonaRes>>();
                            if (consultaPersona != null)
                            {
                                foreach(var PersonaFam in consultaPersona)
                                {
                                    string IdPersonaTrabajador = string.Empty; // Initialize with an empty string or a default value.
                                    // Update the foreach loop where 'IdPersonaTrabajador' is assigned.
                                    foreach (var regtrabajador in producto)
                                    {
                                        IdPersonaTrabajador = regtrabajador.IdPersona;
                                    }
                                    personaFamTrab.IdPersonaTrab = IdPersonaTrabajador; // Asignamos el IdPersonaTrabajador
                                    personaFamTrab.IdPersonaFam = PersonaFam.IdPersona.ToString();
                                    personaFamTrab.IdParentesco = item.IdParentesco;
                                    personaFamTrab.IdUser = model.idUser;
                                    personaFamTrab.Apoderado = "0";
                                    personaFamTrab.FecPro = DateTime.Now;
                                    personaFamTrab.hora = DateTime.Now.ToString("HH:mm:ss");
                                    personaFamTrab.Estado = "1"; // Estado activo
                                    personaFamTrab.FecModificacion = DateTime.Now;
                                    personaFamTrab.IdUserModificacion = model.idUser;
                                    ListaFamiliar.Add(personaFamTrab);                                    
                                }                               
                            }
                            else
                            {
                                throw new Exception("No se pudo obtener informacion del familiar con DNI:"+ dni.DNI + " ");
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine("JSON Error: " + ex.Message);
                        }
                        // Si no existe, creamos un nuevo registro de persona
                    }
                    else { 
                        PersonaModel personaModel = CopySharedProperties<PersonaFamModel, PersonaModel>(item);
                        client.BaseAddress = new Uri(_apiModel.APIURL);
                        var registrapersonarespone = await client.PostAsJsonAsync("/api/Utilitarios/Registro/RegistraPersona", personaModel);
                        Console.WriteLine(registrapersonarespone);
                        if (registrapersonarespone.IsSuccessStatusCode)
                        {
                            string IdPersonaFamiliar = string.Empty;
                            var dataresponse = await registrapersonarespone.Content.ReadFromJsonAsync < List<ConsultaPersonaRes>>();                            
                            foreach(var persona in dataresponse)
                            {
                                IdPersonaFamiliar=persona.IdPersona; // Obtenemos el IdPersona del familiar registrado
                            }
                            string IdPersonaTrabajador = string.Empty; // Initialize with an empty string or a default value.
                            foreach (var regtrabajador in producto)
                            {
                                IdPersonaTrabajador = regtrabajador.IdPersona;
                            }
                            personaFamTrab.IdPersonaTrab = IdPersonaTrabajador; // Asignamos el IdPersonaTrabajador
                            personaFamTrab.IdPersonaFam = IdPersonaFamiliar;
                            personaFamTrab.IdParentesco = item.IdParentesco;
                            personaFamTrab.IdUser = model.idUser;
                            personaFamTrab.Apoderado = "0";
                            personaFamTrab.FecPro = DateTime.Now;
                            personaFamTrab.hora = DateTime.Now.ToString("HH:mm:ss");
                            personaFamTrab.Estado = "1"; // Estado activo
                            personaFamTrab.FecModificacion = DateTime.Now;
                            personaFamTrab.IdUserModificacion = model.idUser;
                            ListaFamiliar.Add(personaFamTrab);
                        }
                        else
                        {
                            throw new Exception("No se pudo realizar el registro del familiar con DNI: " + dni.DNI);
                        }
                    }
                }   

                var famclient = new HttpClient(); // Inicializamos el cliente para crear los familiares de acuerdo a la data enviada
                famclient.BaseAddress = new Uri(_apiModel.APIURL);

                var registrarFamiliaresResponse = await famclient.PostAsJsonAsync("/api/Recursos_Humanos/RegistroMiembrosFam/save", ListaFamiliar);
                if (registrarFamiliaresResponse.IsSuccessStatusCode)
                {
                    var miembrosfamresponse = await registrarFamiliaresResponse.Content.ReadFromJsonAsync<Respuesta<List<requestRegistroMiembrosFamModel>>>();
                    ListaConsultaPersona=miembrosfamresponse.Data; // Obtenemos la data de la respuesta

                }
                else
                {
                    
                    return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Error al registrar familiares", Data = new ErrorTxA { codigo = "04", Mensaje = "Error al registrar familiares" } });
                }
                await transaction.CommitAsync();
                if (producto != null && producto.Count > 0)
                {                    
                    return Ok(new Respuesta<List<RegistroTrabajadorDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No existen registros", Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos del la base de datos" } });
                }

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // PUT: api/RegistroTrabajador/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroTrabajadorDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutRegistroTrabajador([FromBody] requestRegistroTrabajadorModel model)
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
                    "IdPersona","Direccion","cReferencia","IdUbigeoDir","Telefono2","Telefono","IngMensual","TipPersona","IdTipViv","Email","IdMedioDif","Requisitoriado","FechaNac","IdUbigeoNac","IdUrbe","idUser","Fecpro","Hora","FechaReg","ApePat","ApeMat","Nombres","Sexo","IdTipDocId","NroDocId","IdOcupacion","IdEstCivil","IdGradInst","IdProfesion","ApeCas","Nacionalidad","CarnetSeguro","IdPension","IdCtaPension","FechaInscripcion","iCalculo","iDeclararPDT","iAfectoQuinta","cInformacionDomicilio","cImagen","fechaIngreso","cIdTipTrabajador","CentroCosto","IdAgencia","IdArea","IdCargo","cIdRegLaboral","cIdTipContrato","dFechaTerminoContrato","cSItuacion","RUC_EPS","dFechaCese","mIngresoBasico","mAsigFamiliar","tipMoneda","ctaBancariaHaberes","cIdBancoHaberes","ctaBancariaCTS","cIdBancoCTS","cidSitEsp","cPeriodo","cTipoPago","bTrabRegAlt","bTrabJorMax","bTrabHorNoc","cCentroRiesgo","SCTRSalud","SCTRPension","mOtrosIngresos","bSindicalizado","bDiscapacitado","bDomiciliado","bEsSaludVida","bAfiliaSeguroPension","bRentaExonerada","BackOffice","Opt"
                };
                //2.- Construimos los parametros para usando reflección,obteniendo el tipo de dato y la longitud
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                //3.- Creamos el store procedure que será llamado
                string storeRegistroTrabajador = $"[PERSONAL].[Registra_Personal] {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                //4.- Almacenamos la información obtenida del store procedure
                List<RegistroTrabajadorDBModel> producto = await _dbContext.RegistroTrabajadorDB.FromSqlRaw(storeRegistroTrabajador, parameters).ToListAsync();
                
                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroTrabajadorDBModel>>{ Exito = 1, Mensaje = "Success",Data = producto });
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

        // GET: api/RegistroTrabajador
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<RegistroTrabajadorDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestRegistroTrabajadorModel>>> GetRegistroTrabajadorModel([FromQuery]string DNI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                List<RegistroTrabajadorDBModel> registroTrabajador = new List<RegistroTrabajadorDBModel>();
                List<PersonaFamModel> familiaresTrabajador = new List<PersonaFamModel>();
                //1.- Definimos los parametros que entrarán al store procedure
                var parameters = new[]
                {
                    new SqlParameter("@DNI", string.IsNullOrEmpty(DNI) ? DBNull.Value : (object)DNI)
                };

                //2.- Creamos el store procedure que será llamado
                string storeRegistroTrabajador = "[PERSONAL].[GetDatosTrabajador] @DNI";
                string storeRegistroFamiliarTrabajador = "[PERSONAL].[getDatosTrabajadorCompleto] @DNI";
                //3.- Almacenamos la información obtenida del store procedure
                List<RegistroTrabajadorDBModel> producto = await _dbContext.RegistroTrabajadorDB.FromSqlRaw(storeRegistroTrabajador, parameters).ToListAsync();
                List<PersonaFamModel> FamiliaresTrabajador = await _dbContext.ReporteMiembrosFam.FromSqlRaw(storeRegistroFamiliarTrabajador, parameters).ToListAsync();
                foreach (var item in producto)
                {
                    // Fix for CS1503: Convert the array to a list before adding it to the ListaFamiliares property.
                    item.ListaFamiliares=FamiliaresTrabajador;
                    registroTrabajador.Add(item);                    
                }
                //4.- Almacenamos la información obtenida del store procedure

                if (producto != null && producto.Count > 0)
                {
                    return Ok(new Respuesta<List<RegistroTrabajadorDBModel>>{ Exito = 1, Mensaje = "Success",Data = registroTrabajador });
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

        // DELETE: api/RegistroTrabajador/{id}
        [HttpGet("ListarCombos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ListarCombosRegistraPersonaBD>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteRegistroTrabajador()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                string store = "SELECT * FROM ListarCombosRegistraPersona";
                var flatList = await _dbContext.ListarCombosRegistraPersonaBDs
                    .FromSqlRaw(store)
                    .ToListAsync();

                if (flatList == null || flatList.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No existen registros",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos de la base de datos" }
                    });
                }

                var grouped = flatList
                    .GroupBy(x => new { x.IdLista, x.NombreLista })
                    .Select(g => new ListaCombosRegistraPersona
                    {
                        IdLista = g.Key.IdLista,
                        NombreLista = g.Key.NombreLista,
                        List = g.Select(i => new ListaCombos
                        {
                            Id = i.Id,
                            Des = i.Des
                        }).ToList()
                    }).ToList();

                return Ok(new Respuesta<List<ListaCombosRegistraPersona>>
                {
                    Exito = 1,
                    Mensaje = "Success",
                    Data = grouped
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

        [HttpGet("ListarTrabajadores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ListarCombosRegistraPersonaBD>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ListarTrabajadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }       
            try
            {
                string store = "select*from [PERSONAL].[ListarTrabajadores]";
                var flatList = await _dbContext.RegistroTrabajadorDB
                    .FromSqlRaw(store)
                    .ToListAsync();

                if (flatList == null || flatList.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No existen registros",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No se obtuvo datos de la base de datos" }
                    });
                }              

                return Ok(new Respuesta<List<RegistroTrabajadorDBModel>>
                {
                    Exito = 1,
                    Mensaje = "Success",
                    Data = flatList
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

        public static TTarget CopySharedProperties<TSource, TTarget>(TSource source)
    where TSource : class
    where TTarget : class, new()
        {
            var target = new TTarget();

            var sourceProps = typeof(TSource).GetProperties();
            var targetProps = typeof(TTarget).GetProperties();

            foreach (var prop in targetProps)
            {
                var match = sourceProps.FirstOrDefault(p => p.Name == prop.Name && p.PropertyType == prop.PropertyType);
                if (match != null)
                {
                    prop.SetValue(target, match.GetValue(source));
                }
            }

            return target;
        }
    }
}