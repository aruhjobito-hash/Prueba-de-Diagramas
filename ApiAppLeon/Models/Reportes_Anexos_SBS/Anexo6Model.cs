
// Developer: VicVil 06/05/2025 - Controlador para generar el anexo6
// DateCreate   : 06/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Reportes_Anexos_SBS
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Anexo6 
    /// </summary>
    public class requestAnexo6Model
    {
        public int? Id { get; set; }


        [Required(ErrorMessage = "El campo CodAnexo6 es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo CodAnexo6 no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo CodAnexo6 debe tener al menos 10 caracteres")]
        public string CodAnexo { get; set; }


        [Required(ErrorMessage = "El campo cAnio es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo cAnio no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo cAnio debe tener al menos 4 caracteres")]
        public string cAnio { get; set; }


        [Required(ErrorMessage = "El campo cMes es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cMes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cMes debe tener al menos 2 caracteres")]
        public string cMes { get; set; }


        public string tFecCreacion { get; set; }


        [Required(ErrorMessage = "El campo cUsuario es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo cUsuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo cUsuario debe tener al menos 6 caracteres")]
        public string cUsuario { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Anexo6 
    /// </summary>
    [Keyless]
    public class Anexo6DBModel
    {
        public string CodDetalle { get; set; }
        public string CodAnexo { get; set; }
        public string CRegistro { get; set; }
        public string CCliente { get; set; }
        public string CFechaNaci { get; set; }
        public string CGenero { get; set; }
        public string CEstadoCivil { get; set; }
        public string CSiglaEmpresa { get; set; }
        public string CCodigoSocio { get; set; }
        public string CPartidaRegistral { get; set; }
        public string CTipoDocumento { get; set; }
        public string CNumeroDocumento { get; set; }
        public string CTipoPersona { get; set; }
        public string CDomicilio { get; set; }
        public string CRelacionLaboral { get; set; }
        public string? CClasificacionDeudor { get; set; }
        public string? CClasificacionDeudorAI { get; set; }
        public string CCodigoAgencia { get; set; }
        public string CMonedaCredito { get; set; }
        public string CNumeroCredito { get; set; }
        public string CTipoCredito { get; set; }
        public string CSubTipoCredito { get; set; }
        public string? CFechaDesembolso { get; set; }
        public string? CMontoDesembolso { get; set; }
        public string? CTasaInteresAnual { get; set; }
        public string? CSaldoColocaciones { get; set; }
        public string CCuentaContable { get; set; }
        public string? CCapitalVigente { get; set; }
        public string? CCapitalReestructurado { get; set; }
        public string? CCapitalRefinanciado { get; set; }
        public string? CCapitalVencido { get; set; }
        public string? CCapitalCobranzaJudicial { get; set; }
        public string? CCapitalContigente { get; set; }
        public string CCuentaContableCapitalContigente { get; set; }
        public string? CDiasMora { get; set; }
        public string? CSaldoGarantiasPreferidas { get; set; }
        public string? CSaldoGarantiasAutoliquidables { get; set; }
        public string? CProvisionesRequeridas { get; set; }
        public string? CProvisionesConstituidas { get; set; }
        public string? CSaldoCreditoCastigados { get; set; }
        public string CCuentaContableCreditoCastigado { get; set; }
        public string? CRendimientoDevengado { get; set; }
        public string? CInteresesSuspenso { get; set; }
        public string? CIngresosDiferidos { get; set; }
        public string CTipoProductos { get; set; }
        public string? CNumeroCuotasProgramadas { get; set; }
        public string? CNumeroCuotasPagadas { get; set; }
        public string? CPeriodicidadCuota { get; set; }
        public string? CPeriodoGracia { get; set; }
        public string? CFechaVencimientoOriginal { get; set; }
        public string? CFechaVencimientoActual { get; set; }
        public bool BActivo { get; set; }
        public string? CSaldoSustitucionContraparte { get; set; }
        public string? CSaldoNoCobertura { get; set; }
        public string? CSaldoReprogramados { get; set; }
        public string? CSaldoCapitalCuentaOrden { get; set; }
        public string CSubcuentaOrden { get; set; }
        public string? CRendimientoDevengadoCovid19 { get; set; }
        public string? CSaldoGarantiaSustitucionContraparte { get; set; }
        public string? CSaldoReprogramadosCovid { get; set; }
        public string CSubcuentaOrdenReproCovid { get; set; }
        public string? CSaldoCapitalImpulso { get; set; }
        public string? CRendimientoDevengadoImpulso { get; set; }
    }


    [Keyless]
    public class Anexo6SucaveDBModel
    {
        public string? Valor { get; set; }
    }

    







  




}