using ApiAppLeon.Models.KasNet;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using static ApiAppLeon.Controllers.Utilitarios.FacturadorController;
using static ApiAppLeon.Models.Utilitarios.JsonFacturacion;
using static ApiAppLeon.Models.Utilitarios.JsonFacturacionB;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class FacturadorController : ControllerBase
    {
        public class JSON
        {
            public string? Json { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ConsultaDeudaResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ConsultaDeudaRequest>> PostJsonFacturador(DatosBoleta boleta)
        {
            DatosBoleta datosBoleta = new DatosBoleta();
            JsonFacturacionB.Root root = new JsonFacturacionB.Root();
            List<JsonFacturacionB.Item> items = new List<JsonFacturacionB.Item>();
            JsonFacturacionB.DatosDelClienteOReceptor datosDelClienteOReceptor = new JsonFacturacionB.DatosDelClienteOReceptor();
            JsonFacturacionB.Totales totales = new JsonFacturacionB.Totales();
            double total = 0.00;

            root.numero_documento = boleta.numero_documento;
            root.fecha_de_emision = boleta.fecha_de_emision;
            root.hora_de_emision = boleta.hora_de_emision;
            root.fecha_de_vencimiento = boleta.fecha_de_vencimiento;
            datosDelClienteOReceptor.numero_documento = boleta.numero_documento_DNI;
            datosDelClienteOReceptor.apellidos_y_nombres_o_razon_social = boleta.apellidos_y_nombres_o_razon_social;
            datosDelClienteOReceptor.codigo_pais = boleta.codigo_pais;
            datosDelClienteOReceptor.ubigeo = boleta.ubigeo;
            datosDelClienteOReceptor.direccion = boleta.direccion;
            datosDelClienteOReceptor.correo_electronico = boleta.correo_electronico;
            datosDelClienteOReceptor.telefono = boleta.telefono;
            if (boleta.ic > 0)
            {
                JsonFacturacionB.Item item = new JsonFacturacionB.Item();
                total = total + boleta.ic;
                item.codigo_interno = "Ic";
                item.descripcion = "INTERESES";
                item.codigo_producto_sunat = "84121503";
                item.valor_unitario = boleta.ic;
                item.precio_unitario = boleta.ic;
                item.total_base_igv = boleta.ic;
                item.total_valor_item = boleta.ic;
                item.total_item = boleta.ic;
                items.Add(item);
            }
            if (boleta.im > 0)
            {
                JsonFacturacionB.Item item = new JsonFacturacionB.Item();
                total = total + boleta.im;
                item.codigo_interno = "Im";
                item.descripcion = "INTERESES";
                item.codigo_producto_sunat = "84121503";
                item.valor_unitario = boleta.im;
                item.precio_unitario = boleta.im;
                item.total_base_igv = boleta.im;
                item.total_valor_item = boleta.im;
                item.total_item = boleta.im;
                items.Add(item);
            }
            if (boleta.iv > 0)
            {
                JsonFacturacionB.Item item = new JsonFacturacionB.Item();
                total = total + boleta.iv;
                item.codigo_interno = "Iv";
                item.descripcion = "INTERESES";
                item.codigo_producto_sunat = "84121503";
                item.valor_unitario = boleta.iv;
                item.precio_unitario = boleta.iv;
                item.total_base_igv = boleta.iv;
                item.total_valor_item = boleta.iv;
                item.total_item = boleta.iv;
                items.Add(item);
            }
            if (boleta.mue > 0)
            {
                JsonFacturacionB.Item item = new JsonFacturacionB.Item();
                total = total + boleta.mue;
                item.codigo_interno = "MuE";
                item.descripcion = "PAGO MuE";
                item.codigo_producto_sunat = "84121503";
                item.valor_unitario = boleta.mue;
                item.precio_unitario = boleta.mue;
                item.total_base_igv = boleta.mue;
                item.total_valor_item = boleta.mue;
                item.total_item = boleta.mue;
                items.Add(item);
            }
            totales.total_operaciones_inafectas = total;
            totales.total_igv = total;
            totales.total_valor = total;
            totales.total_venta = total;
            root.datos_del_cliente_o_receptor = datosDelClienteOReceptor;
            root.totales = totales;
            root.items = items;

            string AccessToken = "kr8z8zpuxEg7WYK05F83fJW96USCEErb3CIRgyvuMJlXBcKOH4";

            HttpClient tRequest = new HttpClient();
            string url = "https://comprobante.cacleonxiii.pe/api/documents";
            tRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            Task<HttpResponseMessage> getTask = tRequest.PostAsJsonAsync(new Uri(url).ToString(), root);

            HttpResponseMessage urlContents = await getTask;
            var resultjson = urlContents.Content.ReadAsStringAsync().Result;
            Console.WriteLine("urlContents.ToString");

            return Ok(resultjson);
        }
    }
}
