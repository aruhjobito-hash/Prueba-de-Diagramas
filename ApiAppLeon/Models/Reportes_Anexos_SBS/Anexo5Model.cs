
// Developer: VicVil 02/05/2025 - Controlador para generar el Anexo5
// DateCreate   : 02/05/2025

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Anexo5 
    /// </summary>
    public class requestAnexo5Model
    {
        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string? cAnio { get; set; }

        [MaxLength(2, ErrorMessage = "El campo Mes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Mes debe tener al menos 2 caracteres")]
        public string? cMes { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string? cUsuario { get; set; }

    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Anexo5 
    /// </summary>
    [Keyless]
    public class Anexo5DBModel
    {
        public long IdDetalleContable { get; set; }
        public long IdAnexo5A { get; set; }
        public decimal CDSaldo { get; set; } = 0;
        //public decimal CDExposicionEquivalente { get; set; } = 0;
        public decimal CDProvisionesGenericas { get; set; } = 0;
        public decimal CDProvisionesEspecificas { get; set; } = 0;
        public decimal CISaldoA { get; set; } = 0;
        public decimal CISaldoB { get; set; } = 0;
        public decimal CISaldoC { get; set; } = 0;
        public decimal CISaldoD { get; set; } = 0;
        public decimal CIProvisionesGenericasA { get; set; } = 0;
        public decimal CIProvisionesGenericasB { get; set; } = 0;
        public decimal CIProvisionesGenericasC { get; set; } = 0;
        public decimal CIProvisionesGenericasD { get; set; } = 0;
        public decimal CIProvisionesEspecificasA { get; set; } = 0;
        public decimal CIProvisionesEspecificasB { get; set; } = 0;
        public decimal CIProvisionesEspecificasC { get; set; } = 0;
        public decimal CIProvisionesEspecificasD { get; set; } = 0;
       // public int Tipo { get; set; }
        //public int SubTipo { get; set; }

    }

    


    
}