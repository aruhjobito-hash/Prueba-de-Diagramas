
// Developer    : VicVil  
// DateCreate   : 09/06/2025
// Description  : Controlador para Generar Reporte 2A
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
    public class Reporte2AController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Reporte2AController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Reporte2A
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostReporte2A([FromBody] requestReporte2AModel model)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {

                var parameterNames = new[]
                {
                    "iOpcion","cAnio","cMes","cUsuario","CodReporte2A"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte2A = $"REPORTES_ANEXOS_SBS.usp_Reporte2A_N {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<Reporte2ADBModel> producto = await _dbContext.Reporte2ADB.FromSqlRaw(storeReporte2A, parameters).ToListAsync();
                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );
                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Reporte2", "Reporte2A.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }
                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Reporte2A");
                worksheet = package.Workbook.Worksheets[0];
                worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";
                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[7, 1].Value = fecchaal;

                if (producto != null)
                {
                    foreach (Reporte2ADBModel Reporte2A in producto)
                    {
                        worksheet.Cells[14, 4].Value = Reporte2A.f1 ?? 0;
                        worksheet.Cells[19, 4].Value = Reporte2A.f2 ?? 0;
                        worksheet.Cells[20, 4].Value = Reporte2A.f3 ?? 0;
                        worksheet.Cells[21, 4].Value = Reporte2A.f4 ?? 0;
                        worksheet.Cells[20, 5].Value = Reporte2A.f5 ?? 0;
                        worksheet.Cells[21, 5].Value = Reporte2A.f6 ?? 0;
                        worksheet.Cells[21, 9].Value = Reporte2A.f7 ?? 0;
                        worksheet.Cells[23, 15].Value = Reporte2A.f8 ?? 0;
                    }
                }

                package.Save();

                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Reporte 2A.xlsx";

                return File(bytes, contentType, fileName);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        // GET: api/Reporte2A
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetReporte2AModel([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
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
                var parameterNames = new[]{"Codigo"};
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte2A = $"REPORTES_ANEXOS_SBS.USP_REPORTE2A_SUCAVE {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeReporte2A, parameters).ToListAsync();
                var contenido = new StringBuilder();
                string codFormato = "1202";
                string codAnexo = "03";
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

                string nameAnexo = "03" + anio.Substring(2, 2) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120201202.SEI";

                var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
                return File(bytes, "text/plain", nameAnexo);



            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }


    }
}