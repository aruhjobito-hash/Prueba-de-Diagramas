
// Developer    : VicVil  
// DateCreate   : 21/05/2025
// Description  : Controlador para Generar Balance Comprobacion de Reportes
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
using OfficeOpenXml.Style;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class BalanceComprobacionController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public BalanceComprobacionController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/BalanceComprobacion
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostBalanceComprobacion([FromBody] requestBalanceComprobacionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var parameterNames = new[]
                {
                    "iOpcion","cAnio","cMes","cUsuario","CodReporteEEFF"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeBalanceComprobacion = $"REPORTES_ANEXOS_SBS.USP_BALANCECOMPROBACION {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<BalanceComprobacionDBModel> producto = await _dbContext.BalanceComprobacionDB.FromSqlRaw(storeBalanceComprobacion, parameters).ToListAsync();

                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );
                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\BalanceComprobacion", "BalanceComprobacion.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }

                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("BalanceComprobacion");
                worksheet = package.Workbook.Worksheets[0];
                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[4, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[6, 1].Value = fecchaal;

                Int32 x = 9;

                if (producto != null)
                {
                    foreach (BalanceComprobacionDBModel item in producto) 
                    {
                        worksheet.Cells[x, 1].Value = item.Cuenta;
                        worksheet.Cells[x, 2, x, 3].Merge = true;
                        worksheet.Cells[x, 2].Value = item.Descripcion;
                        worksheet.Cells[x, 4].Value = Convert.ToDecimal(item.SaldoAnterior);
                        worksheet.Cells[x, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[x, 5].Value = Convert.ToDecimal(item.Debito);
                        worksheet.Cells[x, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[x, 6].Value = Convert.ToDecimal(item.Credito);
                        worksheet.Cells[x, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet.Cells[x, 7].Value = Convert.ToDecimal(item.SaldoActual);
                        worksheet.Cells[x, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                        x++;

                    }

                    if (producto.Count != 0)
                    {
                        worksheet.Cells[9, 1, x - 1, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[9, 1, x - 1, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[9, 1, x - 1, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[9, 1, x - 1, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    }

                    worksheet.Cells[x, 1, x, 7].Merge = true;
                    x++;
                    worksheet.Cells[x, 1, x, 3].Merge = true;
                }


                package.Save();
                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Balance de Comprobacion.xlsx";
                return File(bytes, contentType, fileName);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/BalanceComprobacion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetBalanceComprobacionModel([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
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
                string storeBalanceComprobacion = $"REPORTES_ANEXOS_SBS.usp_BalanceComprobacion_Sucave {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveStringDBModel> producto = await _dbContext.SucaveStringDB.FromSqlRaw(storeBalanceComprobacion, parameters).ToListAsync();

                var contenido = new StringBuilder();
                string codFormato = "1000";
                string codAnexo = "01";
                string codEmpresa = "01202";
                string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
                string codExpresionMontos = "012";
                string datosControl = "              0";

                string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

                contenido.AppendLine(header);

                foreach (var item in producto)
                {
                    contenido.AppendLine(item.id + item.valor);
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