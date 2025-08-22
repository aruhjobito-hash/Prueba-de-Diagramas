using ApiAppLeon.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAppLeon.Controllers.Utilitarios
{
    public class PDFGeneradorController : Controller
    {
        private readonly PdfService _pdfService;

        public PDFGeneradorController()
        {
            _pdfService = new PdfService(); // You could inject this as a service later
        }
        [ApiExplorerSettings(GroupName = "Utilitarios")]
        [HttpGet("api/Utilitarios/PDFBase64")]
        public IActionResult GetPdfAsBase64()
        {
            var base64Pdf = _pdfService.GeneratePdfBase64();
            return Ok(new { pdfBase64 = base64Pdf });
        }

        [ApiExplorerSettings(GroupName = "Utilitarios")]
        [HttpPost("api/Utilitarios/generatePDF")]
        public IActionResult Generate([FromBody] PdfDocumentData data)
        {

            var base64 = _pdfService.GeneratePdfBase64(data);
            return Ok(new { pdfBase64 = base64 });
        }
    }
}
