
// Developer: VicVil 26/04/2025 - Controlador para generar el anexo13
// DateCreate   : 26/04/2025

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Anexo13Controller 
    /// </summary>
    public class requestAnexo13Model
    {
        public string? CodAnexo { get; set; }

        public string? cAnio { get; set; }

        public string? cMes { get; set; }

        public string? tFecCreacion { get; set; }


        public string? cUsuario { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (ANEXO 13) que recibirá los datos del store procedure: sp_Anexo13Controller 
    /// </summary>
    [Keyless]
    public class Anexo13DBModel
    {
        public int iNroFila { get; set; }

        public string CodAnexoDetalle { get; set; }

        public string CodAnexo { get; set; }

        public string CodAnexoEscala { get; set; }

        public int? iNumerosCuentaMN { get; set; }

        public int? iNumerosCuentaME { get; set; }

        public int? iNumerosCuentaTotal { get; set; }

        public decimal? nSaldoMN { get; set; }

        public decimal? nSaldoEquivalente { get; set; }

        public decimal? nSaldoTotal { get; set; }

        public int bActivo { get; set; }
    }

   

    [Keyless]
    public class Anexo13SucaveDBModel
    {
        public int? Id { get; set; }

        public string? Valor { get; set; }

    }


    


}