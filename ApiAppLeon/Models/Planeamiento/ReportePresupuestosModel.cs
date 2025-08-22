
// Developer: migzav 10/05/2025 - Controlador Mantenedor Reportes de Presupuestoss
// DateCreate   : 10/05/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Planeamiento
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: ReportePresupuestos 
    /// </summary>
    public class requestReportePresupuestosModel
    {
        public string IdAgencia { get; set; }
        public string IdArea { get; set; }
        public string Año { get; set; }
        public int Digitos { get; set; }
        public string MesDesde { get; set; }
        public string MesHasta { get; set; }
        public string TipoMoneda { get; set; }
        public string Tipo { get; set; }
    }


    public class requestTipoReporteModel
    {
        public string IdAgencia { get; set; }
        public string IdArea { get; set; }
        public string Año { get; set; }
        public int Digitos { get; set; }
        public string TipoReporte { get; set; }
        public string? MesDesde { get; set; }
        public string? MesHasta { get; set; }
        public string Tipo { get; set; }
        public string? TipoMoneda { get; set; }
    }


    public class ComparativoReporteGlobalRequest
    {
        public string IdAgencia { get; set; }
        public string IdArea { get; set; }
        public string Año { get; set; }
        public int Digitos { get; set; }
        public string TipoComparativo { get; set; } = string.Empty;
        public string? MesDesde { get; set; }
        public string? MesHasta { get; set; }
        public int? TrimestreDesde { get; set; }
        public int? TrimestreHasta { get; set; }
        public int? SemestreDesde { get; set; }
        public int? SemestreHasta { get; set; }
        public string Tipo { get; set; }
    }

    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_ReportePresupuestos 
    /// </summary>
    [Keyless]
    public class ReporteComparativoMensualPresupuestosDBModel
    {

        public string CtaContable { get; set; }
        public string CtaNombre { get; set; }
        public decimal? P01 { get; set; }
        public decimal? E01 { get; set; }
        public decimal? D01 { get; set; }
        public decimal? V01 { get; set; }
        public decimal? P02 { get; set; }
        public decimal? E02 { get; set; }
        public decimal? D02 { get; set; }
        public decimal? V02 { get; set; }
        public decimal? P03 { get; set; }
        public decimal? E03 { get; set; }
        public decimal? D03 { get; set; }
        public decimal? V03 { get; set; }
        public decimal? P04 { get; set; }
        public decimal? E04 { get; set; }
        public decimal? D04 { get; set; }
        public decimal? V04 { get; set; }
        public decimal? P05 { get; set; }
        public decimal? E05 { get; set; }
        public decimal? D05 { get; set; }
        public decimal? V05 { get; set; }
        public decimal? P06 { get; set; }
        public decimal? E06 { get; set; }
        public decimal? D06 { get; set; }
        public decimal? V06 { get; set; }
        public decimal? P07 { get; set; }
        public decimal? E07 { get; set; }
        public decimal? D07 { get; set; }
        public decimal? V07 { get; set; }
        public decimal? P08 { get; set; }
        public decimal? E08 { get; set; }
        public decimal? D08 { get; set; }
        public decimal? V08 { get; set; }
        public decimal? P09 { get; set; }
        public decimal? E09 { get; set; }
        public decimal? D09 { get; set; }
        public decimal? V09 { get; set; }
        public decimal? P10 { get; set; }
        public decimal? E10 { get; set; }
        public decimal? D10 { get; set; }
        public decimal? V10 { get; set; }
        public decimal? P11 { get; set; }
        public decimal? E11 { get; set; }
        public decimal? D11 { get; set; }
        public decimal? V11 { get; set; }
        public decimal? P12 { get; set; }
        public decimal? E12 { get; set; }
        public decimal? D12 { get; set; }
        public decimal? V12 { get; set; }
        public decimal? PT { get; set; }
        public decimal? ET { get; set; }
        public decimal? DT { get; set; }
        public decimal? VT { get; set; }

    }


    [Keyless]
    public class ReporteComparativoTrimestralPresupuestosDBModel
    {

        public string CtaContable { get; set; }
        public string CtaNombre { get; set; }
        public decimal? P01 { get; set; }
        public decimal? T1 { get; set; }
        public decimal? D01 { get; set; }
        public decimal? V01 { get; set; }
        public decimal? P02 { get; set; }
        public decimal? T2 { get; set; }
        public decimal? D02 { get; set; }
        public decimal? V02 { get; set; }
        public decimal? P03 { get; set; }
        public decimal? T3 { get; set; }
        public decimal? D03 { get; set; }
        public decimal? V03 { get; set; }
        public decimal? P04 { get; set; }
        public decimal? T4 { get; set; }
        public decimal? D04 { get; set; }
        public decimal? V04 { get; set; }
        public decimal? PT { get; set; }
        public decimal? ET { get; set; }
        public decimal? DT { get; set; }
        public decimal? VT { get; set; }

    }

    [Keyless]
    public class ReporteComparativoSemestralPresupuestosDBModel
    {

        public string CtaContable { get; set; }
        public string CtaNombre { get; set; }
        public decimal? P01 { get; set; }
        public decimal? S1 { get; set; }
        public decimal? D01 { get; set; }
        public decimal? V01 { get; set; }
        public decimal? P02 { get; set; }
        public decimal? S2 { get; set; }
        public decimal? D02 { get; set; }
        public decimal? V02 { get; set; }
        public decimal? PT { get; set; }
        public decimal? ET { get; set; }
        public decimal? DT { get; set; }
        public decimal? VT { get; set; }

    }


    [Keyless]
    public class ReporteComparativoAcumulativoPresupuestosDBModel
    {

        public string CtaContable { get; set; }
        public string CtaNombre { get; set; }
        public decimal P01 { get; set; }
        public decimal E01 { get; set; }
        public decimal D01 { get; set; }
        public decimal V01 { get; set; }


    }


    [Keyless]
    public class ReportePresupuestadoDBModel
    {

        public string CtaContable { get; set; }
        public string CtaNombre { get; set; }
        public decimal Enero { get; set; }
        public decimal Febrero { get; set; }
        public decimal Marzo { get; set; }
        public decimal Abril { get; set; }
        public decimal Mayo { get; set; }
        public decimal Junio { get; set; }
        public decimal Julio { get; set; }
        public decimal Agosto { get; set; }
        public decimal Septiembre { get; set; }
        public decimal Octubre { get; set; }
        public decimal Noviembre { get; set; }
        public decimal Diciembre { get; set; }
        public decimal TotalPresupuesto { get; set; }

    }

    [Keyless]
    public class ReporteEjecutadoDBModel
    {

        public string CtaContable { get; set; }
        public string Descripcion { get; set; }
        public decimal E01 { get; set; }
        public decimal E02 { get; set; }
        public decimal E03 { get; set; }
        public decimal E04 { get; set; }
        public decimal E05 { get; set; }
        public decimal E06 { get; set; }
        public decimal E07 { get; set; }
        public decimal E08 { get; set; }
        public decimal E09 { get; set; }
        public decimal E10 { get; set; }
        public decimal E11 { get; set; }
        public decimal E12 { get; set; }

    }

    public class requestPresupuestadoInversionesModel
    {
        public int Año { get; set; }
        public string IdAgencia { get; set; }
        public string IdArea { get; set; }
        public string TipMoneda { get; set; }
        public string MesDesde { get; set; } 
        public string MesHasta { get; set; } 
    }

    public class ComparativoInversionesRequest
    {
        public string IdAgencia { get; set; }
        public string IdArea { get; set; }
        public string Año { get; set; }
        public string TipMoneda { get; set; }
        public string TipoComparativo { get; set; } = string.Empty;
        public string? MesDesde { get; set; }
        public string? MesHasta { get; set; }
        public int? TrimestreDesde { get; set; }
        public int? TrimestreHasta { get; set; }
        public int? SemestreDesde { get; set; }
        public int? SemestreHasta { get; set; }
        public string Tipo { get; set; }
    }


    [Keyless]
    public class ReporteComparativoMensualInversionesDBModel
    {
        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int? CantP { get; set; }
        public int? CantE { get; set; }
        public string? NRp { get; set; }
        public string? NRe { get; set; }
        public decimal? EneroP { get; set; }
        public decimal? EneroE { get; set; }
        public decimal? EneroD { get; set; }
        public decimal? EneroV { get; set; }
        public decimal? FebreroP { get; set; }
        public decimal? FebreroE { get; set; }
        public decimal? FebreroD { get; set; }
        public decimal? FebreroV { get; set; }
        public decimal? MarzoP { get; set; }
        public decimal? MarzoE { get; set; }
        public decimal? MarzoD { get; set; }
        public decimal? MarzoV { get; set; }
        public decimal? AbrilP { get; set; }
        public decimal? AbrilE { get; set; }
        public decimal? AbrilD { get; set; }
        public decimal? AbrilV { get; set; }
        public decimal? MayoP { get; set; }
        public decimal? MayoE { get; set; }
        public decimal? MayoD { get; set; }
        public decimal? MayoV { get; set; }
        public decimal? JunioP { get; set; }
        public decimal? JunioE { get; set; }
        public decimal? JunioD { get; set; }
        public decimal? JunioV { get; set; }
        public decimal? JulioP { get; set; }
        public decimal? JulioE { get; set; }
        public decimal? JulioD { get; set; }
        public decimal? JulioV { get; set; }
        public decimal? AgostoP { get; set; }
        public decimal? AgostoE { get; set; }
        public decimal? AgostoD { get; set; }
        public decimal? AgostoV { get; set; }
        public decimal? SeptiembreP { get; set; }
        public decimal? SeptiembreE { get; set; }
        public decimal? SeptiembreD { get; set; }
        public decimal? SeptiembreV { get; set; }
        public decimal? OctubreP { get; set; }
        public decimal? OctubreE { get; set; }
        public decimal? OctubreD { get; set; }
        public decimal? OctubreV { get; set; }
        public decimal? NoviembreP { get; set; }
        public decimal? NoviembreE { get; set; }
        public decimal? NoviembreD { get; set; }
        public decimal? NoviembreV { get; set; }
        public decimal? DiciembreP { get; set; }
        public decimal? DiciembreE { get; set; }
        public decimal? DiciembreD { get; set; }
        public decimal? DiciembreV { get; set; }
    }

    

    [Keyless]
    public class ReporteComparativoTrimestralInversionesDBModel
    {
        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int? CantP { get; set; }
        public int? CantE { get; set; }
        public string? NRp { get; set; }
        public string? NRe { get; set; }
        public decimal? Trimestre1P { get; set; }
        public decimal? Trimestre1E { get; set; }
        public decimal? Trimestre1D { get; set; }
        public decimal? Trimestre1V { get; set; }
        public decimal? Trimestre2P { get; set; }
        public decimal? Trimestre2E { get; set; }
        public decimal? Trimestre2D { get; set; }
        public decimal? Trimestre2V { get; set; }
        public decimal? Trimestre3P { get; set; }
        public decimal? Trimestre3E { get; set; }
        public decimal? Trimestre3D { get; set; }
        public decimal? Trimestre3V { get; set; }
        public decimal? Trimestre4P { get; set; }
        public decimal? Trimestre4E { get; set; }
        public decimal? Trimestre4D { get; set; }
        public decimal? Trimestre4V { get; set; }

    }

    [Keyless]
    public class ReporteComparativoSemestralInversionesDBModel
    {
        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int? CantP { get; set; }
        public int? CantE { get; set; }
        public string? NRp { get; set; }
        public string? NRe { get; set; }
        public decimal? Sem1P { get; set; }
        public decimal? Sem1E { get; set; }
        public decimal? Sem1D { get; set; }
        public decimal? Sem1V { get; set; }
        public decimal? Sem2P { get; set; }
        public decimal? Sem2E { get; set; }
        public decimal? Sem2D { get; set; }
        public decimal? Sem2V { get; set; }

    }

    [Keyless]
    public class ReporteAcumulativoInversionesDBModel
    {
        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int? CantP { get; set; }
        public int? CantE { get; set; }
        public decimal? TotalPresupuestado { get; set; }
        public decimal? TotalEjecutado { get; set; }
        public decimal? Diferencia { get; set; }
        public decimal? Variacion { get; set; }
     
    }


    [Keyless]

    public class ReporteEjecutadoInversionesDBModel
    {
        public string IdArticulo { get; set; }
        public decimal? Cant { get; set; }
        public string NR { get; set; }
        public string NombreArticulo { get; set; }
        public decimal EneroE { get; set; }
        public decimal FebreroE { get; set; }
        public decimal MarzoE { get; set; }
        public decimal AbrilE { get; set; }
        public decimal MayoE { get; set; }
        public decimal JunioE { get; set; }
        public decimal JulioE { get; set; }
        public decimal AgostoE { get; set; }
        public decimal SeptiembreE { get; set; }
        public decimal OctubreE { get; set; }
        public decimal NoviembreE { get; set; }
        public decimal DiciembreE { get; set; }
        public decimal TotalEjecutado { get; set; }
    }


}