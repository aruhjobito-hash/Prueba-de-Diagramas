
// Developer    : feragu  
// DateCreate   : 05/03/2025
// Description  : Controlador para parametros de Contabilidad
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
    public class PlaCuentasControllerController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private int nLonCta;
        private int nTamRaiz;
        private string xRaiz;
        private string wTipMon;
        

        public PlaCuentasControllerController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/PlaCuentasController
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPlanCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestPlanCuentasModel>>> PostPlanCuentas([FromBody] requestPlanCuentasModel model)
        {
           

            var errors = ValidarCuentaContable(model, out string xRaiz, out int nTamRaiz,out string wTipMon);
            if (errors.Any())
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Errores de validacion",
                    Data = new ErrorTxA { codigo = "01", Mensaje = string.Join("; ", errors) }
                });
            }

            try
            {
                //(object?)model.cValor ?? DBNull.Value
                string query = "Contabilidad.sp_PlaCuentasController @op,@anio,@ctacontable,@raiz,@tipmon,@Descripcion,@wAsto,@NvaCtaContable,@sLogin";
                List<PlanCuentasDBModel> lista = await _dbContext.PlanCuentasDB.FromSqlRaw(query,
                    new SqlParameter("@op", "3"),
                    new SqlParameter("@anio", model.Año),
                    new SqlParameter("@ctacontable", model.CtaContable),
                    new SqlParameter("@raiz", xRaiz),
                    new SqlParameter("@tipmon", wTipMon),
                    new SqlParameter("@Descripcion", model.Descripcion),
                    new SqlParameter("@wAsto", model.CtaAsiento),
                    new SqlParameter("@NvaCtaContable",(object?)model.NvaCtaContable ?? DBNull.Value),
                    new SqlParameter("@sLogin",model.IdUser)
                    ).ToListAsync();
                int band = 0;
                
                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<PlanCuentasDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else if (band == 2) 
                {
                    return Ok(new Respuesta<List<PlanCuentasDBModel>>
                    {
                        Exito = 0,
                        Mensaje = "Errores de validacion",
                        Data =  lista
                    });
                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
                //***************
                //if (lista != null && lista.Count > 0)
                //{
                    
                //    return Ok(new Respuesta<PlanCuentasDBModel>{ Exito = 1, Mensaje = "Success" });
                //}
                //else
                //{
                //    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                //}
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // PUT: api/PlaCuentasController/{id}        
        //[HttpPut("PlaCuentasController/{X-profile-Token}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPlanCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutPlanCuentas( [FromBody] requestPlanCuentasModel model)
        {
            var errors = ValidarCuentaContable(model, out string xRaiz, out int nTamRaiz, out string wTipMon);
            if (errors.Any())
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Errores de validacion",
                    Data = new ErrorTxA { codigo = "01", Mensaje = string.Join("; ", errors) }
                });
            }

            try
            {
                //(object?)model.cValor ?? DBNull.Value
                string query = "Contabilidad.sp_PlaCuentasController @op,@anio,@ctacontable,@raiz,@tipmon,@Descripcion,@wAsto,@NvaCtaContable,@sLogin";
                List<PlanCuentasDBModel> lista = await _dbContext.PlanCuentasDB.FromSqlRaw(query,
                    new SqlParameter("@op", "4"),
                    new SqlParameter("@anio", model.Año),
                    new SqlParameter("@ctacontable", model.CtaContable),
                    new SqlParameter("@raiz", xRaiz),
                    new SqlParameter("@tipmon", wTipMon),
                    new SqlParameter("@Descripcion", model.Descripcion),
                    new SqlParameter("@wAsto", model.CtaAsiento),
                    new SqlParameter("@NvaCtaContable", (object?)model.NvaCtaContable ?? DBNull.Value),
                    new SqlParameter("@sLogin", model.IdUser)
                    ).ToListAsync();
                int band = 0;

                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<PlanCuentasDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else if (band == 2)
                {
                    return BadRequest(new Respuesta<List<PlanCuentasDBModel>>
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

        // GET: api/PlaCuentasController
        //[HttpGet("PlaCuentasController/{X-profile-Token}")]
        [HttpGet("PlanCuentas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPlanCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestPlanCuentasModel>>> GetPlanCuentas(string anio,string ctacontable)
        {
            try
            {
                string query = "Contabilidad.sp_PlaCuentasController @op,@anio,@ctacontable";
                List<PlanCuentasDBModel> lista = await _dbContext.PlanCuentasDB.FromSqlRaw(query,
                    new SqlParameter("@op", "1"),
                    new SqlParameter("@anio", anio),
                    new SqlParameter("@ctacontable", ctacontable)
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        [HttpGet("PlanCuentasCta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPlanCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestPlanCuentasModel>>> GetPlanCuentasCta(string anio, string ctacontable)
        {
            try
            {
                string query = "Contabilidad.sp_PlaCuentasController @op,@anio,@ctacontable";
                List<PlanCuentasDBModel> lista = await _dbContext.PlanCuentasDB.FromSqlRaw(query,
                    new SqlParameter("@op", "2"),
                    new SqlParameter("@anio", anio),
                    new SqlParameter("@ctacontable", ctacontable)
                    ).ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }
        [HttpGet("Areas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<AreaDBModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<AreaDBModel>>> GetAreas()
        {
            try
            {
                string query = "Contabilidad.sp_PlaCuentasController @op";
                List<AreaDBModel> lista = await _dbContext.PlanCuentasAreaDB.FromSqlRaw(query,
                    new SqlParameter("@op", "6")
                    ).AsNoTracking().ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }


        // DELETE: api/PlaCuentasController/{id}
        //[HttpDelete("PlaCuentasController/{X-profile-Token}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestPlanCuentasModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeletePlanCuentas(string ctacontable, string anio)
        {
            try
            {
                //(object?)model.cValor ?? DBNull.Value
                string query = "Contabilidad.sp_PlaCuentasController @op,@anio,@ctacontable,@raiz,@tipmon,@Descripcion,@wAsto,@NvaCtaContable,@sLogin";
                List<PlanCuentasDBModel> lista = await _dbContext.PlanCuentasDB.FromSqlRaw(query,
                    new SqlParameter("@op", "5"),
                    new SqlParameter("@anio", anio),
                    new SqlParameter("@ctacontable", ctacontable),
                    new SqlParameter("@raiz",DBNull.Value),
                    new SqlParameter("@tipmon", DBNull.Value),
                    new SqlParameter("@Descripcion", DBNull.Value),
                    new SqlParameter("@wAsto", DBNull.Value),
                    new SqlParameter("@NvaCtaContable", DBNull.Value),
                    new SqlParameter("@sLogin", DBNull.Value)
                    ).ToListAsync();
                int band = 0;

                band = lista.Select(item => item.bEstado).FirstOrDefault(bEstado => bEstado == "1" || bEstado == "0") == "1" ? 1 : 2;

                if (band == 1)
                {
                    return Ok(new Respuesta<List<PlanCuentasDBModel>>
                    {
                        Exito = 1,
                        Mensaje = "Success",
                        Data = lista
                    });
                }
                else if (band == 2)
                {
                    return BadRequest(new Respuesta<List<PlanCuentasDBModel>>
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


                //string storePlaCuentasController = "sp_PlaCuentasController";
                //var deleteResult = await _dbContext.PlanCuentasDB.FromSqlRaw(storePlaCuentasController, id).ToListAsync();
                //if (deleteResult.Count == 0)
                //{
                //    return NotFound();
                //}
                //return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        ///***********************************//
        private List<string> ValidarCuentaContable(requestPlanCuentasModel model, out string xRaiz, out int nTamRaiz,out string wTipMon)
        {
            xRaiz = "";
            nTamRaiz = 0;
            wTipMon = "";
            int nLonCta = model.CtaContable.Trim().Length;

            if (nLonCta > 2)
            {
                xRaiz = model.CtaContable.Trim().Substring(0, nLonCta - 2);
                nTamRaiz = xRaiz.Trim().Length;
                wTipMon = model.CtaContable.Trim().Substring(2, 1);
            }
            else
            {
                if (nTamRaiz == 0 && nLonCta == 2)
                {
                    nTamRaiz = nLonCta - 1;
                    xRaiz = model.CtaContable.Trim().Substring(0, nTamRaiz);
                }
                if (nTamRaiz == 0 && nLonCta > 2)
                {
                    nTamRaiz = nLonCta - 2;
                    xRaiz = model.CtaContable.Trim().Substring(0, nTamRaiz);
                }
            }

            return new List<string>
            {
                string.IsNullOrWhiteSpace(model.CtaContable) ? "Debe indicar la cuenta contable" : null,
                string.IsNullOrWhiteSpace(model.Descripcion) ? "Debe indicar la descripción de la cuenta contable" : null,
                xRaiz == model.CtaContable.Trim() ? "La divisionaria no puede ser igual a la raíz" : null,
                xRaiz != model.CtaContable.Trim().Substring(0, nTamRaiz) ? "La cuenta no es divisionaria de la raíz indicada" : null
            }.Where(x => x != null).ToList();
        }

    }
}