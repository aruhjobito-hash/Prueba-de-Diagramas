using System.ComponentModel.DataAnnotations;

namespace ApiAppLeon.Models.Operaciones
{
    public class TransferenciaAhorrosCreditoModel
    {
    }
    public class TransferenciaAhorroCreditoBD
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
        public decimal? montos { get; set; }
        public string? glosa { get; set; }
    }   
    public class TransferenciaAhorrosCreditoRequest
    {
        [Required(ErrorMessage = "EL CAMPO IDDOC NO ES OPCIONAL")]
        [MaxLength(4, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA IDDOC ES ES DE 4")]
        [MinLength(4, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA IDDOC ES DE 4")]
        public string? iddoc { get; set; }

        [Required(ErrorMessage = "EL CAMPO NRODOC NO ES OPCIONAL")]
        [MaxLength(7, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA NRODOC ES ES DE 7")]
        [MinLength(7, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA NRODOC ES DE 7")]
        public string? nrodoc { get; set; }

        [Required(ErrorMessage = "EL CAMPO IDAGENCIA NO ES OPCIONAL")]
        [MaxLength(2, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA IDAGENCIA ES ES DE 2")]
        [MinLength(2, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA IDAGENCIA ES DE 2")]
        public string? idagencia { get; set; }

        [Required(ErrorMessage = "EL CAMPO TIPMONEDA NO ES OPCIONAL")]
        [MaxLength(1, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA TIPMONEDA ES ES DE 1")]
        [MinLength(1, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA TIPMONEDA ES DE 1")]
        public string? tipmoneda { get; set; }

        [Required(ErrorMessage = "EL CAMPO MONTOTOTAL NO ES OPCIONAL")]
        public decimal? montoTotal { get; set; }

        [Required(ErrorMessage = "EL CAMPO AMORT NO ES OPCIONAL")]
        public decimal? amort { get; set; }

        [Required(ErrorMessage = "EL CAMPO INTCOMP NO ES OPCIONAL")]
        public decimal? intComp { get; set; }

        [Required(ErrorMessage = "EL CAMPO INTVENC NO ES OPCIONAL")]
        public decimal? intVenc { get; set; }

        [Required(ErrorMessage = "EL CAMPO INTMOR NO ES OPCIONAL")]
        public decimal? intMor { get; set; }

        [MaxLength(3, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA NROCUOTA ES ES DE 3")]
        [MinLength(3, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA NROCUOTA ES DE 3")]
        [Required(ErrorMessage = "EL CAMPO NROCUOTA NO ES OPCIONAL")]
        public string? Nrocuota { get; set; }

        [Required(ErrorMessage = "EL CAMPO DESGRA NO ES OPCIONAL")]
        public decimal? desgra { get; set; }

        [Required(ErrorMessage = "EL CAMPO FECHAVEN NO ES OPCIONAL")]
        public string? fechaven { get; set; }

        [MaxLength(2, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA IDTIPCTAAHO ES ES DE 2")]
        [MinLength(2, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA IDTIPCTAAHO ES DE 2")]
        [Required(ErrorMessage = "EL CAMPO IDTIPCTAAHO NO ES OPCIONAL")]
        public string? idTipCtaAho { get; set; }

        [MaxLength(7, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA NUMCUENTAAHO ES ES DE 7")]
        [MinLength(7, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA NUMCUENTAAHO ES DE 7")]
        [Required(ErrorMessage = "EL CAMPO NUMCUENTAAHO NO ES OPCIONAL")]
        public string? numCuentaAho { get; set; }

        [MaxLength(2, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA IDAGENCIACTAAHO ES ES DE 2")]
        [MinLength(2, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA IDAGENCIACTAAHO ES DE 2")]
        [Required(ErrorMessage = "EL CAMPO IDAGENCIACTAAHO NO ES OPCIONAL")]
        public string? idagenciaCtaAho { get; set; }

        [Required(ErrorMessage = "EL CAMPO TIPMONEDACTAAHO NO ES OPCIONAL")]
        [MaxLength(1, ErrorMessage = "EL MAXIMO DE CARACTERES ES PARA TIPMONEDACTAAHO ES ES DE 1")]
        [MinLength(1, ErrorMessage = "EL MINIMO DE CARACTERES ES PARA TIPMONEDACTAAHO ES DE 1")]
        public string? tipMonedaCtaAho { get; set; }
    }
}
