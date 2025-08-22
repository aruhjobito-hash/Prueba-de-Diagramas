namespace ApiAppLeon.Models.Utilitarios
{
    public class JsonFacturacion
    {
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DatosDelClienteOReceptor
        {
            public string? codigo_tipo_documento_identidad { get; set; }
            public string? numero_documento { get; set; }
            public string? apellidos_y_nombres_o_razon_social { get; set; }
            public string? codigo_pais { get; set; }
            public string? ubigeo { get; set; }
            public string? direccion { get; set; }
            public string? correo_electronico { get; set; }
            public string? telefono { get; set; }
        }

        public class DatosDelEmisor
        {
            public string? codigo_pais { get; set; }
            public string? ubigeo { get; set; }
            public string? direccion { get; set; }
            public string? correo_electronico { get; set; }
            public string? telefono { get; set; }
            public string? codigo_del_domicilio_fiscal { get; set; }
        }

        public class DocumentoAfectado
        {
            public string? external_id { get; set; }
        }

        public class Item
        {
            public string? codigo_interno { get; set; }
            public string? descripcion { get; set; }
            public string? codigo_producto_sunat { get; set; }
            public string? unidad_de_medida { get; set; }
            public int? cantidad { get; set; }
            public double? valor_unitario { get; set; }
            public string? codigo_tipo_precio { get; set; }
            public double? precio_unitario { get; set; }
            public string? codigo_tipo_afectacion_igv { get; set; }
            public double? total_base_igv { get; set; }
            public int? porcentaje_igv { get; set; }
            public int? total_igv { get; set; }
            public int? total_impuestos { get; set; }
            public double? total_valor_item { get; set; }
            public double? total_item { get; set; }
        }

        public class Root
        {
            public string? serie_documento { get; set; }
            public string? numero_documento { get; set; }
            public string? fecha_de_emision { get; set; }
            public string? hora_de_emision { get; set; }
            public string? codigo_tipo_documento { get; set; }
            public string? codigo_tipo_nota { get; set; }
            public string? motivo_o_sustento_de_nota { get; set; }
            public string? codigo_tipo_moneda { get; set; }
            public string? numero_orden_de_compra { get; set; }
            public DatosDelEmisor? datos_del_emisor { get; set; }
            public DocumentoAfectado? documento_afectado { get; set; }
            public DatosDelClienteOReceptor? datos_del_cliente_o_receptor { get; set; }
            public Totales? totales { get; set; }
            public List<Item>? items { get; set; }
        }

        public class Totales
        {
            public double? total_exportacion { get; set; }
            public double? total_operaciones_gravadas { get; set; }
            public double? total_operaciones_inafectas { get; set; }
            public double? total_operaciones_exoneradas { get; set; }
            public double? total_operaciones_gratuitas { get; set; }
            public double? total_igv { get; set; }
            public double? total_impuestos { get; set; }
            public double? total_valor { get; set; }
            public double? total_venta { get; set; }
        }


    }
}