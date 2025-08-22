
// Developer    : VicVil  
// DateCreate   : 02/05/2025
// Description  : Controlador para generar el Anexo5
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Reportes_Anexos_SBS;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using ApiAppLeon.Controllers.Utilitarios;
using System.Text;

namespace Reportes_Anexos_SBS.Controllers
{
    [ApiExplorerSettings(GroupName = "Reportes_Anexos_SBS")]
    [Route("api/Reportes_Anexos_SBS/[controller]")]
    [ApiController]
    public class Anexo5Controller : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Anexo5Controller(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Anexo5
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostAnexo5([FromBody] requestAnexo5Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var parameterNames = new[] { "cAnio", "cMes", "cUsuario" };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeAnexo5 = $"REPORTES_ANEXOS_SBS.SP_Anexo5A {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                _dbContext.Database.SetCommandTimeout(180);
                List<Anexo5DBModel> producto = await _dbContext.Anexo5DB.FromSqlRaw(storeAnexo5, parameters).ToListAsync();


                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );
                
                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Anexo5", "Anexo5A_plantilla.xlsx");
                if (!System.IO.File.Exists(pathPlantilla))
                {
                    return NotFound("No se encontró la plantilla.");
                }
                using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.Add("Reporte1");
                worksheet = package.Workbook.Worksheets[0];

                worksheet.Cells[4, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
                worksheet.Cells[5, 1].Value = "CÓDIGO: 01202";

                string fecchaal = Utilitarios.MesEnLetras(model.cMes) + " DE " + model.cAnio.ToString();
                worksheet.Cells[6, 1].Value = fecchaal;

                // ANEXO 5A movido a otro archivo
                if (producto != null)
                {
                    foreach (Anexo5DBModel R in producto)
                    {
                        int x = 11;

                        worksheet.Cells[x, 3].Value = R.CDSaldo;
                        worksheet.Cells[x, 4].Value = R.CDSaldo;
                        worksheet.Cells[x, 5].Value = R.CDProvisionesGenericas;
                        worksheet.Cells[x, 6].Value = R.CDProvisionesEspecificas;

                        x += 1;
                        worksheet.Cells[x, 3].Value = "";
                        worksheet.Cells[x, 4].Value = "";
                        worksheet.Cells[x, 5].Value = "";
                        worksheet.Cells[x, 6].Value = "";


                        x += 5;
                        worksheet.Cells[x, 3].Value = Math.Round(R.CDSaldo, 0);
                        worksheet.Cells[x, 4].Value = Math.Round(R.CDSaldo, 0);
                        worksheet.Cells[x, 5].Value = Math.Round(R.CDProvisionesGenericas, 0);
                        worksheet.Cells[x, 6].Value = Math.Round(R.CDProvisionesEspecificas, 0);

                        x += 2;
                        worksheet.Cells[x, 3].Value = Math.Round(R.CDSaldo, 0);
                        worksheet.Cells[x + 1, 3].Value = Math.Round(R.CDProvisionesGenericas, 0);
                        worksheet.Cells[x + 2, 3].Value = Math.Round(R.CDProvisionesEspecificas, 0);

                    }
                }

                package.Save();

                var bytes = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "ANEXO 5 - CIFRAS.xlsx";
                return File(bytes, contentType, fileName);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

       













    }
}