using Clases.Sistema;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore.Controllers.Sistema
{
    public class AgenciaController
    {
        private readonly APILEON apiLeon = new APILEON();
        public AgenciaController() { }
        public async Task<List<AgenciasDBModel>> LoadAgenciaDataAsync()
        {
            try
            {
                string url;
                url = $"/api/Sistemas/Agencias";
                var Documento = await apiLeon.APILEONGET<List<AgenciasDBModel>>(url);
                if (Documento != null)
                {
                    return Documento;
                }
                else
                {
                    return new List<AgenciasDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AgenciasDBModel>();
            }
        }
    }
}
