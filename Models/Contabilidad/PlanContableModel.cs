namespace LeonXIIICore.Models.Contabilidad
{
    public class requestPlanCuentasModel
    {

        public string CtaContable { get; set; }
        public string Año { get; set; }
        public string Descripcion { get; set; }
        public string CtaAsiento { get; set; }
        public string? NvaCtaContable { get; set; }
        public string IdUser { get; set; }
        public string? idarea { get; set; }


    }
    public class PlanCuentasDBModel
    {
        public string bEstado { get; set; }
        public string CtaContable { get; set; }
        public string Año { get; set; }
        public string Descripcion { get; set; }
        public string CtaAsiento { get; set; }
        public string? NvaCtaContable { get; set; }
        public string? area { get; set; }
        public string? idarea { get; set; }
    }
    public class AreaModel
    {
        public string idarea { get; set; }
        public string area { get; set; }
    }
}
