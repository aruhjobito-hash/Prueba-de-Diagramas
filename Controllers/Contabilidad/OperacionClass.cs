using LeonXIIICore.Models.Contabilidad;
using Clases.Sistema;
using LeonXIIICore.Models.Sistema;

namespace LeonXIIICore.Controllers.Contabilidad
{
    public class OperacionClass
    {
        private readonly APILEON apiLeon = new APILEON();
        public OperacionClass() { }
        public async Task<List<TipOpeDBModel>> LoadTipOpeDataAsync()
        {
            try
            {

                string url;

                url = $"/api/Contabilidad/Operacion/TipOpe";

                // Realizar la solicitud GET a la API
                var TipOpe = await apiLeon.APILEONGET<List<TipOpeDBModel>>(url);

                if (TipOpe != null)
                {
                    return TipOpe;
                }
                else
                {
                    return new List<TipOpeDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TipOpeDBModel>();
            }
        }
        public async Task<List<TabTipCuentaModel>> LoadTabTipCuentaDataAsync()
        {
            try
            {

                string url;

                url = $"/api/Contabilidad/Operacion/TabTipCuenta";

                // Realizar la solicitud GET a la API
                var TipOpe = await apiLeon.APILEONGET<List<TabTipCuentaModel>>(url);

                if (TipOpe != null)
                {
                    return TipOpe;
                }
                else
                {
                    return new List<TabTipCuentaModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TabTipCuentaModel>();
            }
        }
        public async Task<List<DocumentosModel>> LoadDocumentosDataAsync()
        {
            try
            {
                string url;
                url = $"/api/Contabilidad/Operacion/Documentos";
                var Documento = await apiLeon.APILEONGET<List<DocumentosModel>>(url);
                if (Documento != null)
                {
                    return Documento;
                }
                else
                {
                    return new List<DocumentosModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DocumentosModel>();
            }
        }
        public async Task<List<AccionOpexTipCtaModel>> LoadAccionOpeDataAsync(string idope="")
        {
            try
            {

                string url;

                url = $"/api/Contabilidad/Operacion/GetAccionOpe?IdOpe={idope}";

                // Realizar la solicitud GET a la API
                var AccionOpe = await apiLeon.APILEONGET<List<AccionOpexTipCtaModel>>(url);

                if (AccionOpe != null)
                {
                    return AccionOpe;
                }
                else
                {
                    return new List<AccionOpexTipCtaModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AccionOpexTipCtaModel>();
            }
        }
        public async Task<List<DocxOpeModel>> LoadDocumentoOpeDataAsync(string idope = "")
        {
            try
            {

                string url;

                url = $"/api/Contabilidad/Operacion/GetDocumentoOpe?IdOpe={idope}";

                // Realizar la solicitud GET a la API
                var DocumentOpe = await apiLeon.APILEONGET<List<DocxOpeModel>>(url);

                if (DocumentOpe != null)
                {
                    return DocumentOpe;
                }
                else
                {
                    return new List<DocxOpeModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DocxOpeModel>();
            }
        }
        public async Task<List<DocxOpeModel>> OnOffDocumentOpeAsync(requestDocxOpeModel model)
        {
            try
            {
                string url = $"/api/Contabilidad/Operacion/DocumentOpe";
                Respuesta<List<DocxOpeModel>> DocxOpeResponse = null;
                DocxOpeResponse = await apiLeon.APILEONPUT<Respuesta<List<DocxOpeModel>>>(url, model);
                if (DocxOpeResponse != null && (DocxOpeResponse.Exito == 1 || DocxOpeResponse.Exito == 0))
                {
                    return DocxOpeResponse.Data;
                }
                else
                {
                    return new List<DocxOpeModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DocxOpeModel>();
            }
        }
        public async Task<List<OperacionDBModel>> LoadOperacionNivel(int nivel,string idope="")
        {
            try
            {

                string url;
                url = $"/api/Contabilidad/Operacion/GetOperacion?nivel={nivel}&IdOpe={idope}";
                var Operaciones = await apiLeon.APILEONGET<List<OperacionDBModel>>(url);
                if (Operaciones != null)
                {
                    return Operaciones;
                }
                else
                {
                    return new List<OperacionDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<OperacionDBModel>();
            }
        }
        public async Task<List<OperacionDBModel>> Create_Update_Delete_OperacionDataAsync(requestOperacionModel model, int opcion)
        {
            try
            {
                string url = $"/api/Contabilidad/Operacion";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<OperacionDBModel>> OperacionResponse = null;

                switch (opcion)
                {
                    case 1:
                        OperacionResponse = await apiLeon.APILEONPOST<Respuesta<List<OperacionDBModel>>>(url, model);
                        break;
                    case 2:
                        OperacionResponse = await apiLeon.APILEONPUT<Respuesta<List<OperacionDBModel>>>(url, model);
                        break;
                    case 3:
                        //url = $"/api/Contabilidad/PlaCuentasController?ctacontable={model.CtaContable}&anio={model.Año}";
                        //OperacionResponse = await apiLeon.APILEONDEL<Respuesta<List<OperacionDBModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (OperacionResponse != null && (OperacionResponse.Exito == 1 || OperacionResponse.Exito == 0))
                {
                    return OperacionResponse.Data;
                }
                else
                {
                    return new List<OperacionDBModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<OperacionDBModel>();
            }
        }
        public async Task<List<AccionOpexTipCtaModel>> Create_Delete_AccionAsync(requestAccionOpexTipCtaModel model, int op)
        {
            try
            {
                string url = $"/api/Contabilidad/Operacion/AccionOpe";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<AccionOpexTipCtaModel>> AccionOpeResponse = null;
                switch (op)
                {
                    case 1:
                        AccionOpeResponse = await apiLeon.APILEONPOST<Respuesta<List<AccionOpexTipCtaModel>>>(url, model);
                        break;
                    case 2:
                        url = $"/api/Contabilidad/Operacion/AccionOpe?IdOpe={model.IdOpe}&IdTipCta={model.IdTipCta}";
                        AccionOpeResponse = await apiLeon.APILEONDEL<Respuesta<List<AccionOpexTipCtaModel>>>(url);
                        break;
                    default:
                        throw new ArgumentException("Opción no válida");
                }
                if (AccionOpeResponse != null && (AccionOpeResponse.Exito == 1 || AccionOpeResponse.Exito == 0))
                {
                    return AccionOpeResponse.Data;
                }
                else
                {
                    return new List<AccionOpexTipCtaModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AccionOpexTipCtaModel>();
            }
        }
        public async Task<List<DocxOpeModel>> CreateDocOpeAsync(requestDocxOpeModel model)
        {
            try
            {
                string url = $"/api/Contabilidad/Operacion/DocumentOpe";
                //var planContableResponse; //= await apiLeon.APILEONPOST<Respuesta<List<PlanCuentasDBModel>>>(url, model);
                Respuesta<List<DocxOpeModel>> DocOpeResponse = null;

                DocOpeResponse = await apiLeon.APILEONPOST<Respuesta<List<DocxOpeModel>>>(url, model);
                
                if (DocOpeResponse != null && (DocOpeResponse.Exito == 1 || DocOpeResponse.Exito == 0))
                {
                    return DocOpeResponse.Data;
                }
                else
                {
                    return new List<DocxOpeModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DocxOpeModel>();
            }
        }
    }
}
