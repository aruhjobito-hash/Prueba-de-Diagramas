
// Developer    : VicVil  
// DateCreate   : 10/06/2025
// Description  : Controlador para Generar Reporte 1
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
    public class Reporte1Controller : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Reporte1Controller(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Reporte1
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostReporte1([FromBody] requestReporte1Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var parameterNames = new[] {"iOpcion","cAnio","cMes","cUsuario","CodReporte1"};
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeReporte1 = $"REPORTES_ANEXOS_SBS.USP_REPORTE_1 {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                _dbContext.Database.SetCommandTimeout(180);
                List<Reporte1DBModel> producto = await _dbContext.Reporte1DB.FromSqlRaw(storeReporte1, parameters).ToListAsync();
                
                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );

                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Reporte1", "Reporte1.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }
                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Reporte1");
                worksheet = package.Workbook.Worksheets[0];

                worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";

                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[7, 1].Value = fecchaal;

                if (producto != null)
                {
                    int i = 12;
                    foreach (Reporte1DBModel Reporte1 in producto)
                    {
                        worksheet.Cells[i, 1].Value  = Reporte1.NroFila;
                        worksheet.Cells[i, 2].Value  = Reporte1.TipoDocumento;
                        worksheet.Cells[i, 3].Value  = Reporte1.NumeroDocumento;
                        worksheet.Cells[i, 4].Value  = Reporte1.TipoPersona;
                        worksheet.Cells[i, 5].Value  = Reporte1.ApeNomRazonSocial;
                        worksheet.Cells[i, 6].Value  = Reporte1.Nacionalidad;
                        worksheet.Cells[i, 7].Value  = Reporte1.Genero;
                        worksheet.Cells[i, 8].Value  = Reporte1.Domicilio;
                        worksheet.Cells[i, 9].Value  = Reporte1.UbigeoDomic;
                        worksheet.Cells[i, 10].Value = Reporte1.AportePagado;
                        worksheet.Cells[i, 11].Value = Reporte1.AporteSuscrito;
                        i++;
                    }
                }

                package.Save();

                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Reporte 1.xlsx";
                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        // GET: api/Reporte1
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetReporte1Model([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
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
                string storeReporte1 = $"REPORTES_ANEXOS_SBS.usp_Reporte1_Sucave {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeReporte1, parameters).ToListAsync();
                
                var contenido = new StringBuilder();
                string codFormato = "1201";
                string codAnexo = "01";
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

                string nameAnexo = "01" + anio.Substring(2, 2) + mes + Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120201202.SEI";

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