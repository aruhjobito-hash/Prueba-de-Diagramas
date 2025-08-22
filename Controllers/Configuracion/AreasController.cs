using Clases.Sistema;
using LeonXIIICore.Models.Configuracion;
using LeonXIIICore.Models.Sistema;
//using AreasModel = LeonXIIICore.Models.Configuracion.AreasModel;
//using requestAreasModel = LeonXIIICore.Models.Configuracion.requestAreasModel;

namespace LeonXIIICore.Controllers.Configuracion
{
    public class AreasController
    {
        private readonly APILEON apiLeon = new APILEON();
        public AreasController() { }
        public async Task<List<AreasModel>> LoadAreasDataAsync()
        {
            try
            {
                string url;
                url = $"/api/Configuracion/Areas";
                var Areas = await apiLeon.APILEONGET<List<AreasModel>>(url);
                if (Areas != null)
                {
                    return Areas;
                }
                else
                {
                    return new List<AreasModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AreasModel>();
            }
        }
        public async Task<List<AreasModel>> Create_Update_Delete_AreaDataAsync(requestAreasModel model, int opcion)
        {
            try
            {
                string url = "";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<AreasModel>> AreaResponse = null;

                switch (opcion)
                {
                    case 1:
                        url = $"/api/Configuracion/Areas";
                        AreaResponse = await apiLeon.APILEONPOST<Respuesta<List<AreasModel>>>(url, model);
                        break;
                    case 2:
                        url = $"/api/Configuracion/Areas/Areas";
                        AreaResponse = await apiLeon.APILEONPUT<Respuesta<List<AreasModel>>>(url, model);
                        break;
                    case 3:
                        url = $"/api/Configuracion/Areas?idarea={model.IdArea}";
                        AreaResponse = await apiLeon.APILEONDEL<Respuesta<List<AreasModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (AreaResponse != null && (AreaResponse.Exito == 1 || AreaResponse.Exito == 0))
                {
                    return AreaResponse.Data;
                }
                else
                {
                    return new List<AreasModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AreasModel>();
            }
        }
        public async Task<List<AreasModel>> OnOffAreaAsync(requestAreasModel model)
        {
            try
            {
                string url = $"/api/Configuracion/Areas/EstAreas";
                Respuesta<List<AreasModel>> EnableResponse = null;
                EnableResponse = await apiLeon.APILEONPUT<Respuesta<List<AreasModel>>>(url, model);
                if (EnableResponse != null && (EnableResponse.Exito == 1 || EnableResponse.Exito == 0))
                {
                    return EnableResponse.Data;
                }
                else
                {
                    return new List<AreasModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AreasModel>();
            }
        }
    }
}
