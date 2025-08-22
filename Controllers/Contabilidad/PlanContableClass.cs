using Clases.Sistema;
using LeonXIIICore.Models.Sistema;
using LeonXIIICore.Models.Contabilidad;


namespace LeonXIIICore.Controllers.Contabilidad
{
    public class PlanContableClass
    {
        private readonly APILEON apiLeon = new APILEON();
        public PlanContableClass() { }
        public async Task<List<PlanCuentasDBModel>> LoadPlanContableDataAsync(string año, string ctacontable = "", bool op = false)
        {
            try
            {
                //var apiLeon = new APILEON();
                string url;

                if (op)
                {
                    url = $"/api/Contabilidad/PlaCuentasController/PlanCuentasCta?anio={año}&ctacontable={ctacontable}";
                }
                else
                {
                    url = $"/api/Contabilidad/PlaCuentasController/PlanCuentas?anio={año}&ctacontable={ctacontable}";
                }


                // Construir la URL con el año proporcionado
                //string url = $"/api/Contabilidad/PlaCuentasController/PlanCuentas?anio={año}";

                // Realizar la solicitud GET a la API
                var planContableResponse = await apiLeon.APILEONGET<List<PlanCuentasDBModel>>(url);

                if (planContableResponse != null)
                {
                    return planContableResponse;


                }
                else
                {
                    return new List<PlanCuentasDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PlanCuentasDBModel>();
            }
        }
        public async Task<List<AreaModel>> LoadAreaDataAsync()
        {
            try
            {
                //var apiLeon = new APILEON();
                string url;

                url = $"/api/Contabilidad/PlaCuentasController/Areas";

                // Realizar la solicitud GET a la API
                var Areas = await apiLeon.APILEONGET<List<AreaModel>>(url);

                if (Areas != null)
                {
                    return Areas;
                }
                else
                {
                    return new List<AreaModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AreaModel>();
            }
        }

        /// <summary>
        /// Crear o Actualizar un registro de Plan Contable
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<PlanCuentasDBModel>> Create_Update_Delete_PlanContableDataAsync(requestPlanCuentasModel model, int opcion)
        {
            try
            {
                string url = $"/api/Contabilidad/PlaCuentasController";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<PlanCuentasDBModel>> planContableResponse = null;

                switch (opcion)
                {
                    case 1:
                        planContableResponse = await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                        break;
                    case 2:
                        planContableResponse = await apiLeon.APILEONPUT<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                        break;
                    case 3:
                        url = $"/api/Contabilidad/PlaCuentasController?ctacontable={model.CtaContable}&anio={model.Año}";
                        planContableResponse = await apiLeon.APILEONDEL<Respuesta<List<PlanCuentasDBModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (planContableResponse != null && (planContableResponse.Exito == 1 || planContableResponse.Exito == 0))
                {
                    return planContableResponse.Data;
                }
                else
                {
                    return new List<PlanCuentasDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PlanCuentasDBModel>();
            }
        }
    }
}
