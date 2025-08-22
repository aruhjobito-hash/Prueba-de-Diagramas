namespace ApiAppLeon.Services
{
    public class PdfDocumentData
    {
        public string LogoBase64 { get; set; } // optional base64 image
        public string BusinessTitle { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string DocumentTitle { get; set; }
        public object Content { get; set; } // Can be string, string[], or list of objects

        public string Vision { get; set; }
        public string Mission { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.Now;
    }

}
