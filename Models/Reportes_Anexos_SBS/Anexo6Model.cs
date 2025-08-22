using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Reportes_Anexos_SBS
{

    public class Anexo6Model
    {
        public string CodAnexo6 { get; set; } = "";
        public string Mes { get; set; } = "";
        public string Anio { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Fecha { get; set; } = "";
        public string Hora { get; set; } = "";
        public string Estado { get; set; } = "";
    }

    public class Anexo6DBModel
    {
        public string CodDetalle { get; set; }
        public string CodAnexo6 { get; set; }
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
}
