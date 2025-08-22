using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Reportes_Anexos_SBS
{
    
    public class AnexoAuditoria
    {
        public string Codigo { get; set; } = "";
        public string Mes { get; set; } = "";
        public string Anio { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Fecha { get; set; } = "";
        public string Hora { get; set; } = "";
        public string Estado { get; set; } = "";
    }

    public class AuditoriaModel
    {
        public string CODIGO { get; set; } = "";
        public string MES { get; set; } = "";
        public string ANIO { get; set; } = "";
        public string USUARIO { get; set; } = "";
        public string FECHA { get; set; } = "";
        public string HORA { get; set; } = "";
        public string ESTADO { get; set; } = "";
    }

    public class CodAnexoDBModel
    {
        public string Codigo { get; set; }
    }




}
