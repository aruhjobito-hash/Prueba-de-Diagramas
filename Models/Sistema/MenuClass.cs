using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonXIIICore.Models.Sistema
{
    public class requestMenuModel
    {
        public Int32? Id { get; set; }

        //[Required(ErrorMessage = "El campo perfil es obligatorio")]
        //[MaxLength(50, ErrorMessage = "El campo perfil no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo perfil debe tener al menos 50 caracteres")]
        public string Perfil { get; set; }

        public string TipoWin { get; set; }

        public string TipoWeb { get; set; }
        public string IdUser { get; set; }
    }
    public class MenuClass
    {
        public string Menu { get; set; }
        public List<SubMenuClass> SubMenu { get; set; }
    }

    public class SubMenuClass
    {
        public string NombreSubMenu { get; set; }
        public string Tipo { get; set; }
        public List<DesgloseClass> Desglose { get; set; }
        public string Funcion { get; set; }
    }

    public class DesgloseClass
    {
        public string Name { get; set; }
        public string Funcion { get; set; }
    }
    public class  perfilesclass
    {
        public string perfil { get; set; }
        public string perfilJson  { get;set;}
    }
}