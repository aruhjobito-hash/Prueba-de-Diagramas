
// Developer    : VicVil  
// DateCreate   : 26/04/2025
// Description  : Controlador para generar el anexo13
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Net.Mime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using ApiAppLeon.Controllers.Utilitarios;
using System.Text;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class Anexo13Controller : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Anexo13Controller(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<ActionResult<Respuesta<requestAnexo13Model>>> PostAnexo13([FromBody] requestAnexo13Model model)
        // POST: api/Anexo13Controller
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostAnexo13([FromBody] requestAnexo13Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string storeAnexo13Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO13CONTROLLER @OPERACION = '5',@CodAnexo13 = NULL, @cMes = {0}, @cAnio = {1}, @cUsuario = {2}";
                List<Anexo13DBModel> producto = await _dbContext.Anexo13DB.FromSqlRaw(storeAnexo13Controller, model.cMes, model.cAnio,model.cUsuario).ToListAsync();

                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );


                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Anexo13", "Anexo13.xlsx");

                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }

                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Anexo13");
                worksheet = package.Workbook.Worksheets[0];

                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[7, 1].Value = fecchaal;

                worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";



                if (producto != null)
                {
                    foreach (Anexo13DBModel Anexo13 in producto)
                    {
                        worksheet.Cells[Anexo13.iNroFila, 3].Value = Anexo13.iNumerosCuentaMN;
                        worksheet.Cells[Anexo13.iNroFila, 4].Value = Anexo13.iNumerosCuentaME;
                        worksheet.Cells[Anexo13.iNroFila, 5].Value = Anexo13.iNumerosCuentaTotal;
                        worksheet.Cells[Anexo13.iNroFila, 6].Value = Anexo13.nSaldoMN;
                        worksheet.Cells[Anexo13.iNroFila, 7].Value = Anexo13.nSaldoEquivalente;
                        worksheet.Cells[Anexo13.iNroFila, 8].Value = Anexo13.nSaldoTotal;

                    }
                }

                
                package.Save();
                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"Anexo13_{model.cMes}-{model.cAnio}.xlsx";
                return File(bytes, contentType, fileName);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Anexo13Controller
        //[HttpGet("DescargarSucave")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo13Model>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        //public async Task<ActionResult<IEnumerable<requestAnexo13Model>>> GetAnexo13SucaveModel([FromQuery] string CodAnexo13)
        //{
        //    try
        //    {
        //        string storeAnexo13Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO13CONTROLLER @OPERACION = '3',@CodAnexo13 = {0}";
        //        var result = await _dbContext.Anexo13SucaveDB.FromSqlRaw(storeAnexo13Controller,CodAnexo13).ToListAsync();

        //        // CABECERA
        //        string codFormato = "1113";
        //        string codAnexo = "02";
        //        string codEmpresa = "01202";
        //        string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
        //        string codExpresionMontos = "012";
        //        string datosControl = "              0";

        //        string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;
        //        var contenido = new StringBuilder();
        //        contenido.AppendLine(header);

        //        foreach (var item in result)
        //        {
        //            contenido.AppendLine(item.Valor);
        //        }

        //        //DEFINIR EL NOMBRE DEL ARCHIVO

        //        string nameAnexo = "02" + anio.Substring(2, 2) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "111301202.SEI";


        //        var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
        //        return File(bytes, "text/plain", nameAnexo);




        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
        //    }
        //}

        // GET: api/Anexo13Controller
        [HttpGet("DescargarSucave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo13Model>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAnexo13Model>>> GetAnexo13SucaveModel([FromQuery] string CodAnexo13)
        {
            try
            {
                string storeAnexo13Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO13CONTROLLER @OPERACION = '3',@CodAnexo13 = {0}";
                var result = await _dbContext.Anexo13SucaveDB.FromSqlRaw(storeAnexo13Controller, CodAnexo13).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }


        // GET: api/Anexo13Controller
        [HttpGet("DatosExcel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestAnexo13Model>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestAnexo13Model>>> GetAnexo13Model([FromQuery] string? CodAnexo, [FromQuery] string? cMes , [FromQuery] string? cAnio, [FromQuery] string? cUsuario)
        {
            try
            {
                string storeAnexo13Controller = "REPORTES_ANEXOS_SBS.SP_ANEXO13CONTROLLER @OPERACION = '4',@CodAnexo13 = {0}, @cMes = {1}, @cAnio = {2}, @cUsuario = {3} ";
                var result = await _dbContext.Anexo13DB.FromSqlRaw(storeAnexo13Controller, CodAnexo,cMes,cAnio,cUsuario).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }




    }
}