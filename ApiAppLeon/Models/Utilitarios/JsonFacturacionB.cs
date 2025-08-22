namespace ApiAppLeon.Models.Utilitarios
{
    public class JsonFacturacionB
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DatosDelClienteOReceptor
        {
            public string codigo_tipo_documento_identidad { get; set; } = "1";
            public string numero_documento { get; set; }
            public string apellidos_y_nombres_o_razon_social { get; set; }
            public string codigo_pais { get; set; }
            public string ubigeo { get; set; }
            public string direccion { get; set; }
            public string correo_electronico { get; set; }
            public string telefono { get; set; }
        }

        public class Item
        {
            public string codigo_interno { get; set; }
            public string descripcion { get; set; } = "INTERESES";
            public string codigo_producto_sunat { get; set; } = "84121503";
            public string codigo_producto_gsl { get; set; } = "";
            public string unidad_de_medida { get; set; } = "ZZ";
            public int cantidad { get; set; } = 1;
            public double valor_unitario { get; set; }
            public string codigo_tipo_precio { get; set; } = "01";
            public double precio_unitario { get; set; }
            public string codigo_tipo_afectacion_igv { get; set; } = "30";
            public double total_base_igv { get; set; }
            public int porcentaje_igv { get; set; } = 18;
            public double total_igv { get; set; } = 0.00;
            public double total_impuestos { get; set; } = 0.00;
            public double total_valor_item { get; set; }
            public double total_item { get; set; }
        }

        public class Root
        {
            public string serie_documento { get; set; } = "B014";
            public string numero_documento { get; set; }
            public string fecha_de_emision { get; set; }
            public string hora_de_emision { get; set; }
            public string codigo_tipo_operacion { get; set; } = "0101";
            public string codigo_tipo_documento { get; set; } = "03";
            public string codigo_tipo_moneda { get; set; } = "PEN";
            public string fecha_de_vencimiento { get; set; }
            public string numero_orden_de_compra { get; set; } = "";
            public DatosDelClienteOReceptor datos_del_cliente_o_receptor { get; set; }
            public Totales totales { get; set; }
            public List<Item> items { get; set; }
            public string informacion_adicional { get; set; } = "Descripción del Producto: ";
        }

        public class Totales
        {
            public double total_exportacion { get; set; } = 0.00;
            public double total_operaciones_gravadas { get; set; } = 0.00;
            public double total_operaciones_inafectas { get; set; }
            public double total_operaciones_exoneradas { get; set; } = 0.00;
            public double total_operaciones_gratuitas { get; set; } = 0.00;
            public double total_igv { get; set; } = 0.00;
            public double total_impuestos { get; set; } = 0.00;
            public double total_valor { get; set; }
            public double total_venta { get; set; }
        }

        public class DatosBoleta
        {
            public string? numero_documento { get; set; }
            public string? fecha_de_emision { get; set; }
            public string? hora_de_emision { get; set; }
            public string? fecha_de_vencimiento { get; set; }
            public string? numero_documento_DNI { get; set; }
            public string? apellidos_y_nombres_o_razon_social { get; set; }
            public string? codigo_pais { get; set; }
            public string? ubigeo { get; set; }
            public string? direccion { get; set; }
            public string? correo_electronico { get; set; }
            public string? telefono { get; set; }
            public double ic { get; set; } = 0.00;
            public double im { get; set; } = 0.00;
            public double iv { get; set; } = 0.00;
            public double mue { get; set; } = 0.00;
            public string? informacion_adicional { get; set; } = "Descripción del Producto: ";
        }
    }
}
