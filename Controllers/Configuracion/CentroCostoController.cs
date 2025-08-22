using Clases.Sistema;
using LeonXIIICore.Models.Configuracion;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore.Controllers.Configuracion
{
    public class CentroCostoController
    {
        private readonly APILEON apiLeon = new APILEON();
        public CentroCostoController() { }
        public async Task<List<CentroCostosModel>> LoadCentroCostoDataAsync(string idagencia)
        {
            try
            {
                string url;
                url = $"/api/Finanzas/CentroCostos?idagencia={idagencia}";
                var CentroCosto = await apiLeon.APILEONGET<List<CentroCostosModel>>(url);
                if (CentroCosto != null)
                {
                    return CentroCosto;
                }
                else
                {
                    return new List<CentroCostosModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CentroCostosModel>();
            }
        }
        public async Task<List<CentroCostosModel>> Create_Update_Delete_CentroCostoDataAsync(RequestCentroCostosModel model, int opcion)
        {
            try
            {
                string url = "";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<CentroCostosModel>> CentroCostoResponse = null;

                switch (opcion)
                {
                    case 1:
                        url = $"/api/Finanzas/CentroCostos";
                        CentroCostoResponse = await apiLeon.APILEONPOST<Respuesta<List<CentroCostosModel>>>(url, model);
                        break;
                    case 2:
                        url = $"/api/Finanzas/CentroCostos/CentroCosto";
                        CentroCostoResponse = await apiLeon.APILEONPUT<Respuesta<List<CentroCostosModel>>>(url, model);
                        break;
                    case 3:
                        url = $"/api/Finanzas/CentroCostos?idarea={model.IdArea}&idagencia={model.IdAgencia}";
                        CentroCostoResponse = await apiLeon.APILEONDEL<Respuesta<List<CentroCostosModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (CentroCostoResponse != null && (CentroCostoResponse.Exito == 1 || CentroCostoResponse.Exito == 0))
                {
                    return CentroCostoResponse.Data;
                }
                else
                {
                    return new List<CentroCostosModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CentroCostosModel>();
            }
        }
        public async Task<List<CentroCostosModel>> OnOffCentroCostoAsync(RequestCentroCostosModel model)
        {
            try
            {
                string url = $"/api/Finanzas/CentroCostos/ActCentroCosto";
                Respuesta<List<CentroCostosModel>> EnableResponse = null;
                EnableResponse = await apiLeon.APILEONPUT<Respuesta<List<CentroCostosModel>>>(url, model);
                if (EnableResponse != null && (EnableResponse.Exito == 1 || EnableResponse.Exito == 0))
                {
                    return EnableResponse.Data;
                }
                else
                {
                    return new List<CentroCostosModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CentroCostosModel>();
            }
        }
    }
}
