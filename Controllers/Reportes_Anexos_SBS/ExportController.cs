using Microsoft.AspNetCore.Mvc;
using System.Text;
using LeonXIIICore.Models.Reportes_Anexos_SBS;
using Clases.Sistema;
using LeonXIIICore.Models.Sistema;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using LeonXIIICore.Controllers.Utilitarios;
using LeonXIIICore.Pages.Reportes_Anexos_SBS.Reporte3;

namespace LeonXIIICore.Controllers.Reportes_Anexos_SBS
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        #region "ANEXO 13"
        [HttpGet("ExportTxtAnexo13")]
        public async Task<IActionResult> ExportTxt([FromQuery] string? codigo, [FromQuery] string anio, [FromQuery] string mes)
        {
            var apiLeon = new APILEON();
            //apiLeon = new APILEON();

            if (codigo == null)
            {
                mes = "00" + Convert.ToString(mes);
                mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;
                var temporal = await apiLeon.APILEONGET<Respuesta<List<CodAnexoDBModel>>>("/api/Reportes_Anexos_SBS/Auditoria/CodAnexo?Operacion=3&NombreTabla=Anexo13&cMes="+mes+"&cAnio="+ anio);
                
                foreach (var item2 in temporal.Data)
                {
                    codigo = item2.Codigo;
                }
            }
            else
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }

            var Anexo13Sucave = await apiLeon.APILEONGET<List<SucaveModel>>("/api/Reportes_Anexos_SBS/Anexo13/DescargarSucave?CodAnexo13=" + codigo);
            var contenido = new StringBuilder();
            
            // CABECERA
            string codFormato = "1113";
            string codAnexo = "02";
            string codEmpresa = "01202";
            string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();  
            string codExpresionMontos = "012";
            string datosControl = "              0";

            string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

            contenido.AppendLine(header);

            foreach (var item in Anexo13Sucave)
            {
                contenido.AppendLine(item.Valor);
            }

            //DEFINIR EL NOMBRE DEL ARCHIVO

            string nameAnexo = "02" + anio.Substring(2, 2) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "111301202.SEI";


            var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
            return File(bytes, "text/plain", nameAnexo);
        
        }

        [HttpGet("ExportExcelAnexo13")]
        public async Task<IActionResult> ExportExcel([FromQuery] string? codigo, [FromQuery] string anio, [FromQuery] string mes, [FromQuery] string IdUser)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            // Ruta a la plantilla
            var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recursos\\Anexo13", "Anexo13.xlsx");

            if (!System.IO.File.Exists(pathPlantilla))
            {
                return NotFound("No se encontró la plantilla.");
            }

            var apiLeon = new APILEON();
            
            
            var datos = await apiLeon.APILEONGET<List<Anexo13DBModel>>($"/api/Reportes_Anexos_SBS/Anexo13/DatosExcel?CodAnexo={codigo}&cMes={mes}&cAnio={anio}&cUsuario={IdUser}");
            
            using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520"; 
            worksheet.Cells[6, 1].Value = "CÓDIGO: 01202"; 

            string fecchaal = Utilitarios.Utilitarios.MesEnLetras(mes) + " DE " + anio.ToString();
            worksheet.Cells[7, 1].Value = fecchaal;

            if (datos != null)
            {
                foreach (Anexo13DBModel Anexo13 in datos)
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
            var fileName = "Anexo 13.xlsx";

            return File(bytes, contentType, fileName);
        }

        [HttpGet("GenerarExcelAnexo13")]
        public async Task<IActionResult> GenerarExcel([FromQuery] string anio, [FromQuery] string mes, [FromQuery] string IdUser)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            mes = "00" + Convert.ToString(mes);
            mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;

            var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recursos\\Anexo13", "Anexo13.xlsx");

            if (!System.IO.File.Exists(pathPlantilla))
            {
                return NotFound("No se encontró la plantilla.");
            }

            var apiLeon = new APILEON();

            requestAnexo13Model model = new requestAnexo13Model();
            model.cMes = mes;
            model.cAnio = anio;
            model.cUsuario = IdUser;

            var datos = await apiLeon.APILEONPOST<RespuestaAnexo13List<Anexo13DBModel>>($"/api/Reportes_Anexos_SBS/Anexo13",model);

            using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
            worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";

            string fecchaal = Utilitarios.Utilitarios.MesEnLetras(mes) + " DE " + anio.ToString();
            worksheet.Cells[7, 1].Value = fecchaal;

            if (datos.data != null)
            {
                foreach (Anexo13DBModel Anexo13 in datos.data)
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
            var fileName = "Anexo 13.xlsx";

            return File(bytes, contentType, fileName);
        }

        #endregion


        #region "REPORTE 3"
        [HttpGet("ExportTxtReporte3")]
        public async Task<IActionResult> ExportReporte3txt([FromQuery] string? codigo, [FromQuery] string anio, [FromQuery] string mes)
        {
            var apiLeon = new APILEON();

            if (codigo == null)
            {
                mes = "00" + Convert.ToString(mes);
                mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;
                var temporal = await apiLeon.APILEONGET<Respuesta<List<CodAnexoDBModel>>>("/api/Reportes_Anexos_SBS/Auditoria/CodAnexo?Operacion=3&NombreTabla=Reporte3&cMes=" + mes + "&cAnio=" + anio);

                foreach (var item2 in temporal.Data)
                {
                    codigo = item2.Codigo;
                }
            }
            else
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }

            var Sucave = await apiLeon.APILEONGET<Respuesta<List<SucaveModel>>>("/api/Reportes_Anexos_SBS/Reporte3?Codigo=" + codigo);
            var contenido = new StringBuilder();

            // CABECERA
            string codFormato = "1203";
            string codAnexo = "01";
            string codEmpresa = "01202";
            string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
            string codExpresionMontos = "012";
            string datosControl = "              0";

            string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

            contenido.AppendLine(header);

            foreach (var item in Sucave.Data)
            {
                contenido.AppendLine(item.Valor);
            }

            string nameAnexo = "01" + anio.Substring(2, 2) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120301202.SEI";

            var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
            return File(bytes, "text/plain", nameAnexo);
        }

        [HttpGet("GenerarExcelReporte3")]
        public async Task<IActionResult> GenerarExcelReporte3([FromQuery] int iOpcion,[FromQuery] string anio, [FromQuery] string mes, [FromQuery] string IdUser, [FromQuery] string? codigo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var apiLeon = new APILEON();
            if (mes.Length > 2)
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }
            mes = "00" + Convert.ToString(mes);
            mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;

            var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recursos\\Reporte3", "Reporte3.xlsx");

            if (!System.IO.File.Exists(pathPlantilla))
            {
                return NotFound("No se encontró la plantilla.");
            }

            requestReporte3Model model = new requestReporte3Model();
            model.iOpcion = iOpcion;
            model.CodReporte3 = codigo;
            model.cMes = mes;
            model.cAnio = anio;
            model.cUsuario = IdUser;


            var datos = await apiLeon.APILEONPOST<Respuesta<List<Reporte3DBModel>>>($"/api/Reportes_Anexos_SBS/Reporte3", model);


            using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
            worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";

            string fecchaal = Utilitarios.Utilitarios.MesEnLetras(mes) + " DE " + anio.ToString();
            worksheet.Cells[7, 1].Value = fecchaal;

            if (datos.Data != null)
            {
                foreach (Reporte3DBModel Reporte3 in datos.Data)
                {
                    worksheet.Cells[Reporte3.NroFila, 3].Value = Reporte3.Monto ?? 0;
                }
            }

            package.Save();

            var bytes = package.GetAsByteArray();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Reporte 3.xlsx";

            return File(bytes, contentType, fileName);
        }
        #endregion

        #region "REPORTE 2A"
        [HttpGet("ExportTxtReporte2A")]
        public async Task<IActionResult> ExportReporte2Atxt([FromQuery] string? codigo, [FromQuery] string anio, [FromQuery] string mes)
        {
            var apiLeon = new APILEON();

            if (codigo == null)
            {
                mes = "00" + Convert.ToString(mes);
                mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;
                var temporal = await apiLeon.APILEONGET<Respuesta<List<CodAnexoDBModel>>>("/api/Reportes_Anexos_SBS/Auditoria/CodAnexo?Operacion=3&NombreTabla=Reporte2A&cMes=" + mes + "&cAnio=" + anio);

                foreach (var item2 in temporal.Data)
                {
                    codigo = item2.Codigo;
                }
            }
            else
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }

            var Sucave = await apiLeon.APILEONGET<Respuesta<List<SucaveModel>>>("/api/Reportes_Anexos_SBS/Reporte2A?Codigo=" + codigo);
            var contenido = new StringBuilder();

            // CABECERA
            string codFormato = "1202";
            string codAnexo = "03";
            string codEmpresa = "01202";
            string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
            string codExpresionMontos = "012";
            string datosControl = "              0";
            
            string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

            contenido.AppendLine(header);

            foreach (var item in Sucave.Data)
            {
                contenido.AppendLine(item.Valor);
            }

            string nameAnexo = "03" + anio.Substring(2, 2) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120201202.SEI";

            var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
            return File(bytes, "text/plain", nameAnexo);
        }

        [HttpGet("GenerarExcelReporte2A")]
        public async Task<IActionResult> GenerarExcelReporte2A([FromQuery] int iOpcion, [FromQuery] string anio, [FromQuery] string mes, [FromQuery] string IdUser, [FromQuery] string? codigo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var apiLeon = new APILEON();

            if(mes.Length > 2)
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }

            mes = "00" + Convert.ToString(mes);
            mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;

            var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recursos\\Reporte2", "Reporte2A.xlsx");

            if (!System.IO.File.Exists(pathPlantilla))
            {
                return NotFound("No se encontró la plantilla.");
            }

            requestReporte2AModel model = new requestReporte2AModel();
            model.iOpcion = iOpcion;
            model.CodReporte2A = codigo;
            model.cMes = mes;
            model.cAnio = anio;
            model.cUsuario = IdUser;


            var datos = await apiLeon.APILEONPOST<Respuesta<List<Reporte2ADBModel>>>($"/api/Reportes_Anexos_SBS/Reporte2A", model);


            using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
            worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";

            string fecchaal = Utilitarios.Utilitarios.MesEnLetras(mes) + " DE " + anio.ToString();
            worksheet.Cells[7, 1].Value = fecchaal;

            if (datos.Data != null)
            {
                foreach (Reporte2ADBModel Reporte2A in datos.Data)
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

        #endregion 

        #region "REPORTE 1"
        [HttpGet("ExportTxtReporte1")]
        public async Task<IActionResult> ExportReporte1txt([FromQuery] string? codigo, [FromQuery] string anio, [FromQuery] string mes)
        {
            var apiLeon = new APILEON();

            if (codigo == null)
            {
                mes = "00" + Convert.ToString(mes);
                mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;
                var temporal = await apiLeon.APILEONGET<Respuesta<List<CodAnexoDBModel>>>("/api/Reportes_Anexos_SBS/Auditoria/CodAnexo?Operacion=3&NombreTabla=Reporte1&cMes=" + mes + "&cAnio=" + anio);

                foreach (var item2 in temporal.Data)
                {
                    codigo = item2.Codigo;
                }
            }
            else
            {
                mes = Utilitarios.Utilitarios.MesEnNúmeros(mes);
            }

            var Sucave = await apiLeon.APILEONGET<Respuesta<List<SucaveModel>>>("/api/Reportes_Anexos_SBS/Reporte1?Codigo=" + codigo);
            var contenido = new StringBuilder();

            // CABECERA
            string codFormato = "1201";
            string codAnexo = "01";
            string codEmpresa = "01202";
            string fechaReporte = Convert.ToString(anio) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString();
            string codExpresionMontos = "012";
            string datosControl = "              0";

            string header = codFormato + codAnexo + codEmpresa + fechaReporte + codExpresionMontos + datosControl;

            contenido.AppendLine(header);

            foreach (var item in Sucave.Data)
            {
                contenido.AppendLine(item.Valor);
            }

            string nameAnexo = "01" + anio.Substring(2, 2) + mes + Utilitarios.Utilitarios.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes)).ToString() + "120201202.SEI";

            var bytes = Encoding.UTF8.GetBytes(contenido.ToString());
            return File(bytes, "text/plain", nameAnexo);
        }

        [HttpGet("GenerarExcelReporte1")]
        public async Task<IActionResult> GenerarExcelReporte1([FromQuery] int iOpcion, [FromQuery] string anio, [FromQuery] string mes, [FromQuery] string IdUser, [FromQuery] string? codigo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var apiLeon = new APILEON();

            mes = "00" + Convert.ToString(mes);
            mes = mes.Length >= 2 ? mes.Substring(mes.Length - 2) : mes;

            var pathPlantilla = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "recursos\\Reporte1", "Reporte1.xlsx");

            if (!System.IO.File.Exists(pathPlantilla))
            {
                return NotFound("No se encontró la plantilla.");
            }

            requestReporte1Model model = new requestReporte1Model();
            model.iOpcion = iOpcion;
            model.CodReporte1 = codigo;
            model.cMes = mes;
            model.cAnio = anio;
            model.cUsuario = IdUser;


            var datos = await apiLeon.APILEONPOST<Respuesta<List<Reporte1DBModel>>>($"/api/Reportes_Anexos_SBS/Reporte1", model);


            using var stream = new FileStream(pathPlantilla, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            worksheet.Cells[5, 1].Value = "EMPRESA: COOPAC LEON XIII LTDA. 520";
            worksheet.Cells[6, 1].Value = "CÓDIGO: 01202";

            string fecchaal = Utilitarios.Utilitarios.MesEnLetras(mes) + " DE " + anio.ToString();
            worksheet.Cells[7, 1].Value = fecchaal;

            if (datos.Data != null)
            {
                int i = 12;
                foreach (Reporte1DBModel Reporte1 in datos.Data)
                {
                    worksheet.Cells[i, 1].Value = Reporte1.NroFila;
                    worksheet.Cells[i, 2].Value = Reporte1.TipoDocumento;
                    worksheet.Cells[i, 3].Value = Reporte1.NumeroDocumento;
                    worksheet.Cells[i, 4].Value = Reporte1.TipoPersona;
                    worksheet.Cells[i, 5].Value = Reporte1.ApeNomRazonSocial;
                    worksheet.Cells[i, 6].Value = Reporte1.Nacionalidad;
                    worksheet.Cells[i, 7].Value = Reporte1.Genero;
                    worksheet.Cells[i, 8].Value = Reporte1.Domicilio;
                    worksheet.Cells[i, 9].Value = Reporte1.UbigeoDomic;
                    worksheet.Cells[i, 10].Value = Reporte1.AportePagado;
                    worksheet.Cells[i, 11].Value = Reporte1.AporteSuscrito;

                    //worksheet.Cells[14, 4].Value = Reporte1.f1 ?? 0;
                    //worksheet.Cells[19, 4].Value = Reporte2.f2 ?? 0;
                    //worksheet.Cells[20, 4].Value = Reporte2A.f3 ?? 0;
                    //worksheet.Cells[21, 4].Value = Reporte2A.f4 ?? 0;
                    //worksheet.Cells[20, 5].Value = Reporte2A.f5 ?? 0;
                    //worksheet.Cells[21, 5].Value = Reporte2A.f6 ?? 0;
                    //worksheet.Cells[21, 9].Value = Reporte2A.f7 ?? 0;
                    //worksheet.Cells[23, 15].Value = Reporte2A.f8 ?? 0;
                    i++;
                }
            }

            package.Save();

            var bytes = package.GetAsByteArray();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Reporte 1.xlsx";

            return File(bytes, contentType, fileName);
        }

        #endregion 



    }
}
