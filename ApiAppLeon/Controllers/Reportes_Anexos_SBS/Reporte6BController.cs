
// Developer    : VicVil  
// DateCreate   : 14/06/2025
// Description  : Controlador para Generar Reporte6B
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
    public class Reporte6BController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Reporte6BController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Reporte6B
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostReporte6B([FromBody] requestReporte6BModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                _dbContext.Database.SetCommandTimeout(360);
                var parameterNames = new[]
                {
                    "iOpcion","cAnio","cMes","cUsuario","CodReporte6B"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte6B = $"REPORTES_ANEXOS_SBS.USP_REPORTE6B {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<Reporte6BDBModel> producto = await _dbContext.Reporte6BDB.FromSqlRaw(storeReporte6B, parameters).ToListAsync();

                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );

                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Reporte6", "Reporte6B.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }
                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Reporte6B");
                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";
                worksheet.Cells[7, 1].Value = fecchaal;

                if (producto != null)
                {

                    foreach (Reporte6BDBModel Reporte6B in producto)
                    {
                        worksheet.Cells[Reporte6B.NroFila, 4].Value = Reporte6B.MNTasaAnual;
                        worksheet.Cells[Reporte6B.NroFila, 5].Value = Reporte6B.MNSaldoSoles;
                        worksheet.Cells[Reporte6B.NroFila, 6].Value = Reporte6B.MNTasaMinima;
                        worksheet.Cells[Reporte6B.NroFila, 7].Value = Reporte6B.MNTasaMaxima;

                        worksheet.Cells[Reporte6B.NroFila, 8].Value = Reporte6B.METasaAnual;
                        worksheet.Cells[Reporte6B.NroFila, 9].Value = Reporte6B.MESaldoDolares;
                        worksheet.Cells[Reporte6B.NroFila, 10].Value = Reporte6B.METasaMinima;
                        worksheet.Cells[Reporte6B.NroFila, 11].Value = Reporte6B.METasaMaxima;
                    }
                }

                package.Save();

                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Reporte 6B.xlsx";

                return File(bytes, contentType, fileName);


            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }



        // GET: api/Reporte6B
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetReporte6BModel([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
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
                var parameterNames = new[]
                {
                    "Codigo"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte6B = $"REPORTES_ANEXOS_SBS.USP_REPORTE6B_SUCAVE {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeReporte6B, parameters).ToListAsync();

                var contenido = new StringBuilder();


                // CABECERA
                string codFormato = "1206";
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

                string nameAnexo = "02" + anio.Substring(2, 2) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120601202.SEI";

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