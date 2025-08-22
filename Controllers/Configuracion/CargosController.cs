using Clases.Sistema;
using LeonXIIICore.Models.Configuracion;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore.Controllers.Configuracion
{
    public class CargosController
    {
        private readonly APILEON apiLeon = new APILEON();
        public CargosController() { }
        public async Task<List<CargosDBModel>> LoadCargosDataAsync()
        {
            try
            {
                string url;
                url = $"/api/Configuracion/Cargos";
                var Cargos = await apiLeon.APILEONGET<List<CargosDBModel>>(url);
                if (Cargos != null)
                {
                    return Cargos;
                }
                else
                {
                    return new List<CargosDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CargosDBModel>();
            }
        }
        public async Task<List<CargosDBModel>> Create_Update_Delete_CargoDataAsync(requestCargosModel model, int opcion)
        {
            try
            {
                string url = "";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<CargosDBModel>> CargoResponse = null;

                switch (opcion)
                {
                    case 1:
                        url = $"/api/Configuracion/Cargos";
                        CargoResponse = await apiLeon.APILEONPOST<Respuesta<List<CargosDBModel>>>(url, model);
                        break;
                    case 2:
                        url = $"/api/Configuracion/Cargos/Cargos";
                        CargoResponse = await apiLeon.APILEONPUT<Respuesta<List<CargosDBModel>>>(url, model);
                        break;
                    case 3:
                        url = $"/api/Configuracion/Cargos?IdCargo={model.IdCargo}";
                        CargoResponse = await apiLeon.APILEONDEL<Respuesta<List<CargosDBModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (CargoResponse != null && (CargoResponse.Exito == 1 || CargoResponse.Exito == 0))
                {
                    return CargoResponse.Data;
                }
                else
                {
                    return new List<CargosDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CargosDBModel>();
            }
        }
        public async Task<List<CargosDBModel>> OnOffCargoAsync(requestCargosModel model)
        {
            try
            {
                string url = $"/api/Configuracion/Cargos/EstCargos";
                Respuesta<List<CargosDBModel>> EnableResponse = null;
                EnableResponse = await apiLeon.APILEONPUT<Respuesta<List<CargosDBModel>>>(url, model);
                if (EnableResponse != null && (EnableResponse.Exito == 1 || EnableResponse.Exito == 0))
                {
                    return EnableResponse.Data;
                }
                else
                {
                    return new List<CargosDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CargosDBModel>();
            }
        }
    }
}
