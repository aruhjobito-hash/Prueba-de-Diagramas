using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Reportes_Anexos_SBS
{
    public class requestReporte1Model
    {
        [Required(ErrorMessage = "El Campo es Obligatorio")]
        public int iOpcion { get; set; }

        [MaxLength(4, ErrorMessage = "El campo Año no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo Año debe tener al menos 4 caracteres")]
        public string? cAnio { get; set; }

        [MaxLength(2, ErrorMessage = "El campo Mes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo Mes debe tener al menos 2 caracteres")]
        public string? cMes { get; set; }

        [MaxLength(6, ErrorMessage = "El campo Usuario no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo Usuario debe tener al menos 6 caracteres")]
        public string? cUsuario { get; set; }

        [MaxLength(10, ErrorMessage = "El campo Codigo no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Codigo debe tener al menos 10 caracteres")]
        public string? CodReporte1 { get; set; }
    }
    
    public class Reporte1DBModel
    {
        public int IdReporte1 { get; set; }
        public long NroFila { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? TipoPersona { get; set; }
        public string? ApeNomRazonSocial { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Genero { get; set; }
        public string? Domicilio { get; set; }
        public string? UbigeoDomic { get; set; }
        public decimal? AportePagado { get; set; }
        public decimal? AporteSuscrito { get; set; }
        public bool Activo { get; set; }
    }



}
