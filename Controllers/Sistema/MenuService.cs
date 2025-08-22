using Clases.Sistema;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore
{
    public class MenuService
    {
        public async Task<List<MenuClass>> ObtenerMenuAsync(string Perfil,string IdUser)
        {
            var apiLeon = new APILEON();

            var menuRequest = new requestMenuModel
            {
                Id = 1,
                Perfil = Perfil,
                TipoWin = "0",
                TipoWeb = "1",
                IdUser = IdUser
            };
            try
            {
                var menuResponse = await apiLeon.APILEONPOST<List<MenuClass>>("/api/Sistemas/Menu", menuRequest);
                return menuResponse ?? new List<MenuClass>();
            }
            catch
            {
                return new List<MenuClass>();
            }


           
        }
    }

}