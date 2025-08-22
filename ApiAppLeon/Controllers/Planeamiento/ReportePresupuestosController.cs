
// Developer    : migzav  
// DateCreate   : 10/05/2025
// Description  : Controlador Mantenedor Reporte de Presupuestos
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models.Planeamiento;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Drawing;
using iText.Commons.Actions.Contexts;

namespace Planeamiento.Controllers
{
    [ApiExplorerSettings(GroupName = "Planeamiento")]
    [Route("api/Planeamiento/[controller]")]
    [ApiController]
    public class ReportePresupuestosController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public ReportePresupuestosController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/ReportePresupuestos
        [HttpPost("ExportarComparativoMensual")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]



        
        public async Task<IActionResult> ExportarComparativoMensual([FromBody] ComparativoReporteGlobalRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model inválido.");
            }

            try
            {

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var data = await _dbContext.ReporteComparativoMensualPresupuestosDB
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobal @IdAgencia, @IdArea, @Año, @Digitos, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo",
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo)
                    ).ToListAsync();

                if (data == null || data.Count == 0)
                    return NotFound("No se encontraron datos.");

                var año = model.Año;
                var mesDesdeInt = int.Parse(model.MesDesde);
                var mesHastaInt = int.Parse(model.MesHasta);

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Comparativo Mensual");

                int startRow = 5;
                ws.Cells[2, 2].Value = $"REPORTE DE COMPARATIVO MENSUAL - AÑO: {año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                string nombreMesDesde = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesDesdeInt).ToUpper();
                string nombreMesHasta = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesHastaInt).ToUpper();

                ws.Cells[3, 2].Value = $"PERIODO: {nombreMesDesde} - {nombreMesHasta}";
                ws.Cells[3, 2].Style.Font.Bold = true;
                ws.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int headerRow1 = startRow;
                int headerRow2 = startRow + 1;

                ws.Cells[headerRow1, 1].Value = "Cuenta Contable";
                ws.Cells[headerRow1, 2].Value = "Nombre de Cuenta";
                ws.Cells[headerRow1, 1, headerRow2, 2].Merge = true;

                int currentDataCol = 3;
                for (int i = mesDesdeInt; i <= mesHastaInt; i++)
                {
                    string mesNombre = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).ToUpper();
                    ws.Cells[headerRow1, currentDataCol].Value = mesNombre;
                    ws.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;

                    ws.Cells[headerRow2, currentDataCol].Value = $"P{i:00}";
                    ws.Cells[headerRow2, currentDataCol + 1].Value = $"E{i:00}";
                    ws.Cells[headerRow2, currentDataCol + 2].Value = $"D{i:00}";
                    ws.Cells[headerRow2, currentDataCol + 3].Value = $"V{i:00}";

                    currentDataCol += 4;
                }

                ws.Cells[headerRow1, currentDataCol].Value = "TOTALES";
                ws.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;

                ws.Cells[headerRow2, currentDataCol].Value = "PT";
                ws.Cells[headerRow2, currentDataCol + 1].Value = "ET";
                ws.Cells[headerRow2, currentDataCol + 2].Value = "DT";
                ws.Cells[headerRow2, currentDataCol + 3].Value = "VT";

                using (var range = ws.Cells[headerRow1, 1, headerRow2, currentDataCol + 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                int rowIdx = headerRow2 + 1;
                foreach (var item in data)
                {
                    ws.Cells[rowIdx, 1].Value = item.CtaContable;
                    ws.Cells[rowIdx, 2].Value = item.CtaNombre;

                    int colIdx = 3;
                    for (int i = mesDesdeInt; i <= mesHastaInt; i++)
                    {
                        var p = item.GetType().GetProperty($"P{i:00}")?.GetValue(item) as decimal?;
                        var e = item.GetType().GetProperty($"E{i:00}")?.GetValue(item) as decimal?;
                        var d = item.GetType().GetProperty($"D{i:00}")?.GetValue(item) as decimal?;
                        var v = item.GetType().GetProperty($"V{i:00}")?.GetValue(item) as decimal?;

                        ws.Cells[rowIdx, colIdx++].Value = p;
                        ws.Cells[rowIdx, colIdx++].Value = e;
                        ws.Cells[rowIdx, colIdx++].Value = d;
                        ws.Cells[rowIdx, colIdx++].Value = v;
                    }

                    ws.Cells[rowIdx, colIdx++].Value = item.PT;
                    ws.Cells[rowIdx, colIdx++].Value = item.ET;
                    ws.Cells[rowIdx, colIdx++].Value = item.DT;
                    ws.Cells[rowIdx, colIdx++].Value = item.VT;

                    rowIdx++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var bytes = await package.GetAsByteArrayAsync();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"ReporteComparativoMensual_{año}_{model.MesDesde}-{model.MesHasta}.xlsx";

                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar Excel: {ex.Message}");
            }
        }


        [HttpPost("ExportarComparativoTrimestral")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoTrimestral([FromBody] ComparativoReporteGlobalRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model inválido.");
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var data = await _dbContext.ReporteComparativoTrimestralPresupuestosDB
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobal @IdAgencia, @IdArea,@Año, @Digitos,@TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo",
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo)
                    ).ToListAsync();

                if (data == null || data.Count == 0)
                    return NotFound("No se encontraron datos.");

                int trimestreDesdeInt = model.TrimestreDesde ?? 1;
                int trimestreHastaInt = model.TrimestreHasta ?? 4;
                var año = model.Año;

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Comparativo Trimestral");

                int startDataRow = 5;

                worksheet.Cells[2, 2].Value = $"REPORTE DE COMPARATIVO TRIMESTRAL - AÑO: {año}";
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Style.Font.Size = 18;
                worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[3, 2].Value = $"PERIODO: TRIMESTRE {trimestreDesdeInt} - TRIMESTRE {trimestreHastaInt}";
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int headerRow1 = startDataRow;
                int headerRow2 = startDataRow + 1;

                worksheet.Cells[headerRow1, 1].Value = "Cuenta Contable";
                worksheet.Cells[headerRow1, 2].Value = "Nombre de Cuenta";
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Merge = true;

                int currentDataCol = 3;
                for (int i = trimestreDesdeInt; i <= trimestreHastaInt; i++)
                {
                    worksheet.Cells[headerRow1, currentDataCol].Value = $"TRIMESTRE {i}";
                    worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;

                    worksheet.Cells[headerRow2, currentDataCol].Value = $"P{i}";
                    worksheet.Cells[headerRow2, currentDataCol + 1].Value = $"T{i}";
                    worksheet.Cells[headerRow2, currentDataCol + 2].Value = $"D{i}";
                    worksheet.Cells[headerRow2, currentDataCol + 3].Value = $"V{i}";

                    currentDataCol += 4;
                }

                worksheet.Cells[headerRow1, currentDataCol].Value = "TOTALES";
                worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;
                worksheet.Cells[headerRow2, currentDataCol].Value = "PT";
                worksheet.Cells[headerRow2, currentDataCol + 1].Value = "ET";
                worksheet.Cells[headerRow2, currentDataCol + 2].Value = "DT";
                worksheet.Cells[headerRow2, currentDataCol + 3].Value = "VT";

          
                using (var range = worksheet.Cells[headerRow1, 1, headerRow2, currentDataCol + 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                int rowIdx = headerRow2 + 1;
                foreach (var item in data)
                {
                    worksheet.Cells[rowIdx, 1].Value = item.CtaContable;
                    worksheet.Cells[rowIdx, 2].Value = item.CtaNombre;

                    int colIdx = 3;
                    for (int i = trimestreDesdeInt; i <= trimestreHastaInt; i++)
                    {
                        var p = item.GetType().GetProperty($"P{i:00}")?.GetValue(item) as decimal?;
                        var t = item.GetType().GetProperty($"T{i}")?.GetValue(item) as decimal?;
                        var d = item.GetType().GetProperty($"D{i:00}")?.GetValue(item) as decimal?;
                        var v = item.GetType().GetProperty($"V{i:00}")?.GetValue(item) as decimal?;

                        worksheet.Cells[rowIdx, colIdx++].Value = p;
                        worksheet.Cells[rowIdx, colIdx++].Value = t;
                        worksheet.Cells[rowIdx, colIdx++].Value = d;
                        worksheet.Cells[rowIdx, colIdx++].Value = v;
                    }

                    worksheet.Cells[rowIdx, colIdx++].Value = item.PT;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.ET;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.DT;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.VT;

                    rowIdx++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var bytes = await package.GetAsByteArrayAsync();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"ReporteComparativoTrimestral_{año}_T{trimestreDesdeInt}-T{trimestreHastaInt}.xlsx";

                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar Excel: {ex.Message}");
            }
        }

        [HttpPost("ExportarComparativoSemestral")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoSemestral([FromBody] ComparativoReporteGlobalRequest model)
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var año = int.TryParse(model.Año, out int parsedAño) ? parsedAño : DateTime.Now.Year;
                int semestreDesdeInt = model.SemestreDesde ?? 1;
                int semestreHastaInt = model.SemestreHasta ?? 1;


                string query = "EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobal  @IdAgencia, @IdArea,@Año, @Digitos,@TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo";
                var result = await _dbContext.ReporteComparativoSemestralPresupuestosDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde ?? ""),
                        new SqlParameter("@MesHasta", model.MesHasta ?? ""),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", semestreDesdeInt),
                        new SqlParameter("@SemestreHasta", semestreHastaInt),
                        new SqlParameter("@Tipo", model.Tipo ?? "")
                    ).ToListAsync();

                if (result == null || result.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para el reporte.",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "Sin datos" }
                    });
                }

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Comparativo Semestral");

                int startDataRow = 5;

                worksheet.Cells[2, 2].Value = $"REPORTE DE COMPARATIVO SEMESTRAL - AÑO: {año}";
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Style.Font.Size = 18;
                worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[3, 2].Value = $"PERIODO: SEMESTRE {semestreDesdeInt} - SEMESTRE {semestreHastaInt}";
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int headerRow1 = startDataRow;
                int headerRow2 = startDataRow + 1;

                worksheet.Cells[headerRow1, 1].Value = "Cuenta Contable";
                worksheet.Cells[headerRow1, 2].Value = "Nombre de Cuenta";
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Merge = true;
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Style.Font.Bold = true;
                worksheet.Cells[headerRow1, 1, headerRow2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                int currentDataCol = 3;

                for (int i = semestreDesdeInt; i <= semestreHastaInt; i++)
                {
                    worksheet.Cells[headerRow1, currentDataCol].Value = $"SEMESTRE {i}";
                    worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;
                    worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.Font.Bold = true;
                    worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    currentDataCol += 4;
                }

                worksheet.Cells[headerRow1, currentDataCol].Value = "TOTALES";
                worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Merge = true;
                worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.Font.Bold = true;
                worksheet.Cells[headerRow1, currentDataCol, headerRow1, currentDataCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                currentDataCol = 3;
                for (int i = semestreDesdeInt; i <= semestreHastaInt; i++)
                {
                    worksheet.Cells[headerRow2, currentDataCol++].Value = $"P{i:00}";
                    worksheet.Cells[headerRow2, currentDataCol++].Value = $"S{i}";
                    worksheet.Cells[headerRow2, currentDataCol++].Value = $"D{i:00}";
                    worksheet.Cells[headerRow2, currentDataCol++].Value = $"V{i:00}";
                }

                worksheet.Cells[headerRow2, currentDataCol++].Value = "PT";
                worksheet.Cells[headerRow2, currentDataCol++].Value = "ET";
                worksheet.Cells[headerRow2, currentDataCol++].Value = "DT";
                worksheet.Cells[headerRow2, currentDataCol++].Value = "VT";

           
                using (var range = worksheet.Cells[headerRow1, 1, headerRow2, currentDataCol - 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }

                int rowIdx = headerRow2 + 1;

                foreach (var item in result)
                {
                    worksheet.Cells[rowIdx, 1].Value = item.CtaContable;
                    worksheet.Cells[rowIdx, 2].Value = item.CtaNombre;

                    int colIdx = 3;

                    for (int i = semestreDesdeInt; i <= semestreHastaInt; i++)
                    {
                        worksheet.Cells[rowIdx, colIdx++].Value = item.GetType().GetProperty($"P{i:00}")?.GetValue(item);
                        worksheet.Cells[rowIdx, colIdx++].Value = item.GetType().GetProperty($"S{i}")?.GetValue(item);
                        worksheet.Cells[rowIdx, colIdx++].Value = item.GetType().GetProperty($"D{i:00}")?.GetValue(item);
                        worksheet.Cells[rowIdx, colIdx++].Value = item.GetType().GetProperty($"V{i:00}")?.GetValue(item);
                    }

                    worksheet.Cells[rowIdx, colIdx++].Value = item.PT;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.ET;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.DT;
                    worksheet.Cells[rowIdx, colIdx++].Value = item.VT;

                    rowIdx++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var bytes = await package.GetAsByteArrayAsync();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"ReporteComparativoSemestral_{año}_S{semestreDesdeInt}-S{semestreHastaInt}.xlsx";

                return File(bytes, contentType, fileName);
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


        [HttpPost("ExportarPresupuestado")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarPresupuestado([FromBody] requestTipoReporteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Validación fallida del modelo" }
                });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var año = model.Año;
                var mesDesde = int.TryParse(model.MesDesde, out var md) ? md : 1;
                var mesHasta = int.TryParse(model.MesHasta, out var mh) ? mh : 12;

                string query = "EXEC PRESUPUESTO_ANUAL.TipoReporte @IdAgencia, @IdArea,@Año, @Digitos,@TipoReporte, @MesDesde, @MesHasta, @Tipo, @TipoMoneda";

                var result = await _dbContext.ReportePresupuestadoPresupuestosDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia ?? ""),
                        new SqlParameter("@IdArea", model.IdArea ?? ""),
                        new SqlParameter("@Año", año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoReporte", model.TipoReporte ?? ""),
                        new SqlParameter("@MesDesde", mesDesde),
                        new SqlParameter("@MesHasta", mesHasta),
                        new SqlParameter("@Tipo", model.Tipo ?? ""),
                        new SqlParameter("@TipoMoneda", model.TipoMoneda ?? "")
                    ).ToListAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "Datos no encontrados" }
                    });
                }

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Presupuestado");
                int startDataRow = 5;

                worksheet.Cells[2, 2].Value = $"REPORTE PRESUPUESTADO - AÑO: {año}";
                worksheet.Cells[2, 2, 2, 10].Merge = true;
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Style.Font.Size = 18;
                worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                string nombreMesDesde = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesDesde).ToUpper();
                string nombreMesHasta = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesHasta).ToUpper();

                worksheet.Cells[3, 2].Value = $"PERIODO: {nombreMesDesde} - {nombreMesHasta}";
                worksheet.Cells[3, 2, 3, 7].Merge = true;
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerRow = startDataRow;
                var colIdx = 1;
                worksheet.Cells[headerRow, colIdx++].Value = "Cuenta Contable";
                worksheet.Cells[headerRow, colIdx++].Value = "Nombre de Cuenta";

                var mesesMap = new Dictionary<int, string>
                    {
                        {1, "Enero"}, {2, "Febrero"}, {3, "Marzo"}, {4, "Abril"},
                        {5, "Mayo"}, {6, "Junio"}, {7, "Julio"}, {8, "Agosto"},
                        {9, "Septiembre"}, {10, "Octubre"}, {11, "Noviembre"}, {12, "Diciembre"}
                    };

                var mesesEnRango = mesesMap
                    .Where(k => k.Key >= mesDesde && k.Key <= mesHasta)
                    .OrderBy(k => k.Key)
                    .Select(k => k.Value)
                    .ToList();

                foreach (var mes in mesesEnRango)
                {
                    worksheet.Cells[headerRow, colIdx++].Value = mes;
                }

                worksheet.Cells[headerRow, colIdx].Value = "Total Presupuesto";

                using (var range = worksheet.Cells[headerRow, 1, headerRow, colIdx])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int currentRow = headerRow + 1;

                foreach (var item in result)
                {
                    int col = 1;
                    worksheet.Cells[currentRow, col++].Value = item.CtaContable;
                    worksheet.Cells[currentRow, col++].Value = item.CtaNombre;

                    foreach (var mes in mesesEnRango)
                    {
                        var valor = (decimal?)item.GetType().GetProperty(mes)?.GetValue(item);
                        worksheet.Cells[currentRow, col].Value = valor ?? 0;
                        worksheet.Cells[currentRow, col].Style.Numberformat.Format = "#,##0.00";
                        col++;
                    }

                    worksheet.Cells[currentRow, col].Value = item.TotalPresupuesto;
                    worksheet.Cells[currentRow, col].Style.Numberformat.Format = "#,##0.00";

                    currentRow++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var bytes = package.GetAsByteArray();
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReportePresupuestado_{año}_{mesDesde}-{mesHasta}.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPost("ExportarEjecutado")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarEjecutado([FromBody] requestTipoReporteModel model)
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string query = "EXEC PRESUPUESTO_ANUAL.TipoReporte @IdAgencia, @IdArea, @Año, @Digitos, @TipoReporte, @MesDesde, @MesHasta, @Tipo, @TipoMoneda";
                var data = await _dbContext.ReporteEjecutadoPresupuestosDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoReporte", model.TipoReporte),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@Tipo", model.Tipo),
                        new SqlParameter("@TipoMoneda", model.TipoMoneda)
                    ).ToListAsync();

                if (data == null || !data.Any())
                {
                    return StatusCode(500, "No se encontraron datos para el reporte ejecutado.");
                }

                int mesDesdeInt = int.TryParse(model.MesDesde, out int md) ? md : 1;
                int mesHastaInt = int.TryParse(model.MesHasta, out int mh) ? mh : 12;

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Ejecutado");

                int startDataRow = 5;

                worksheet.Cells[2, 2].Value = $"REPORTE EJECUTADO - AÑO: {model.Año}";
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Style.Font.Size = 18;
                worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

              
                string nombreMesDesde = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesDesdeInt).ToUpper();
                string nombreMesHasta = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesHastaInt).ToUpper();

                worksheet.Cells[3, 2].Value = $"PERIODO: {nombreMesDesde} - {nombreMesHasta}";
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        
                int col = 1;
                worksheet.Cells[startDataRow, col++].Value = "Cuenta Contable";
                worksheet.Cells[startDataRow, col++].Value = "Descripción";

 
                for (int i = 1; i <= 12; i++)
                {
                    worksheet.Cells[startDataRow, col++].Value = $"E{i:D2}";
                }

                using (var range = worksheet.Cells[startDataRow, 1, startDataRow, col - 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int row = startDataRow + 1;
                foreach (var item in data)
                {
                    col = 1;
                    worksheet.Cells[row, col++].Value = item.CtaContable;
                    worksheet.Cells[row, col++].Value = item.Descripcion;
                    worksheet.Cells[row, col++].Value = item.E01;
                    worksheet.Cells[row, col++].Value = item.E02;
                    worksheet.Cells[row, col++].Value = item.E03;
                    worksheet.Cells[row, col++].Value = item.E04;
                    worksheet.Cells[row, col++].Value = item.E05;
                    worksheet.Cells[row, col++].Value = item.E06;
                    worksheet.Cells[row, col++].Value = item.E07;
                    worksheet.Cells[row, col++].Value = item.E08;
                    worksheet.Cells[row, col++].Value = item.E09;
                    worksheet.Cells[row, col++].Value = item.E10;
                    worksheet.Cells[row, col++].Value = item.E11;
                    worksheet.Cells[row, col++].Value = item.E12;
                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var bytes = await package.GetAsByteArrayAsync();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = $"ReporteEjecutado_{model.Año}_{model.MesDesde}-{model.MesHasta}.xlsx";

                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error al generar el reporte Excel" }
                });
            }
        }


        [HttpPost("ExportarComparativoAcumulativo")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoAcumulativo([FromBody] ComparativoReporteGlobalRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Validación fallida del modelo" }
                });
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var año = model.Año;
                var mesDesdeInt = int.Parse(model.MesDesde);
                var mesHastaInt = int.Parse(model.MesHasta);

                var result = await _dbContext.ReporteComparativoAcumulativoPresupuestosDB
                    .FromSqlRaw("EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobal @IdAgencia, @IdArea, @Año, @Digitos, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo",
                        new SqlParameter("@IdAgencia", model.IdAgencia ?? ""),
                        new SqlParameter("@IdArea", model.IdArea ?? ""),
                        new SqlParameter("@Año", año),
                        new SqlParameter("@Digitos", model.Digitos),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo ?? ""),
                        new SqlParameter("@MesDesde", mesDesdeInt),
                        new SqlParameter("@MesHasta", mesHastaInt),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde ?? 0),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta ?? 0),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde ?? 0),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta ?? 0),
                        new SqlParameter("@Tipo", model.Tipo ?? "")
                    ).ToListAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "Datos no encontrados" }
                    });
                }

                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Comparativo Acumulativo");

                int startDataRow = 5;

       
                worksheet.Cells[2, 2].Value = $"REPORTE COMPARATIVO ACUMULATIVO - AÑO: {model.Año}";
                worksheet.Cells[2, 2, 2, 7].Merge = true;
                worksheet.Cells[2, 2].Style.Font.Size = 18;
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                string nombreMesDesde = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesDesdeInt).ToUpper();
                string nombreMesHasta = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mesHastaInt).ToUpper();

                worksheet.Cells[3, 2].Value = $"PERIODO: {nombreMesDesde} - {nombreMesHasta}";
                worksheet.Cells[3, 2, 3, 7].Merge = true;
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            
                var headers = new[] { "Cuenta", "Nombre", "Presupuestado", "Ejecutado", "Diferencia", "Variación %" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[startDataRow, i + 2].Value = headers[i];
                    worksheet.Cells[startDataRow, i + 2].Style.Font.Bold = true;
                    worksheet.Cells[startDataRow, i + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[startDataRow, i + 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    worksheet.Cells[startDataRow, i + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Column(i + 2).Width = 18;
                }

            
                int currentRow = startDataRow + 1;
                foreach (var item in result)
                {
                    worksheet.Cells[currentRow, 2].Value = item.CtaContable;
                    worksheet.Cells[currentRow, 3].Value = item.CtaNombre;
                    worksheet.Cells[currentRow, 4].Value = item.P01;
                    worksheet.Cells[currentRow, 5].Value = item.E01;
                    worksheet.Cells[currentRow, 6].Value = item.D01;

                    
                    worksheet.Cells[currentRow, 7].Value = item.V01;
                    worksheet.Cells[currentRow, 7].Style.Numberformat.Format = "0.00%";

                    currentRow++;
                }


                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var excelBytes = package.GetAsByteArray();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ComparativoAcumulativo.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPost("ExportarPresupuestadoInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarPresupuestadoInversiones([FromBody] requestPresupuestadoInversionesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.sp_BuscarPresupuestoInversiones @Año, @IdAgencia, @IdArea, @TipMoneda";

                var data = await _dbContext.PresupuestosInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@TipMoneda", model.TipMoneda))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Presupuesto Inversiones");

                ws.Cells[2, 2].Value = $"REPORTE PRESUPUESTADO INVERSIONES MENSUAL - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;
                string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                var mesDesdeInt = int.Parse(model.MesDesde);
                var mesHastaInt = int.Parse(model.MesHasta);



                int mesDesde = mesDesdeInt - 1; 
                int mesHasta = mesHastaInt - 1;

                string[] headers = new[] { "IdArticulo", "Cant", "NR", "IdUnd", "Und", "Concepto" }
                    .Concat(meses.Skip(mesDesde).Take(mesHasta - mesDesde + 1))
                    .Concat(new[] { "TotalPresupuestado" }).ToArray();

                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.Cant;
                    ws.Cells[row, col++].Value = item.NR;
                    ws.Cells[row, col++].Value = item.IdUnd;
                    ws.Cells[row, col++].Value = item.Und;
                    ws.Cells[row, col++].Value = item.Concepto;

                    decimal total = 0;
                    foreach (var mes in meses.Skip(mesDesde).Take(mesHasta - mesDesde + 1))
                    {
                        var valor = (decimal)(item.GetType().GetProperty(mes)?.GetValue(item) ?? 0m);
                        total += valor;
                        ws.Cells[row, col++].Value = valor;
                    }

                    ws.Cells[row, col].Value = total;
                    row++;
                }


                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"PresupuestoInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPost("ExportarEjecutadoInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarEjecutadoInversiones([FromBody] requestPresupuestadoInversionesModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.BuscarPresupuestoEjecutadoInversiones @Año, @IdAgencia, @IdArea, @TipMoneda";

                var data = await _dbContext.ReporteEjecutadoInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@TipMoneda", model.TipMoneda))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Ejecutado Inversiones");

                ws.Cells[2, 2].Value = $"REPORTE EJECUTADO INVERSIONES MENSUAL - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;


                string[] meses = {
                    "EneroE", "FebreroE", "MarzoE", "AbrilE", "MayoE", "JunioE",
                    "JulioE", "AgostoE", "SeptiembreE", "OctubreE", "NoviembreE", "DiciembreE"
                };

                string[] nombresMesesVisibles = {
                    "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                    "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
                };

                var mesDesdeInt = int.Parse(model.MesDesde);
                var mesHastaInt = int.Parse(model.MesHasta);
                int mesDesde = mesDesdeInt - 1;
                int mesHasta = mesHastaInt - 1;


                string[] headers = new[] { "IdArticulo", "Cant", "NR", "NombreArticulo" }
                    .Concat(nombresMesesVisibles.Skip(mesDesde).Take(mesHasta - mesDesde + 1))
                    .Concat(new[] { "TotalEjecutado" })
                    .ToArray();


                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }


                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.Cant;
                    ws.Cells[row, col++].Value = item.NR;
                    ws.Cells[row, col++].Value = item.NombreArticulo;

                    decimal total = 0;
                    foreach (var mes in meses.Skip(mesDesde).Take(mesHasta - mesDesde + 1))
                    {
                        var valor = (decimal)(item.GetType().GetProperty(mes)?.GetValue(item) ?? 0m);
                        total += valor;
                        ws.Cells[row, col++].Value = valor;
                    }

                    ws.Cells[row, col].Value = total;
                    row++;
                }



                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"EjecutadoInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPost("ExportarComparativoMensualInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoMensualInversiones([FromBody] ComparativoInversionesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobalInversiones @IdAgencia, @IdArea, @Año, @TipMoneda, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo";

                var data = await _dbContext.ReporteComparativoMensualInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@TipMoneda", model.TipMoneda),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Mensual Inversiones");

                ws.Cells[2, 2].Value = $"COMPARATIVO INVERSIONES MENSUAL - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;

                string[] meses = new[]
                        {
                    "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                    "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
                };

                int mesDesde = int.Parse(model.MesDesde ?? "1") - 1;
                int mesHasta = int.Parse(model.MesHasta ?? "12") - 1;

                var headers = new List<string> { "IdArticulo", "NombreArticulo", "CantP", "CantE", "NRp", "NRe" };

                for (int i = mesDesde; i <= mesHasta; i++)
                {
                    string mes = meses[i];
                    headers.Add($"{mes}P");
                    headers.Add($"{mes}E");
                    headers.Add($"{mes}D");
                    headers.Add($"{mes}V");
                }

                for (int i = 0; i < headers.Count; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                }

                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.NombreArticulo;
                    ws.Cells[row, col++].Value = item.CantP;
                    ws.Cells[row, col++].Value = item.CantE;
                    ws.Cells[row, col++].Value = item.NRp;
                    ws.Cells[row, col++].Value = item.NRe;

                    for (int i = mesDesde; i <= mesHasta; i++)
                    {
                        string mes = meses[i];
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"{mes}P")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"{mes}E")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"{mes}D")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"{mes}V")?.GetValue(item) ?? 0m;
                    }

                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"ComparativoMensualInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }



        [HttpPost("ExportarComparativoTrimestralInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoTrimestralInversiones([FromBody] ComparativoInversionesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobalInversiones @IdAgencia, @IdArea, @Año, @TipMoneda, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo";

                var data = await _dbContext.ReporteComparativoTrimestralInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@TipMoneda", model.TipMoneda),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Trimestral Inversiones");

                ws.Cells[2, 2].Value = $"COMPARATIVO INVERSIONES TRIMESTRAL - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;

                var headers = new List<string> { "IdArticulo", "NombreArticulo", "CantP", "CantE", "NRp", "NRe" };


                int trimestreDesde = model.TrimestreDesde ?? 1;
                int trimestreHasta = model.TrimestreHasta ?? 4;

                for (int t = trimestreDesde; t <= trimestreHasta; t++)
                {
                    headers.Add($"T{t}P");
                    headers.Add($"T{t}E");
                    headers.Add($"T{t}D");
                    headers.Add($"T{t}V");
                }

                for (int i = 0; i < headers.Count; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                }

                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.NombreArticulo;
                    ws.Cells[row, col++].Value = item.CantP;
                    ws.Cells[row, col++].Value = item.CantE;
                    ws.Cells[row, col++].Value = item.NRp;
                    ws.Cells[row, col++].Value = item.NRe;

                    for (int t = trimestreDesde; t <= trimestreHasta; t++)
                    {
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Trimestre{t}P")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Trimestre{t}E")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Trimestre{t}D")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Trimestre{t}V")?.GetValue(item) ?? 0m;
                    }

                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"ComparativoTrimestralInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }



        [HttpPost("ExportarComparativoSemestralInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoSemestralInversiones([FromBody] ComparativoInversionesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobalInversiones @IdAgencia, @IdArea, @Año, @TipMoneda, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo";


                var data = await _dbContext.ReporteComparativoSemestralInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@TipMoneda", model.TipMoneda),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Semestral Inversiones");

                ws.Cells[2, 2].Value = $"COMPARATIVO INVERSIONES SEMESTRAL - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;

                var headers = new List<string> { "IdArticulo", "NombreArticulo", "CantP", "CantE", "NRp", "NRe" };

                int semestreDesde = model.SemestreDesde ?? 1;
                int semestreHasta = model.SemestreHasta ?? 2;

                for (int s = semestreDesde; s <= semestreHasta; s++)
                {
                    headers.Add($"S{s}P");
                    headers.Add($"S{s}E");
                    headers.Add($"S{s}D");
                    headers.Add($"S{s}V");
                }

                for (int i = 0; i < headers.Count; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                }

                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.NombreArticulo;
                    ws.Cells[row, col++].Value = item.CantP;
                    ws.Cells[row, col++].Value = item.CantE;
                    ws.Cells[row, col++].Value = item.NRp;
                    ws.Cells[row, col++].Value = item.NRe;

                    for (int s = semestreDesde; s <= semestreHasta; s++)
                    {
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Sem{s}P")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Sem{s}E")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Sem{s}D")?.GetValue(item) ?? 0m;
                        ws.Cells[row, col++].Value = item.GetType().GetProperty($"Sem{s}V")?.GetValue(item) ?? 0m;
                    }

                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"ComparativoSemestralInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }


        [HttpPost("ExportarComparativoAcumulativoInversiones")]
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> ExportarComparativoAcumulativoInversiones([FromBody] ComparativoInversionesRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = "Modelo inválido",
                    Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" }
                });
            }

            try
            {
                var query = "EXEC PRESUPUESTO_ANUAL.ComparativoReporteGlobalInversiones @IdAgencia, @IdArea, @Año, @TipMoneda, @TipoComparativo, @MesDesde, @MesHasta, @TrimestreDesde, @TrimestreHasta, @SemestreDesde, @SemestreHasta, @Tipo";

                var data = await _dbContext.ReporteAcumulativoInversionesDB
                    .FromSqlRaw(query,
                        new SqlParameter("@IdAgencia", model.IdAgencia),
                        new SqlParameter("@IdArea", model.IdArea),
                        new SqlParameter("@Año", model.Año),
                        new SqlParameter("@TipMoneda", model.TipMoneda),
                        new SqlParameter("@TipoComparativo", model.TipoComparativo),
                        new SqlParameter("@MesDesde", model.MesDesde),
                        new SqlParameter("@MesHasta", model.MesHasta),
                        new SqlParameter("@TrimestreDesde", model.TrimestreDesde),
                        new SqlParameter("@TrimestreHasta", model.TrimestreHasta),
                        new SqlParameter("@SemestreDesde", model.SemestreDesde),
                        new SqlParameter("@SemestreHasta", model.SemestreHasta),
                        new SqlParameter("@Tipo", model.Tipo))
                    .ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new Respuesta<ErrorTxA>
                    {
                        Exito = 0,
                        Mensaje = "No se encontraron datos para exportar",
                        Data = new ErrorTxA { codigo = "02", Mensaje = "No data found" }
                    });
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Acumulativo Inversiones");

                // Título
                ws.Cells[2, 2].Value = $"COMPARATIVO INVERSIONES ACUMULATIVO - AÑO: {model.Año}";
                ws.Cells[2, 2].Style.Font.Bold = true;
                ws.Cells[2, 2].Style.Font.Size = 18;
                ws.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 5;
                var headers = new List<string>
        {
            "IdArticulo", "NombreArticulo", "CantP", "CantE",
            "Total Presupuestado", "Total Ejecutado", "Diferencia", "Variación"
        };

                for (int i = 0; i < headers.Count; i++)
                {
                    ws.Cells[startRow, i + 1].Value = headers[i];
                    ws.Cells[startRow, i + 1].Style.Font.Bold = true;
                    ws.Cells[startRow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[startRow, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                }

                int row = startRow + 1;
                foreach (var item in data)
                {
                    int col = 1;
                    ws.Cells[row, col++].Value = item.IdArticulo;
                    ws.Cells[row, col++].Value = item.NombreArticulo;
                    ws.Cells[row, col++].Value = item.CantP;
                    ws.Cells[row, col++].Value = item.CantE;
                    ws.Cells[row, col++].Value = item.TotalPresupuestado;
                    ws.Cells[row, col++].Value = item.TotalEjecutado;
                    ws.Cells[row, col++].Value = item.Diferencia;
                    ws.Cells[row, col++].Value = item.Variacion;
                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                var fileBytes = package.GetAsByteArray();
                var fileName = $"ComparativoAcumulativoInversiones_{model.Año}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA>
                {
                    Exito = 0,
                    Mensaje = ex.Message,
                    Data = new ErrorTxA { codigo = "03", Mensaje = "Error interno del servidor" }
                });
            }
        }



    }
}