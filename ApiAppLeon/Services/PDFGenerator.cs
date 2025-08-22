using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Text.Json;

namespace ApiAppLeon.Services
{
    public class PdfService
    {
        public string GeneratePdfBase64()
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            var doc = new Document(pdf);

            doc.Add(new Paragraph("Hello 👋 from your .NET 6 backend!"));
            doc.Add(new Paragraph($"Generated at: {DateTime.Now}"));

            doc.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        public string GeneratePdfBase64(PdfDocumentData data)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdf = new PdfDocument(writer);
            var doc = new Document(pdf);

            // 1. Header
            var headerTable = new Table(3).UseAllAvailableWidth();

            if (!string.IsNullOrEmpty(data.LogoBase64))
            {
                var base64 = data.LogoBase64;

                // ✅ Remove data URI prefix if present
                var base64Data = base64.Contains(",") ? base64.Substring(base64.IndexOf(",") + 1) : base64;

                byte[] logoBytes = Convert.FromBase64String(base64Data);
                var imageData = ImageDataFactory.Create(logoBytes); // you had .CreateImageInstance
                var logo = new Image(imageData).ScaleToFit(64, 64);

                headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));
            }
            else
            {
                headerTable.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
            }

            headerTable.AddCell(new Cell().Add(new Paragraph(data.BusinessTitle ?? "")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(14)).SetBorder(Border.NO_BORDER));

            string userInfo = $"ID: {data.UserId ?? ""}\n{data.UserName ?? ""}";
            headerTable.AddCell(new Cell().Add(new Paragraph(userInfo)
                .SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));

            doc.Add(headerTable);

            // Date and Time
            doc.Add(new Paragraph($"Fecha: {data.GeneratedAt:yyyy-MM-dd}")
                .SetTextAlignment(TextAlignment.LEFT).SetFontSize(10));
            doc.Add(new Paragraph($"Hora: {data.GeneratedAt:HH:mm:ss}")
                .SetTextAlignment(TextAlignment.LEFT).SetFontSize(10));

            // 2. Document Title
            doc.Add(new Paragraph(data.DocumentTitle ?? "")
                .SetFontSize(18).SetBold().SetMarginTop(10));

            // 3. Dynamic Content
            if (data.Content is string strContent)
            {
                doc.Add(new Paragraph(strContent));
            }
            else if (data.Content is IEnumerable<string> list)
            {
                var table = new Table(1).UseAllAvailableWidth();
                foreach (var item in list)
                    table.AddCell(new Cell().Add(new Paragraph(item)));
                doc.Add(table);
            }
            else if (data.Content is IEnumerable<object> objList)
            {
                foreach (var obj in objList)
                {
                    var subTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
                    var props = obj.GetType().GetProperties();

                    foreach (var prop in props)
                    {
                        subTable.AddCell(new Paragraph(prop.Name).SetBold());
                        string val = JsonSerializer.Serialize(prop.GetValue(obj));
                        subTable.AddCell(new Paragraph(val.Replace("\"", "")));
                    }

                    doc.Add(subTable);
                    doc.Add(new Paragraph("")); // spacing
                }
            }
            else
            {
                doc.Add(new Paragraph("Formato de contenido no compatible."));
            }

            // 4. Watermark: Vision / Mission (semi-transparent)
            if (!string.IsNullOrEmpty(data.Vision) || !string.IsNullOrEmpty(data.Mission))
            {
                var watermark = new Paragraph($"{data.Vision ?? ""}\n{data.Mission ?? ""}")
                    .SetFontSize(20)
                    .SetFontColor(ColorConstants.GRAY, 0.4f)
                    .SetTextAlignment(TextAlignment.CENTER);

                doc.ShowTextAligned(watermark, 297, 421, pdf.GetNumberOfPages(),
                    TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
            }

            doc.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
