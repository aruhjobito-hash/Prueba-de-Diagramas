
// Developer: josara 11/03/2025 - Controlador para mantenedor de menus(web y windows)
// DateCreate   : 11/03/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: Menu 
    /// </summary>
    public class requestMenuModel
    {
        public Int32? Id { get; set; }

        //[Required(ErrorMessage = "El campo perfil es obligatorio")]
        //[MaxLength(50, ErrorMessage = "El campo perfil no puede tener más de 50 caracteres")]
        //[MinLength(50, ErrorMessage = "El campo perfil debe tener al menos 50 caracteres")]
        public string Perfil { get; set; }
        [Required(ErrorMessage = "El campo TipoWin es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipoWin no puede tener más de 6 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipoWin debe tener al menos 6 caracteres")]
        public string TipoWin { get; set; }
        [Required(ErrorMessage = "El campo TipoWin es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipoWin no puede tener más de 6 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipoWin debe tener al menos 6 caracteres")]
        public string TipoWeb { get; set; }

        [Required(ErrorMessage = "El campo iduser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo iduser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo iduser debe tener al menos 6 caracteres")]
        public string IdUser { get; set; }
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Menu 
    /// </summary>
    [Keyless]
    public class MenuDBModel
    {
        public Int64 Nro { get; set; }
        public string MenuIcono { get; set; }
        public string Menu { get; set; }
        public string SubMenuIcono { get; set; }
        public string SubMenu { get; set; }
        public string Tipo { get; set; }
        public string NombreSubMenu { get; set; }
        public string FuncionMenu { get; set; }
        public string NameIcono { get; set; }
        public string Name { get; set; }
        public string FuncionSubMenu { get; set; }

    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_Menu 
    /// <class name="Menu">Menu is string,</class>
    /// <class name="SubMenu">SubMenu is List(SubMenuClass)</class>
    /// </summary>
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
    public class MenuRoot
    {
        public string icon { get; set; }
        public string label { get; set; }
        public string route { get; set; }
        public List<SubItem> subItems { get; set; }
    }

    public class SubItem
    {
        public string icon { get; set; }
        public string label { get; set; }
        public string route { get; set; }
        public List<SubItem> subItems { get; set; }
    }

    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}
