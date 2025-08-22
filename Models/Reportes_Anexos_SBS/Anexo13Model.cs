using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Reportes_Anexos_SBS
{
    //ENTIDADES

    public class requestAnexo13Model
    {
        public string? CodAnexo13 { get; set; }
        public string? cAnio { get; set; }
        public string? cMes { get; set; }
        public string? tFecCreacion { get; set; }
        public string? cUsuario { get; set; }
    }



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

    public class RespuestaAnexo13List<T>
    {
        public int exito { get; set; }
        public string mensaje { get; set; }
        public List<T> data { get; set; }
    }


}
