using Clases.Sistema;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore
{
    public class DatosTrabajadorService
    {
        public async Task<List<DatosTrabajadorDBModel>> ObtenerDatosTrabajadorAsync(string IdUser)
        {
            var apiLeon = new APILEON();

            var menuRequest = new requestDatosTrabajadorModel
            {
                IdUser = IdUser,
            };
            try
            {
                var menuResponse = await apiLeon.APILEONPOST<List<DatosTrabajadorDBModel>>("/api/Sistemas/DatosTrabajador", menuRequest);
                return menuResponse ?? new List<DatosTrabajadorDBModel>();
            }
            catch
            {
                // Manejo de excepciones, puedes registrar el error o lanzar una excepción personalizada
                return new List<DatosTrabajadorDBModel>();
            }
           
        }
    }

}