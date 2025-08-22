using Clases.Sistema;
using LeonXIIICore.Models.Configuracion;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore.Controllers.Configuracion
{
    public class PensionesController
    {
        private readonly APILEON apiLeon = new APILEON();
        public PensionesController() { }
        public async Task<List<PensionDBModel>> LoadPensionesDataAsync()
        {
            try
            {
                string url;
                url = $"/api/Configuracion/Pension";
                var Pensiones = await apiLeon.APILEONGET<List<PensionDBModel>>(url);
                if (Pensiones != null)
                {
                    return Pensiones;
                }
                else
                {
                    return new List<PensionDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PensionDBModel>();
            }
        }
        public async Task<List<PensionDBModel>> Create_Update_Delete_PensionDataAsync(requestPensionModel model, int opcion)
        {
            try
            {
                string url = "";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<PensionDBModel>> PensionResponse = null;

                switch (opcion)
                {
                    case 1:
                        url = $"/api/Configuracion/Pension";
                        PensionResponse = await apiLeon.APILEONPOST<Respuesta<List<PensionDBModel>>>(url, model);
                        break;
                    case 2:
                        url = $"/api/Configuracion/Pension/Pension";
                        PensionResponse = await apiLeon.APILEONPUT<Respuesta<List<PensionDBModel>>>(url, model);
                        break;
                    case 3:
                        url = $"/api/Configuracion/Pension?idpension={model.IdPension}";
                        PensionResponse = await apiLeon.APILEONDEL<Respuesta<List<PensionDBModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (PensionResponse != null && (PensionResponse.Exito == 1 || PensionResponse.Exito == 0))
                {
                    return PensionResponse.Data;
                }
                else
                {
                    return new List<PensionDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PensionDBModel>();
            }
        }
        public async Task<List<PensionDBModel>> OnOffPensionAsync(requestPensionModel model)
        {
            try
            {
                string url = $"/api/Configuracion/Pension/EstPension";
                Respuesta<List<PensionDBModel>> EnableResponse = null;
                EnableResponse = await apiLeon.APILEONPUT<Respuesta<List<PensionDBModel>>>(url, model);
                if (EnableResponse != null && (EnableResponse.Exito == 1 || EnableResponse.Exito == 0))
                {
                    return EnableResponse.Data;
                }
                else
                {
                    return new List<PensionDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PensionDBModel>();
            }
        }
    }
}
