
// Developer    : VicVil  
// DateCreate   : 16/06/2025
// Description  : Controlador para Generar EEFF SITUACION FINANCIERA
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using OfficeOpenXml;
using Microsoft.Data.SqlClient;
using System.Text;
using ApiAppLeon.Controllers.Utilitarios;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class EEFFSITUACIONFINANCIERAController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public EEFFSITUACIONFINANCIERAController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/EEFFSITUACIONFINANCIERA
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostEEFFSITUACIONFINANCIERA([FromBody] requestEEFFSITUACIONFINANCIERAModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var parameterNames = new[] { "iOpcion","cAnio","cMes","cUsuario","CodReporteEEFF" };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeEEFFSITUACIONFINANCIERA = $"REPORTES_ANEXOS_SBS.USP_EEFF_SITUACIONFINANCIERA {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<EEFFSITUACIONFINANCIERADBModel> producto = await _dbContext.EEFFSITUACIONFINANCIERADB.FromSqlRaw(storeEEFFSITUACIONFINANCIERA, parameters).ToListAsync();

                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );

                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\EEFF\\SituacionFinanciera", "SituacionFinanciera.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }

                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("SituacionFinanciera");
                worksheet = package.Workbook.Worksheets[0];
                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[4, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[5, 1].Value = "CÓDIGO: 01202";
                worksheet.Cells[6, 1].Value = fecchaal;

                if (producto != null)
                {

                    foreach (EEFFSITUACIONFINANCIERADBModel item in producto)
                    {
                        worksheet.Cells[Convert.ToInt32(item.Posicion), 3].Value = item.MonedaLocal;
                        worksheet.Cells[Convert.ToInt32(item.Posicion), 4].Value = item.MonedaExtranjera;
                        worksheet.Cells[Convert.ToInt32(item.Posicion), 5].Value = item.Total;
                    }
                }

                package.Save();
                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "EEFF - Situacion Finaciera.xlsx";
                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        // GET: api/EEFFSITUACIONFINANCIERA
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetEEFFSITUACIONFINANCIERAModel([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Invalid Model State",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }
            try
            {
                var parameterNames = new[] { "Codigo" };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeEEFFSITUACIONFINANCIERA = $"REPORTES_ANEXOS_SBS.usp_EEFF_SituacionFinanciera_Sucave {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeEEFFSITUACIONFINANCIERA, parameters).ToListAsync();

                var contenido = new StringBuilder();
                string codFormato = "1000";
                string codAnexo = "02";
                string codEmpresa = "01202";
                string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
                string codExpresionMontos = "012";
                string datosControl = "              0";

                string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

                contenido.AppendLine(header);

                foreach (var item in producto)
                {
                    contenido.AppendLine(item.valor);
                }

                string nameAnexo = "02" + anio.Substring(2, 2) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "100001202.SEI";

                var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
                return File(bytes, "text/plain", nameAnexo);

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

        

    }
}