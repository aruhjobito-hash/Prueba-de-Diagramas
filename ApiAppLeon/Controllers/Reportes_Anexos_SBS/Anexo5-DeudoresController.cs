
// Developer    : VicVil  
// DateCreate   : 15/07/2025
// Description  : Controlador para Generar Anexo 5 - Deudores Provisiones
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
    public class Anexo5DeudoresController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public Anexo5DeudoresController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Anexo5-Deudores
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<IActionResult> PostAnexo5Deudores([FromBody] requestAnexo5DeudoresModel model)
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
                    "iOpcion","cAnio","cMes","cUsuario"
                };
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();
                string storeAnexo5Deudores = $"REPORTES_ANEXOS_SBS.USP_ANEXO5A {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<Anexo5DeudoresDBModel> producto = await _dbContext.Anexo5DeudoresDB.FromSqlRaw(storeAnexo5Deudores, parameters).ToListAsync();
                if (producto == null || !producto.Any())
                    return NotFound(
                        new Respuesta<ErrorTxA>
                        {
                            Exito = 0,
                            Mensaje = "No data returned from DB",
                            Data = new ErrorTxA { codigo = "02", Mensaje = "No products found" }
                        }
                    );
                var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "recursos\\Anexo5", "Anexo5_2023.xlsx");
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
                worksheet.Cells[4, 8].Value = "CÓDIGO: 01202";
                worksheet.Cells[6, 1].Value = fecchaal;

                if (producto != null)
                {
                    int x = 14;
                    foreach (Anexo5DeudoresDBModel R in producto)
                    {
                        switch (R.Tipo)
                        {
                            case 2:
                                x = 24;
                                break;
                            case 3:
                                x = 34;
                                break;
                            case 4:
                                x = 44;
                                break;
                            case 6:
                                x = 65;
                                break;
                            case 7:
                                x = 75;
                                break;
                            case 8:
                                x = 85;
                                break;
                            case 9:
                                x = 93;
                                break;
                            case 10:
                                x = 95;
                                break;
                            case 11:
                                x = 105;
                                break;
                            case 12:
                                x = 115;
                                break;
                            case 13:
                                x = 125;
                                break;
                        }

                        if (R.Tipo == 8)
                        {
                            if (R.Posicion == 3) // Pequeñas Empresas
                            {
                                worksheet.Cells[x + 3, 3].Value = R.Normal;
                                worksheet.Cells[x + 3, 4].Value = R.CPP;
                                worksheet.Cells[x + 3, 5].Value = R.Deficiente;
                                worksheet.Cells[x + 3, 6].Value = R.Dudoso;
                                worksheet.Cells[x + 3, 7].Value = R.Perdida;
                                worksheet.Cells[x + 3, 8].Value = R.Total;
                            }

                            if (R.Posicion == 4) // Microempresas
                            {
                                worksheet.Cells[x + 4, 3].Value = R.Normal;
                                worksheet.Cells[x + 4, 4].Value = R.CPP;
                                worksheet.Cells[x + 4, 5].Value = R.Deficiente;
                                worksheet.Cells[x + 4, 6].Value = R.Dudoso;
                                worksheet.Cells[x + 4, 7].Value = R.Perdida;
                                worksheet.Cells[x + 4, 8].Value = R.Total;
                            }

                            if (R.Posicion == 7) // Hipotecario
                            {
                                worksheet.Cells[x + 5, 3].Value = R.Normal;
                                worksheet.Cells[x + 5, 4].Value = R.CPP;
                                worksheet.Cells[x + 5, 5].Value = R.Deficiente;
                                worksheet.Cells[x + 5, 6].Value = R.Dudoso;
                                worksheet.Cells[x + 5, 7].Value = R.Perdida;
                                worksheet.Cells[x + 5, 8].Value = R.Total;
                            }

                            if (R.Posicion == 8)
                            {
                                worksheet.Cells[x + 6, 3].Value = R.Normal;
                                worksheet.Cells[x + 6, 4].Value = R.CPP;
                                worksheet.Cells[x + 6, 5].Value = R.Deficiente;
                                worksheet.Cells[x + 6, 6].Value = R.Dudoso;
                                worksheet.Cells[x + 6, 7].Value = R.Perdida;
                                worksheet.Cells[x + 6, 8].Value = R.Total;
                            }
                        }
                        else if (R.Tipo != 5)
                        {
                            worksheet.Cells[x + R.Posicion, 3].Value = R.Normal;
                            worksheet.Cells[x + R.Posicion, 4].Value = R.CPP;
                            worksheet.Cells[x + R.Posicion, 5].Value = R.Deficiente;
                            worksheet.Cells[x + R.Posicion, 6].Value = R.Dudoso;
                            worksheet.Cells[x + R.Posicion, 7].Value = R.Perdida;
                            worksheet.Cells[x + R.Posicion, 8].Value = R.Total;
                        }
                    }

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



        // GET: api/Anexo5-Deudores
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult> GetAnexo5DeudoresModel([FromQuery] requestCodSucaveDBModel model, [FromQuery] string anio, [FromQuery] string mes)
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
                string storeAnexo5Deudores = $"REPORTES_ANEXOS_SBS.Anexo5A_Sucave {string.Join(", ", parameterNames.Select(n => "@" + n))}";
                List<SucaveDBModel> producto = await _dbContext.SucaveDB.FromSqlRaw(storeAnexo5Deudores, parameters).ToListAsync();
                var contenido = new StringBuilder();
                string codFormato = "1105";
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