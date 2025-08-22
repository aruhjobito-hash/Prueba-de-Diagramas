
// Developer    : VicVil  
// DateCreate   : 13/06/2025
// Description  : Controlador para Generar Reporte6A
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
    public class Reporte6AController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Reporte6AController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Reporte6A
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostReporte6A([FromBody] requestReporte6AModel model)
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
                    "iOpcion","cAnio","cMes","cUsuario","CodReporte6A"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte6A = $"REPORTES_ANEXOS_SBS.USP_REPORTE6A {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                _dbContext.Database.SetCommandTimeout(180);
                List<Reporte6ADBModel> producto = await _dbContext.Reporte6ADB.FromSqlRaw(storeReporte6A, parameters).ToListAsync();

                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );

                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Reporte6", "Reporte6A.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }
                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Reporte6A");
                worksheet = package.Workbook.Worksheets[0];
                worksheet.Cells[3, 2].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[4, 2].Value = fecchaal;

                if (producto != null)
                {
                    foreach (Reporte6ADBModel Reporte6 in producto)
                    {
                        worksheet.Cells[Reporte6.NroFila, 3].Value = Reporte6.MN_TasaAnual;
                        worksheet.Cells[Reporte6.NroFila, 4].Value = Reporte6.MN_Saldo;
                        worksheet.Cells[Reporte6.NroFila, 5].Value = Reporte6.ME_TasaAnual;
                        worksheet.Cells[Reporte6.NroFila, 6].Value = Reporte6.ME_Saldo;
                    }
                }

                package.Save();
                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Reporte6A.xlsx";

                return File(bytes, contentType, fileName);
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }






        // GET: api/Reporte6A
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetReporte6AModel([FromQuery] requestCodSucaveDBModel model,[FromQuery] string anio,[FromQuery] string mes)
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
                var parameters = parameterNames
                    .Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value))
                    .ToArray();

                string storeReporte6A = $"REPORTES_ANEXOS_SBS.USP_REPORTE6A_SUCAVE {string.Join(", ", parameterNames.Select(n => "@" + n))}";

                //  Consultar los datos
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeReporte6A, parameters).ToListAsync();

                //  Armar contenido del archivo
                var contenido = new StringBuilder();

                string codFormato = "1206";
                string codAnexo = "01";
                string codEmpresa = "01202";
                string fechaReporte = anio + mes.PadLeft(2, '0') + Utilitarios.DaysInMonth(int.Parse(anio), int.Parse(mes)).ToString("D2");
                string codExpresionMontos = "012";
                string datosControl = "              0"; // 14 espacios + 1 cero

                string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;
                contenido.AppendLine(header);

                foreach (var item in producto)
                {
                    contenido.AppendLine(item.valor);
                }

                //  Nombre del archivo generado
                string nameAnexo = $"01{anio.Substring(2, 2)}{mes.PadLeft(2, '0')}{Utilitarios.DaysInMonth(int.Parse(anio), int.Parse(mes)).ToString("D2")}120601202.SEI";

                //  Retornar como archivo
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