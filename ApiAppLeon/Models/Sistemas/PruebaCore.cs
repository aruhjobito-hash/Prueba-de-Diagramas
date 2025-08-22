using System.Data.SqlTypes;

namespace ApiAppLeon.Models.Sistemas
{
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
    public class PruebaCore
    {
        public int? Id { get; set; }
        public string? Tittle { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? iddoc { get; set; }
        public string? nrodoc { get; set; }
        public string? idagencia { get; set; }
        public string? tipmoneda { get; set; }
        public string? nrocuota { get; set; }
        public decimal? total { get; set; }
        public decimal? am { get; set; }
        public decimal? au { get; set; }
        public decimal? ic { get; set; }
        public decimal? im { get; set; }
        public decimal? iv { get; set; }
        public DateTime FecVen { get; set; }
    }
}
